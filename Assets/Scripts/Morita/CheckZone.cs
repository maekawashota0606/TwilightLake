using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckZone : MonoBehaviour
{
    /*--------------------------------------------------*/
    /*SerializeField*/
    [SerializeField,Header("�`�F�b�N�]�[���̑傫��(�P��:�l��)")]
    private int Num_of_Player;

    /*--------------------------------------------------*/
    /*private Zone*/
    private BoxCollider BC;
    private EnemyAI enemy;
    /*--------------------------------------------------*/
    /*public Zone*/


    private void Awake()
    {
        enemy = GetComponentInParent<EnemyAI>();
        //Boxcollider�擾
        BC = this.gameObject.GetComponent<BoxCollider>();
    }

    private void Start()
    {
        //Box�R���C�_�[
        BC.size = new Vector3(Num_of_Player,BC.size.y,BC.size.z);
        BC.center = new Vector3(enemy.getDirection() * Num_of_Player, BC.center.y, BC.center.z);
    }
}
