using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventMessage : MonoBehaviour
{
    [Header("大元のキャンバス")]
    public Image canvas;
    [Header("看板の取得")]
    public SignBoard signBoard;
    [Header("タイマーの取得")]
    public Timer timer;
    [Header("〇ボタンで調べるのUIの取得")]
    public SearchText searchText;
    [Header("CSVの取得")]
    public CSVReader cSV;

    [Header("スクリプト{Yesボタン}")]
    private ButtonScript buttonScriptYes;
    [Header("スクリプト{Noボタン}")]
    private ButtonScript buttonScriptNo;

    [Header("Yesボタンのテキスト")]
    [SerializeField]
    private Text ButtonTextYes;

    [Header("Noボタンのテキスト")]
    [SerializeField]
    private Text ButtonTextNo;

    [Header("Yesボタンが押されたときにisButtonClickYesを切り替えるスイッチの箱")]
    [SerializeField]
    private bool SwitchYes;
    [Header("Noボタンが押されたときにisButtonClickNoを切り替えるスイッチの箱")]
    [SerializeField]
    private bool SwitchNo;

    [Header("Yesボタンの取得")]
    public Image ButtonYes;
    [Header("Noボタンの取得")]
    public Image ButtonNo;

    ////はいを押された時のboolを0と1で
    //private int yesid = 0;

    ////いいえを押された時のboolを0と1で
    //private int noid = 0;

    [Header("メッセージUI")]
    public Text messageText;

    //unity側で表示するテキストを書く
    [Header("CSVデータが入るstringの箱")]
    [SerializeField]
    private List<string> allMessage;

    //何番目のメッセージか
    private int messsageNum = 0;

    //ボタンの大きさを操る場所の箱
    private Vector2 TFBaseYes;

    //ボタンの大きさを操る場所の箱
    private Vector2 TFBaseNo;

    //それぞれのボタンを大きくする値をUnity上からも設定できるように
    [Header("ボタンのX軸の大きさ変更値")]
    public float ButtonBigerX = 1;

    [Header("ボタンのX軸の大きさ変更値")]
    public float ButtonBigerY = 1;

    ////テキストスピード
    //[SerializeField]
    //private float textspeed = 0.05f;

    ////アイコンの点滅の経過時間
    //private float elapsedTime = 0f;

    ////今見ている文字番号
    //private int nowTextNum = 0;

    //マウスクリックを促すアイコン
    [Header("クリックアイコンのゲームオブジェクトを取得")]
    public GameObject clickIcon;

    ////クリックアイコンの点滅秒数
    //[SerializeField]
    //private float clickFlashTime = 0.2f;

    ////メッセージを全て表示したかどうか
    //private bool isEndMessage = false;

    ////会話を再度表示するかどうかのやつ
    //private bool returnMessage = false;

    // Start is called before the first frame update
    void Start()
    {
        //ここでUIを全て非表示にする。(enabledはImageを非表示なので取得は出来る)
        canvas.enabled = false;//
        messageText.enabled = false;
        clickIcon.SetActive(false);
        ButtonYes.enabled = false;
        ButtonNo.enabled = false;
        ButtonTextYes.enabled = false;
        ButtonTextNo.enabled = false;

        cSV.GetComponent<CSVReader>().Read();
        allMessage = cSV.GetComponent<CSVReader>().csvDatas;
        buttonScriptYes = ButtonYes.GetComponent<ButtonScript>();
        buttonScriptNo = ButtonNo.GetComponent<ButtonScript>();
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
                messageText.text = allMessage[messsageNum];
                string currentEvent = allMessage[messsageNum];
                string[] csvdatas = currentEvent.Split(',');

                Debug.Log("テキスト出たあ");

                //messageNumがcaseの数字まで来たとき(表示関係)
                switch (messsageNum)
                {
                    //2行目に来たら分岐のボタンを表示(caseの後ろの数字を変えると色んな行で分岐のボタンを表示できる)
                    case 1:
                        clickIcon.SetActive(false);
                        ButtonYes.enabled = true;
                        ButtonNo.enabled = true;
                        ButtonTextYes.enabled = true;
                        ButtonTextNo.enabled = true;

                        ////それぞれのボタンを押したら(datas[数字])の数字と同じ行数に飛ぶ
                        //if (SwitchYes == true)
                        //{
                        //    yesid = int.Parse(cSV.csvDatas[2]);
                        //    messsageNum = yesid;
                        //}
                        //if (SwitchNo == true)
                        //{
                        //    isEndMessage = true;
                        //}
                        break;
                    //7行目に来たら分岐のボタンを表示(caseの後ろの数字を変えると色んな行で分岐のボタンを表示できる)
                    case 6:
                        clickIcon.SetActive(false);
                        ButtonYes.enabled = true;
                        ButtonNo.enabled = true;
                        ButtonTextYes.enabled = true;
                        ButtonTextNo.enabled = true;

                        ////それぞれのボタンを押したら(datas[数字])の数字と同じ行数に飛ぶ
                        //if (SwitchYes == true)
                        //{
                        //    yesid = int.Parse(cSV.csvDatas[10]);
                        //    messsageNum = yesid;
                        //}
                        //if (SwitchNo == true)
                        //{
                        //    noid = int.Parse(cSV.csvDatas[8]);
                        //    messsageNum = noid;
                        //}
                        break;
                    //他の行数に来たら会話を進める
                    default:
                        clickIcon.SetActive(true);
                        ButtonYes.enabled = false;
                        ButtonNo.enabled = false;
                        messsageNum++;
                        break;
                }
            }
            //messageNumがallMessageの数字より多いなら会話を終了する
            else
            {
                //nowTextNum = 0;
                messsageNum++;
                messageText.enabled = false;
                clickIcon.SetActive(false);
                canvas.enabled = false;

                //elapsedTime = 0f;

                ////メッセージが全部表示されていたらゲームオブジェクト自体の削除
                //if (messsageNum >= allMessage.Count)
                //{
                //    isEndMessage = true;
                //}
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
