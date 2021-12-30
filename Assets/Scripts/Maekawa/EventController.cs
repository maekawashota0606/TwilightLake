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
        // 多重呼び出しを防ぐ
        if (_isActive)
            yield break;
        _isActive = true;

        // csv読み込み
        LoadScenario(npc.eventID, npc.eventIndex);
        // UI準備
        TextController.Instance.ActivateCanvas();

        // テキスト表示開始
        int row = 0;
        bool isEnd = false;
        while (!isEnd)
        {
            // テキスト表示
            TextController.Instance.mainText.text = _datasAtIndex[npc.eventState][row][(int)FieldName.Message];

            // 選択肢があるなら
            bool isBranch = _datasAtIndex[npc.eventState][row][(int)FieldName.Isbranch] != "0";
            if (isBranch)
                TextController.Instance.ActivateChoice(_datasAtIndex[npc.eventState][row][(int)FieldName.TrueWord],
                                                        _datasAtIndex[npc.eventState][row][(int)FieldName.FalseWord]);
            else
                TextController.Instance.DeactivateChoice();

            bool answer = false;
            // テキスト送りの入力受付
            do
            {
                yield return null;
                // Y/N選択
                if (isBranch)
                    answer = TextController.Instance.Choose();
            }
            // プレイヤーがメッセージを送るのを待つ
            while (!TextController.Instance.InputReception());

            // 指定がなければ1行進める
            int nexteRow = row + 1;

            // 終了判定
            if (_datasAtIndex[npc.eventState][row][(int)FieldName.IsEnd] != "0")
                isEnd = true;
            // 選択肢によって分岐
            else if (isBranch)
            {
                Debug.Log(_datasAtIndex[npc.eventState][row][(int)FieldName.IfChooseTrue] + _datasAtIndex[npc.eventState][row][(int)FieldName.IfChooseFalse]);
                if (answer)
                    nexteRow = int.Parse(_datasAtIndex[npc.eventState][row][(int)FieldName.IfChooseTrue]);
                else
                    nexteRow = int.Parse(_datasAtIndex[npc.eventState][row][(int)FieldName.IfChooseFalse]);         
            }

            // イベント進行
            if (int.TryParse(_datasAtIndex[npc.eventState][row][(int)FieldName.ChangeState], out int state))
            {
                nexteRow = 0;
                npc.eventState = state;
            }

            // 指定した行に進む
            row = nexteRow;
        }

        TextController.Instance.DeactivateCanvas();
        _isActive = false;
    }
}
