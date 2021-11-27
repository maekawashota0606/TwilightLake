using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField, Header("ナイフのprefab")]
    private GameObject Knife;
    [SerializeField, Header("斧のprefab")]
    private GameObject axe;
    [SerializeField, Header("ブーメランのprefab")]
    private GameObject boomerang;
    [SerializeField, Header("チェックゾーンのobj")]
    private CheckZone check;
    [SerializeField, Header("デフォルトクールダウン時間")]
    private float Default_Cooldown_time = 5;

    public float point;
    private float Cooldowntime;
    //クールダウン中かどうか
    private bool isCooldown = false;
    private GameObject Player;

    enum TimeZone
    {
        moring,
        noon,
        night
    }TimeZone timezone;
    private void Start()
    {
        //Player取得
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space ))
        {
            point++;
        }
        if(point<10)
        {
            timezone = TimeZone.moring;
        }
        else if(point>=10&&point<25)
        {
            timezone = TimeZone.noon;
        }
        else if (point>=25)
        {
            timezone = TimeZone.night;
        }
    }



    /// <summary>
    /// 攻撃関数、クールダウンも含めております、
    /// N_Attack(クールダウン時間)
    /// </summary>
    public IEnumerator N_Attack(float Cooldown,Vector3 pos)
    {
        
        //クールダウンに入る
        if (isCooldown != true)
        {
            if (Cooldown != 0)
            {
                Cooldowntime = Cooldown;
            }
            else
            {
                Cooldowntime = Default_Cooldown_time;
            }
            isCooldown = true;
            //ナイフ召喚!!!!!!!!!!!!!!!!!!!
            switch(timezone)
            {
                case TimeZone.moring:
                    Instantiate(Knife, pos, transform.rotation, transform);
                    break;
                case TimeZone.noon:
                    Instantiate(axe, pos, transform.rotation, transform);
                    break;
                case TimeZone.night:
                    Instantiate(boomerang, pos, transform.rotation, transform);
                    break;
            }
            
            
            //クールダウン
            yield return new WaitForSeconds(Cooldowntime);
            //クールダウンend
            isCooldown = false;
        }
    }
}
