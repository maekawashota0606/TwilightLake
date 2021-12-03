using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField]
    private float hp;

    public float HitPoint
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            if(hp<=0)
            {
                GameManager.Point++;
                //Destory�ɂ��Ă��������g�p���邩��
                //Enemy�𐶐�����Ƃ���Active���[�hfalse�ɂȂ��Ă���̂��g�p
                this.gameObject.SetActive(false);
            }
        }
    }
    public void AddDamage(int damage)
    {
        HitPoint -= damage;
    }
}
