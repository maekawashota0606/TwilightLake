using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckZone : MonoBehaviour
{
    /*--------------------------------------------------*/
    /*SerializeField*/
    [SerializeField]
    private BoxCollider BC;
    [SerializeField,Header("チェックゾーンの大きさ(単位:人数)")]
    private int Num_of_Player;

    /*--------------------------------------------------*/
    /*private Zone*/
    private EnemyAI enemy;
    /*--------------------------------------------------*/
    /*public Zone*/
    

    private void Start()
    {
        enemy = GetComponentInParent<EnemyAI>();
        //Boxcollider取得
        BC = this.gameObject.GetComponent<BoxCollider>();
        //Boxコライダー
        BC.size = new Vector3(Num_of_Player,BC.size.y,BC.size.z);
        BC.center = new Vector3(enemy.getDirection() * (Num_of_Player / 2), BC.center.y, BC.center.z);
    }
}
