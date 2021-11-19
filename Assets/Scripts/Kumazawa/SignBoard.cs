using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignBoard : MonoBehaviour
{
    //看板に付けるスクリプト

    //看板に入ってスペースが押されたかの判定
    public bool Aflagflag = false;

    //プレイヤーが範囲内にいるかどうかの判定
    public bool KanbanHit = false;

    //プレイヤーが範囲外に出たかの判定
    public bool KanbanGetout = false;

    public TalkPanel talkPanel;
    public SearchText searchText;

    //〇ボタンで調べるUIを取得するやつ
    public Image Search;

    private void OnTriggerEnter(Collider other)
    {
        //プレイヤーが範囲内に入ったら会話する
        if (other.gameObject.tag == "Player")
        {
            KanbanHit = true;
            searchText.ShowSearch();

            Debug.Log("当たった");
        }
    }
    private void OnTriggerExit(Collider other)
    {
            //プレイヤーが範囲外に出たら会話しない
            if (KanbanHit == true)
            {
                KanbanHit = false;
                searchText.HideSearch();
            }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーが看板に当たってスペースキーが押された時の処理
        if (KanbanHit == true && Input.GetKeyDown(KeyCode.Space))
        {
            Aflagflag = true;
        }
    }

    //private void OnTriggerEnter (Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {

    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    //プレイヤーが範囲外に出たら会話しない
    //    if(other.gameObject.tag == "Player")
    //    {
            
    //    }
    //}
}
