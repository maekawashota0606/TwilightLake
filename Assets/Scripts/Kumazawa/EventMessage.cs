using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class EventMessage : MonoBehaviour
{
    public Image canvas;
    public SignBoard signBoard;
    public Timer timer;
    public SearchText searchText;
    public CSVReader cSV;

    public GameObject ButtonYes;
    public GameObject ButtonNo;
    
    [Header("メッセージUI")]
    public Text messageText;

    //unity側で表示するテキストを書く
    [SerializeField]
    private List<string> allMessage;

    //何番目のメッセージか
    private int messsageNum = 0;

    //テキストスピード
    [SerializeField]
    private float textspeed = 0.05f;

    //アイコンの点滅の経過時間
    private float elapsedTime = 0f;

    //今見ている文字番号
    private int nowTextNum = 0;

    //マウスクリックを促すアイコン
    public GameObject clickIcon;

    ////クリックアイコンの点滅秒数
    //[SerializeField]
    //private float clickFlashTime = 0.2f;

    //メッセージを全て表示したかどうか
    private bool isEndMessage = false;

    //会話を再度表示するかどうかのやつ
    private bool returnMessage = false;

    // Start is called before the first frame update
    void Start()
    {
        //UIを全て非表示にする
        canvas.enabled = false;//enabledはImageを非表示なので
        messageText.enabled = false;
        clickIcon.SetActive(false);
        ButtonYes.SetActive(false);
        ButtonNo.SetActive(false);

        cSV.GetComponent<CSVReader>().Read();
        allMessage = cSV.GetComponent<CSVReader>().csvDatas;
    }

    public void Event1()
    {
        //kanbanが持ってるSihnBoardスクリプトの中のフラグ変数がtrueの時
        if (signBoard.KanbanHit == true && Input.GetKeyDown(KeyCode.Space))
        {
            canvas.enabled = true;
            clickIcon.SetActive(true);
            messageText.enabled = true;
            searchText.HideSearch();

            //messageNumがallMessageの数字より少ないなら
            if (messsageNum < allMessage.Count)
            {
                //ここまでに表示しているテキストに残りのメッセージに対応した数字を足す
                //messsageNum++;

                messageText.text = allMessage[messsageNum];
                string currentEvent = allMessage[messsageNum];
                string[] datas = currentEvent.Split(',');
                Debug.Log("テキスト出たあ");

                //messageNumがcaseの数字まで来たとき
                switch (messsageNum)
                {
                    //2行目に来たら分岐のボタンを表示(caseの後ろの数字を変えると色んな行で分岐のボタンを表示できる)
                    case 2:
                        Debug.Log("KUMAAAAAAAAAAAAAAAA");
                        clickIcon.SetActive(false);
                        ButtonYes.SetActive(true);
                        ButtonNo.SetActive(true);
                        break;
                    //7行目に来たら分岐のボタンを表示(caseの後ろの数字を変えると色んな行で分岐のボタンを表示できる)
                    case 7:
                        clickIcon.SetActive(false);
                        ButtonYes.SetActive(true);
                        ButtonNo.SetActive(true);
                        break;
                    //他の行数に来たら会話を進める
                    default:
                        clickIcon.SetActive(true);
                        ButtonYes.SetActive(false);
                        ButtonNo.SetActive(false);
                        break;
                }
            }
            //messageNumがallMessageの数字より多いなら会話を終了する
            else
            {
                nowTextNum = 0;
                messsageNum++;
                messageText.enabled = false;
                clickIcon.SetActive(false);
                canvas.enabled = false;

                elapsedTime = 0f;

                //メッセージが全部表示されていたらゲームオブジェクト自体の削除
                if (messsageNum >= allMessage.Count)
                {
                    isEndMessage = true;
                }
            }
        }
        ////クリックアイコンの点滅処理
        //if (messsageNum < allMessage.Length - 1 && canvas.enabled == true)
        //{
        //    elapsedTime += Time.deltaTime;

        //    //クリックアイコンを点滅する時間を超えた時、反転させる
        //    if (elapsedTime >= clickFlashTime)
        //    {
        //        clickIcon.SetActive = !clickIcon.SetActive;
        //        elapsedTime = 0f;
        //    }
        //}
    }
}
