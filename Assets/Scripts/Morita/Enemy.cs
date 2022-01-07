using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float hp;
    [SerializeField,Tooltip("���S����Ƃ��ɒǉ�����J���}�|�C���g�̒l")]
    private int Point;
    [SerializeField]
    private GameDirector director;
    private EnemyAI enemyAI;

    /// <summary>��������</summary>
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
                //Destory�ɂ��Ă��������g�p���邩��
                //Enemy�𐶐�����Ƃ���Active���[�hfalse�ɂȂ��Ă���̂��g�p
                this.gameObject.SetActive(false);
            }
        }
    }

    private void Awake()
    {
        enemyAI = transform.GetComponent<EnemyAI>();
    }
}
