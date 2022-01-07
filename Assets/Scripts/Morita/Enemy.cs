using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float hp;
    [SerializeField,Tooltip("死亡するときに追加するカルマポイントの値")]
    private int Point;
    [SerializeField]
    private GameDirector director;
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
                director.AddKarmaPoint(Point);
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
}
