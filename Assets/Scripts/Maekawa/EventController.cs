using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class EventController : SingletonMonoBehaviour<EventController>
{
    [SerializeField]
    private Canvas _ScenarioCanvas = null;
    [SerializeField]
    private Text _mainText = null;
    [SerializeField]
    private GameObject _BranchArea = null;
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
        _ScenarioCanvas.gameObject.SetActive(false);
        _BranchArea.gameObject.SetActive(false);
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
            // コメントアウト, 空白行を除外
            if (row.StartsWith("#") || row.StartsWith(","))
                continue;

            //
            string[] datas = row.Split(',');
            // 今回必要な部分をフィルター
            if (int.Parse(datas[(int)FieldName.Index]) != index)
                continue;

            //
            int stateAtRow = int.Parse(datas[(int)FieldName.State]);
            // State分リストを生成
            if (_datasAtIndex.Count <= stateAtRow)
                _datasAtIndex.Add(new List<string[]>(100));
            // ステートで振り分けて追加
            _datasAtIndex[stateAtRow].Add(datas);
        }
        reader.Close();
    }

    public IEnumerator DisplayScenario(NPC npc)
    {
        if (_isActive)
            yield break;
        _isActive = true;

        _ScenarioCanvas.gameObject.SetActive(true);
        LoadScenario(npc.eventID, npc.eventIndex);

        Debug.Log("start");
        int row = 0;
        bool isEnd = false;
        while (!isEnd)
        {
            // テキスト表示
            Debug.Log(_datasAtIndex[npc.eventState][row][(int)FieldName.Message]);

            // Y/N選択
            bool answer = false;
            if (_datasAtIndex[npc.eventState][row][(int)FieldName.Isbranch] != "0")
            {
                // 選択肢
                Debug.Log($"Y = { _datasAtIndex[npc.eventState][row][(int)FieldName.TrueWord] } / N = {_datasAtIndex[npc.eventState][row][(int)FieldName.FalseWord]}");


                bool isConfirm = false;
                do
                {
                    yield return null;

                    Debug.Log("enter");
                    if (Input.GetButtonDown("Jump"))
                    {
                        isConfirm = true;
                        answer = true;
                    }
                    else if(Input.GetButtonDown("Avoid"))
                    {
                        isConfirm = true;
                    }
                }
                while (isConfirm);
            }
            else
            {
                // テキスト送りの入力受付
                do
                {
                    yield return null;
                }
                while (!Input.GetButtonDown("Jump"));
            }

            // 終了判定
            if (_datasAtIndex[npc.eventState][row][(int)FieldName.IsEnd] != "0")
                isEnd = true;

            // イベント進行
            int state = 0;
            if (int.TryParse(_datasAtIndex[npc.eventState][row][(int)FieldName.ChangeState], out state))
            {
                row = 0;
                npc.eventState = state;
            }

            //
            if (_datasAtIndex[npc.eventState][row][(int)FieldName.Isbranch] != "0")
            {
                if (answer)
                    row = int.Parse(_datasAtIndex[npc.eventState][row][(int)FieldName.IfChooseTrue]);
                else
                    row = int.Parse(_datasAtIndex[npc.eventState][row][(int)FieldName.IfChooseFalse]);
            }
            else
                row++;
        }

        _ScenarioCanvas.gameObject.SetActive(false);
        _BranchArea.gameObject.SetActive(false);
        _isActive = false;
        Debug.Log("end");
    }
}
