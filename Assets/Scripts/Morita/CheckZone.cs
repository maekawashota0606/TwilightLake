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
    /*--------------------------------------------------*/
    /*public Zone*/


    private void Awake()
    {
        //Boxcollider�擾
        BC = this.gameObject.GetComponent<BoxCollider>();
    }

    private void Start()
    {
        //Box�R���C�_�[
        BC.size = new Vector3(Num_of_Player,BC.size.y,BC.size.z);
    }
}
