using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    //シナリオを格納
    private string[] scenarios2;

    //uiTextへの参照を保つ
    private Text uitext;

    //現在の行番号
    int currentLine;

    //セリフを表示するテキスト
    public Text message;
    int flag = 0;

    //テキストウィンドウ
    public GameObject panel;

    public void StartText(string[] scenarios)
    {
        flag = 1;
        scenarios2 = scenarios;
        currentLine = 0;
        uitext = message;

        panel.SetActive(true);

        uitext.gameObject.SetActive(true);
        TextUpdate();
    }
    void TextUpdate()
    {
        uitext.text = scenarios2[currentLine];
        currentLine++;
    }

}
