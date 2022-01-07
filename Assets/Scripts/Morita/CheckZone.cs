using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckZone : MonoBehaviour
{
    /*--------------------------------------------------*/
    /*SerializeField*/
    [SerializeField,Header("チェックゾーンの大きさ(単位:人数)")]
    private int Num_of_Player;

    /*--------------------------------------------------*/
    /*private Zone*/
    private BoxCollider BC;
    /*--------------------------------------------------*/
    /*public Zone*/


    private void Awake()
    {
        //Boxcollider取得
        BC = this.gameObject.GetComponent<BoxCollider>();
    }

    private void Start()
    {
        //Boxコライダー
        BC.size = new Vector3(Num_of_Player,BC.size.y,BC.size.z);
    }
}
