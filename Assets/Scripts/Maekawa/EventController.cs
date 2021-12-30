using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class EventController : SingletonMonoBehaviour<EventController>
{
    private List<List<string[]>> _datasAtIndex = new List<List<string[]>>(100);
    private const string _FILE_PATH = "Scenario/Event_";
    private bool _isActive = false;

    private enum FieldName : byte
    {
        Index,
        State,
        Num,
        Message,
        Isbranch,
        TrueWord,
        FalseWord,
        IfChooseTrue,
        IfChooseFalse,
        SkipIndex,
        ChangeState,
        IsEnd
    }

    private void Start()
    {

    }

    private void LoadScenario(int id, int index)
    {
        _datasAtIndex.Clear();
        string path = _FILE_PATH + id.ToString(); 
        TextAsset csvFile = Resources.Load(path, typeof(TextAsset)) as TextAsset;
        StringReader reader = new StringReader(csvFile.text);
        while (reader.Peek() != -1)
        {
            string row = reader.ReadLine();
            // �R�����g�A�E�g, �󔒍s�����O
            if (row.StartsWith("#") || row.StartsWith(","))
                continue;

            //
            string[] datas = row.Split(',');
            // ����K�v�ȕ������t�B���^�[
            if (int.Parse(datas[(int)FieldName.Index]) != index)
                continue;

            //
            int stateAtRow = int.Parse(datas[(int)FieldName.State]);
            // State�����X�g�𐶐�
            if (_datasAtIndex.Count <= stateAtRow)
                _datasAtIndex.Add(new List<string[]>(100));
            // �X�e�[�g�ŐU�蕪���Ēǉ�
            _datasAtIndex[stateAtRow].Add(datas);
        }
        reader.Close();
    }

    public IEnumerator DisplayScenario(NPC npc)
    {
        // ���d�Ăяo����h��
        if (_isActive)
            yield break;
        _isActive = true;

        // csv�ǂݍ���
        LoadScenario(npc.eventID, npc.eventIndex);
        // UI����
        TextController.Instance.ActivateCanvas();

        // �e�L�X�g�\���J�n
        int row = 0;
        bool isEnd = false;
        while (!isEnd)
        {
            // �e�L�X�g�\��
            TextController.Instance.mainText.text = _datasAtIndex[npc.eventState][row][(int)FieldName.Message];

            // �I����������Ȃ�
            bool isBranch = _datasAtIndex[npc.eventState][row][(int)FieldName.Isbranch] != "0";
            if (isBranch)
                TextController.Instance.ActivateChoice(_datasAtIndex[npc.eventState][row][(int)FieldName.TrueWord],
                                                        _datasAtIndex[npc.eventState][row][(int)FieldName.FalseWord]);
            else
                TextController.Instance.DeactivateChoice();

            bool answer = false;
            // �e�L�X�g����̓��͎�t
            do
            {
                yield return null;
                // Y/N�I��
                if (isBranch)
                    answer = TextController.Instance.Choose();
            }
            // �v���C���[�����b�Z�[�W�𑗂�̂�҂�
            while (!TextController.Instance.InputReception());

            // �w�肪�Ȃ����1�s�i�߂�
            int nexteRow = row + 1;

            // �I������
            if (_datasAtIndex[npc.eventState][row][(int)FieldName.IsEnd] != "0")
                isEnd = true;
            // �I�����ɂ���ĕ���
            else if (isBranch)
            {
                Debug.Log(_datasAtIndex[npc.eventState][row][(int)FieldName.IfChooseTrue] + _datasAtIndex[npc.eventState][row][(int)FieldName.IfChooseFalse]);
                if (answer)
                    nexteRow = int.Parse(_datasAtIndex[npc.eventState][row][(int)FieldName.IfChooseTrue]);
                else
                    nexteRow = int.Parse(_datasAtIndex[npc.eventState][row][(int)FieldName.IfChooseFalse]);         
            }

            // �C�x���g�i�s
            if (int.TryParse(_datasAtIndex[npc.eventState][row][(int)FieldName.ChangeState], out int state))
            {
                nexteRow = 0;
                npc.eventState = state;
            }

            // �w�肵���s�ɐi��
            row = nexteRow;
        }

        TextController.Instance.DeactivateCanvas();
        _isActive = false;
    }
}
