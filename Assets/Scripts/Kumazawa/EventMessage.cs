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

    public GameObject ButtonYes;
    public GameObject ButtonNo;
    
    [Header("メッセージUI")]
    public Text messageText;

    //unity側で表示するテキストを書く
    [SerializeField]
    private string[] allMessage;

    //何番目のメッセージか
    private int messsageNum;

    //テキストスピード
    [SerializeField]
    private float textspeed = 0.05f;

    //アイコンの点滅の経過時間
    private float elapsedTime = 0f;

    //今見ている文字番号
    private int nowTextNum = 0;

    //マウスクリックを促すアイコン
    public GameObject clickIcon;

    //クリックアイコンの点滅秒数
    [SerializeField]
    private float clickFlashTime = 0.2f;

    //メッセージを全て表示したかどうか
    private bool isEndMessage = false;

    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;
        messageText.enabled = false;
        clickIcon.SetActive(false);
        ButtonYes.SetActive(false);
        ButtonNo.SetActive(false);

        //switch(messsageNum)
        //{
        //    case 0:
        //        break;
        //    case 1:
        //        break;
        //    case 2:
        //        break;
        //    case 3:
        //        break;
        //    case 4:
        //        break;
        //    case 5:
        //        break;
        //    case 6:
        //        break;
        //    case 7:
        //        clickIcon.enabled = false;
        //        ButtonYes.SetActive(true);
        //        ButtonNo.SetActive(true);
        //        break;
        //    case 8:
        //        break;
        //    case 9:
        //        break;
        //    case 10:
        //        break;
        //    case 11:
        //        break;
        //    case 12:
        //        break;
        //    defult:
        //        break;

        //}
    }

    public void Event1()
    {
        //kanbanが持ってるSihnBoardスクリプトの中のフラグ変数がtrueの時
        if (signBoard.KanbanHit && Input.GetKeyDown(KeyCode.Space))
        {
            canvas.enabled = true;
            clickIcon.SetActive(true);
            messageText.enabled = true;
            searchText.HideSearch();

            //messageNumがallMessageの数字より少ないなら
            if (messsageNum < allMessage.Length - 1)
            {
                //ここまでに表示しているテキストに残りのメッセージに対応した数字を足す
                messsageNum++;
                messageText.text = allMessage[messsageNum];

                if (messsageNum == 2)
                {
                    Debug.Log("KUMAAAAAAAAAAAAAAAA");
                    clickIcon.SetActive(false);
                    ButtonYes.SetActive(true);
                    ButtonNo.SetActive(true);
                }
                //7行目に来たら分岐
                if (messsageNum == 7)
                {
                    clickIcon.SetActive(false);
                    ButtonYes.SetActive(true);
                    ButtonNo.SetActive(true);
                }

                else
                {
                    clickIcon.SetActive(true);
                    ButtonYes.SetActive(false);
                    ButtonNo.SetActive(false);
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
                if (messsageNum >= allMessage.Length)
                {
                    isEndMessage = true;
                }
            }
            elapsedTime += Time.deltaTime;
        }
        //クリックアイコンの点滅処理
        if (messsageNum < allMessage.Length - 1 && canvas.enabled == true)
        {
            elapsedTime += Time.deltaTime;

            //クリックアイコンを点滅する時間を超えた時、反転させる
            if (elapsedTime >= clickFlashTime)
            {
                //clickIcon.SetActive = !clickIcon.SetActive;
                //elapsedTime = 0f;
            }
        }

    }
}
