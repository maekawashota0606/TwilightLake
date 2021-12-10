using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField]
    private float hp;
    private EnemyAI enemyAI;

    /// <summary>所持武器</summary>
    //public WeaponManager.WeaponType holdweapon = WeaponManager.WeaponType.Knife;

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

    private void Awake()
    {
        enemyAI = transform.GetComponent<EnemyAI>();
    }
    public void AddDamage(int damage)
    {
        enemyAI.StartCoroutine("Hurt");
        HitPoint -= damage;
    }
}
