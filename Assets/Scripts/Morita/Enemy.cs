using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject Knife;
    [SerializeField]
    private float Cooldowntime;

    //�N�[���_�E�������ǂ���
    private bool isCooldown = false;
    private GameObject Player;

    //Enemy�̌���
    public int direction = -1;


    private void Start()
    {
        //Player�擾
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        //�N�[���_�E���o�Ȃ���΍U������
        if(isCooldown == false)
        {
            //�A�^�b�N!!!!!
            StartCoroutine("Attack");
        }
    }

    /// <summary>
    /// ?????????
    /// </summary>
    IEnumerator Attack()
    {
        //�N�[���_�E���ɓ���
        isCooldown = true;

        if (isCooldown != true)
        {
            //�i�C�t����!!!!!!!!!!!!!!!!!!!
            Instantiate(Knife, this.transform);
            //�N�[���_�E��
            yield return new WaitForSeconds(Cooldowntime);
            //�N�[���_�E��end
            isCooldown = false;
        }
    }
}
