using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField,Header("�i�C�t��prefab")]
    private GameObject Knife;
    [SerializeField,Header("�`�F�b�N�]�[����obj")]
    private CheckZone check;
    [SerializeField, Header("�f�t�H���g�N�[���_�E������")]
    private float Default_Cooldown_time = 5;

    private float Cooldowntime;
    //�N�[���_�E�������ǂ���
    private bool isCooldown = false;
    private GameObject Player;


    private void Start()
    {
        //Player�擾
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// �U���֐��A�N�[���_�E�����܂߂Ă���܂��A
    /// N_Attack(�N�[���_�E������)
    /// </summary>
    public IEnumerator N_Attack(float Cooldown)
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
            Debug.Log("Attack Now");
            Debug.Log("cooldowntime" + Cooldowntime);
            //�i�C�t����!!!!!!!!!!!!!!!!!!!
            Instantiate(Knife, this.transform);
            //�N�[���_�E��
            yield return new WaitForSeconds(Cooldowntime);
            //�N�[���_�E��end
            isCooldown = false;
        }
    }
}
