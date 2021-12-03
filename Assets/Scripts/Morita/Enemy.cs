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
                //Destoryにしてもメモリ使用するから
                //Enemyを生成するときはActiveモードfalseになってるものを使用
                this.gameObject.SetActive(false);
            }
        }
    }
    public void AddDamage(int damage)
    {
        HitPoint -= damage;
    }
}
