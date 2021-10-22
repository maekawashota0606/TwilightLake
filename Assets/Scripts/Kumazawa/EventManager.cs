using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//看板にプレイヤーが当たったら[!]を出す(boolで管理)
//エンターキーを押したら[!]を消してウィンドウの表示(この時にboolをfalseにする)
//OnTriggerExitで離れた際のboolをfalseにすることを忘れない
//YesとNoを選択できるようにする(選択してる矢印を付ける、点滅もできたらする)
//Yesの時の分岐のリンクを作る
//Noの時の分岐のリンクを作る
//ウィンドウが出てる間は周りの動きを止める


public class EventManager : MonoBehaviour
{
    public Canvas Window;
    public Image image;
    public Hukidasi hukidasi;
    public SignBoard signBoard;
    public PlayerController playerController;

    Vector2 posA = new Vector2();
    Vector2 posB = new Vector2();

    

    private void Update()
    {
        float distance = Vector2.Distance(posA, posB);

        if(posA.x == posB.x)
        {
            Debug.Log("重なったよ");

            hukidasi.ShowBalloon();
        }
    }



    //プレイヤーが看板の判定に入った時
    /*private void OnTriggerEnter(Collider collider)
    {
        button = collider.gameObject.tag.Equals("Player");
        gameObject.SetActive(true);
    }

    //プレイヤーが看板の判定から出た時
    private void OnTriggerExit(Collider collider)
    {
        button = !collider.gameObject.tag.Equals("Player");
        gameObject.SetActive(false);
    }*/

}
