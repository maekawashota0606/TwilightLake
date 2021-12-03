using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//OnTriggerExitで離れた際のboolをfalseにすることを忘れない
//YesとNoを選択できるようにする(選択してる矢印を付ける、点滅もできたらする)
//Yesの時の分岐のリンクを作る
//Noの時の分岐のリンクを作る



public class EventManager : MonoBehaviour
{
    //public Canvas Window;
    //public GameObject KANBAN;
    //private PlayerController playerController;
    //private SearchText searchText;

    //[SerializeField]
    //private EventMessage message;

    //private void Start()
    //{

    //}

    //private void Update()
    //{
    //    message.Event1();
        
    //}

    ////皆で使うカルマポイント
    //public int KarmaPoint = 10;

    //private void Start()
    //{

    //}


    //private void Update()
    //{
    //    if(KANBAN.GetComponent<SignBoard>().Aflagflag == true )
    //    {
    //        sentence.SetActive(true);

    //        Time.timeScale = 0f;
    //    }

    //    if (KANBAN.GetComponent<SignBoard>().Aflagflag == true && Input.GetKeyDown(KeyCode.Space))
    //    {
    //        KANBAN.GetComponent<SignBoard>().Aflagflag = false;
    //        sentence.SetActive(false);

    //        Time.timeScale = 1f;
    //    }
    //}

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
