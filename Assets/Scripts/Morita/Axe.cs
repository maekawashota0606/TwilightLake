using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Axe : MonoBehaviour
{
    [SerializeField, Header("投げる高さ")]
    private float FlyHight = 5.0f;
    [SerializeField, Header("届くまでの時間")]
    private float FlyDuration = 5.0f;
    [SerializeField]
    private int Damage;

    private EnemyAI enemyAI;
    private void Start()
    {
        enemyAI = this.transform.parent.GetComponentInChildren<EnemyAI>();
        //コールバックつけないでDestoryすると,DOTweenから警告文出る
        this.transform.DOJump(enemyAI.target.position, jumpPower: FlyHight, numJumps: 1, duration: FlyDuration).OnComplete(CallbackFunction);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //Player.Instance.AddDamage(Damage);
            //ここにダメージを与える関数いれる
            //this.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// コールバック関数
    /// </summary>
    void CallbackFunction()
    {
        Destroy(this.gameObject);
    }
}
