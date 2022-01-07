using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Axe : MonoBehaviour
{
    [SerializeField, Header("�����鍂��")]
    private float FlyHight = 5.0f;
    [SerializeField, Header("�͂��܂ł̎���")]
    private float FlyDuration = 5.0f;
    [SerializeField]
    private int Damage;

    private EnemyAI enemyAI;
    private void Start()
    {
        enemyAI = this.transform.parent.GetComponentInChildren<EnemyAI>();
        //�R�[���o�b�N���Ȃ���Destory�����,DOTween����x�����o��
        this.transform.DOJump(enemyAI.target.position, jumpPower: FlyHight, numJumps: 1, duration: FlyDuration).OnComplete(CallbackFunction);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //Player.Instance.AddDamage(Damage);
            //�����Ƀ_���[�W��^����֐������
            //this.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// �R�[���o�b�N�֐�
    /// </summary>
    void CallbackFunction()
    {
        Destroy(this.gameObject);
    }
}
