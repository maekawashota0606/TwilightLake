using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EventSystem : MonoBehaviour
{
    public EventManager eventManager;

    //イベントのテキスト内容
    public string[] scenarios;

    //シナリオを格納
    private string[] scenarios2;//[]はリストになるやつ

    //UITextへの参照を保つ
    private Text uiText;

    //現在の行番号
    int currentLine;

    //セリフを表示するテキスト
    public Text message;
    int flag = 0;

    //テキストウィンドウ
    public GameObject sentence;

    public void StartText(string[] scenarios)
    {
        flag = 1;
        scenarios2 = scenarios;
        currentLine = 0;
        uiText = message;

    }

    public void Click()
    {
        if (flag == 1)
        {
            //現在の行番号がラストまで行ってない状態でボタンを押すとテキストを更新する
            if (currentLine < scenarios2.Length)
            {
                TextUpdate();
            }

            else
            {
                //最後まで行ったら、テキストとテキストウィンドウを消す
                uiText.gameObject.SetActive(false);
                sentence.SetActive(false);
                flag = 0;
            }
        }
    }

    void TextUpdate()
    {
        //現在の行番号をUITextに流し込み、現在の行番号を一つ追加する
        uiText.text = scenarios2[currentLine];
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentLine++;
            Debug.Log("呼ばれた");
        }
    }
    
}

