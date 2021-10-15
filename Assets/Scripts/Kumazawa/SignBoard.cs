using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignBoard : MonoBehaviour
{
    public Text POPUP;
    //接触判定
    //private bool popUp = false;


    //

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter (Collider player)
    {
        //Text.SetActive(true);

        Debug.Log("当たってる");
    }
}
