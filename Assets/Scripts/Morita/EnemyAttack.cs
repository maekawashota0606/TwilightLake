using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField, Header("�i�C�t��prefab")]
    private GameObject Knife;
    [SerializeField, Header("����prefab")]
    private GameObject axe;
    [SerializeField, Header("�u�[��������prefab")]
    private GameObject boomerang;
    [SerializeField, Header("�`�F�b�N�]�[����obj")]
    private CheckZone check;
    [SerializeField, Header("�f�t�H���g�N�[���_�E������")]
    private float Default_Cooldown_time = 5;

    public float point
    {
        get { return point; }
        set
        {
            switch(point)
            {
                case 10:
                    timezone = TimeZone.moring;
                    break;
                case 15:
                    timezone = TimeZone.moring;
                    break;
                case 25:
                    timezone = TimeZone.night;
                    break;
            }
        }
    }
    private float Cooldowntime;
    //�N�[���_�E�������ǂ���
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
        //Player�擾
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space ))
        {
            point++;
        }
    }
    /// <summary>
    /// �U���֐��A�N�[���_�E�����܂߂Ă���܂��A
    /// N_Attack(�N�[���_�E������)
    /// </summary>
    public IEnumerator N_Attack(float Cooldown,Vector3 pos)
    {
        //�N�[���_�E���ɓ���
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
            //�i�C�t����!!!!!!!!!!!!!!!!!!!
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
            //�N�[���_�E��
            yield return new WaitForSeconds(Cooldowntime);
            //�N�[���_�E��end
            isCooldown = false;
        }
    }
}
