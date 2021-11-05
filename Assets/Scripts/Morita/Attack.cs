using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField,Header("ナイフのprefab")]
    private GameObject Knife;
    [SerializeField,Header("チェックゾーンのobj")]
    private CheckZone check;
    [SerializeField, Header("デフォルトクールダウン時間")]
    private float Default_Cooldown_time = 5;

    private float Cooldowntime;
    //クールダウン中かどうか
    private bool isCooldown = false;
    private GameObject Player;


    private void Start()
    {
        //Player取得
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// 攻撃関数、クールダウンも含めております、
    /// N_Attack(クールダウン時間)
    /// </summary>
    public IEnumerator N_Attack(float Cooldown)
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
            Debug.Log("Attack Now");
            Debug.Log("cooldowntime" + Cooldowntime);
            //ナイフ召喚!!!!!!!!!!!!!!!!!!!
            Instantiate(Knife, this.transform);
            //クールダウン
            yield return new WaitForSeconds(Cooldowntime);
            //クールダウンend
            isCooldown = false;
        }
    }
}
