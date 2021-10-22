using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject Knife;
    [SerializeField]
    private float Cooldowntime;

    //クールダウン中かどうか
    private bool isCooldown = false;
    private GameObject Player;

    //Enemyの向き
    public int direction = -1;


    private void Start()
    {
        //Player取得
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        //クールダウン出なければ攻撃する
        if(isCooldown == false)
        {
            //アタック!!!!!
            StartCoroutine("Attack");
        }
    }

    /// <summary>
    /// ?????????
    /// </summary>
    IEnumerator Attack()
    {
        //クールダウンに入る
        isCooldown = true;

        if (isCooldown != true)
        {
            //ナイフ召喚!!!!!!!!!!!!!!!!!!!
            Instantiate(Knife, this.transform);
            //クールダウン
            yield return new WaitForSeconds(Cooldowntime);
            //クールダウンend
            isCooldown = false;
        }
    }
}
