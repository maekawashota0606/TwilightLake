using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class message : MonoBehaviour
{
    public Image canvas;
    public SignBoard signBoard;
    public Timer timer;
    public SearchText searchText;

    //メッセージUI
    public Text messageText;

    [SerializeField]
    private string[] allMessage;

    //メッセージの何番目か
    private int messsageNum;

    //テキストスピード
    [SerializeField]
    private float textspeed = 0.05f;

    //次のメッセージを表示するまでの経過時間、アイコンの点滅の経過時間
    private float elapsedTime = 0f;

    //今見ている文字番号
    private int nowTextNum = 0;

    //マウスクリックを促すアイコン
    [SerializeField]
    private Image clickIcon;

    //クリックアイコンの点滅秒数
    [SerializeField]
    private float clickFlashTime = 0.2f;

    //メッセージを全て表示したかどうか
    private bool isEndMessage = false;

    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;
        clickIcon.enabled = false;
        messageText.enabled = false;
        messageText = GetComponentInChildren<Text>();
        searchText = GetComponent<SearchText>();
    }

    // Update is called once per frame
    void Update()
    {
        ////メッセージが終わっているか、メッセージが無い場合はこれ以降何もしない
        //if (isEndMessage || allMessage == null)
        //{
        //    return;
        //}

        //kanbanが持ってるSihnBoardスクリプトの中のフラグ変数がtrueの時
        if (signBoard.GetComponent<SignBoard>().KanbanHit == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                canvas.enabled = true;
                clickIcon.enabled = true;
                messageText.enabled = true;
            }
            elapsedTime += Time.deltaTime;

            //メッセージ表示中にシフトキーを押したら一括表示
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //messageNumがallMessageの数字より少ないなら
                if (messsageNum < allMessage.Length - 1)
                {
                    //ここまでに表示しているテキストに残りのメッセージに対応した数字を足す
                    messsageNum++;
                    messageText.text = allMessage[messsageNum];
                }
                //messageNumがallMessageの数字より多いなら会話を終了する
                else
                {
                    nowTextNum = 0;
                    messsageNum++;
                    messageText.enabled = false;
                    clickIcon.enabled = false;
                    canvas.enabled = false;

                    elapsedTime = 0f;

                    //メッセージが全部表示されていたらゲームオブジェクト自体の削除
                    if (messsageNum >= allMessage.Length)
                    {
                        isEndMessage = true;
                    }
                }
            }

            //クリックアイコンの点滅処理(自分でも必要なのか分からなくなってます)
            if (messsageNum < allMessage.Length - 1 && canvas.enabled == true)
            {
                elapsedTime += Time.deltaTime;

                //クリックアイコンを点滅する時間を超えた時、反転させる
                if (elapsedTime >= clickFlashTime)
                {
                    clickIcon.enabled = !clickIcon.enabled;
                    elapsedTime = 0f;
                }
            }
        }
    }
}
