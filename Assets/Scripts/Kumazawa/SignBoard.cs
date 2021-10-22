using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignBoard : MonoBehaviour
{
    //話す内容
    public string[] scenarios;

    //プレイヤーが一定範囲内に入ったら会話できるサインを表すオブジェクト
    public GameObject POPUP;

    //プレイヤーが範囲内にいるかどうかの判定
    bool Aflagflag = false;

    public EventScript EventScript;

    //接触判定
    //private bool popUp = false;


    //

    // Start is called before the first frame update
    void Start()
    {
        POPUP.SetActive(false);
        Transform posB = this.gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.tag == "player")
        {
            Aflagflag = true;

            //会話が可能な状態である事と、会話内容を表示するやつ
            EventScript.StartEvent(Aflagflag, scenarios);

            //
            POPUP.transform.position = gameObject.transform.position
                + new Vector3(-0.5f, 1f, 0);
            POPUP.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //プレイヤーが範囲外に出たら会話しない
        if(other.gameObject.tag == "player")
        {
            Aflagflag = false;
            POPUP.SetActive(false);
        }
    }
}
