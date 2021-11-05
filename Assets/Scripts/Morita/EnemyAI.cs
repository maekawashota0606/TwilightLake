using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    #region private Variables
    //-----private-----//
    [SerializeField,Header("�ړ����x")]
    private float M_Speed;

    private int HP;
    private float distance; //�G�ƃv���C���[�̋�����ۑ�����


    /// <summary>
    /// <para>����</para>
    /// <para>-1��1 �ɂ����Ȃ�Ȃ��悤�ɐݒ肷��</para>
    /// </summary>
    private int direction;
    #endregion

    #region Public Variables
    //------public------//
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange; //�v���[���[���͈͓��ɂ��邩�ǂ������m�F���܂�
    [Header("�������牺�͓������Ȃ��ł�������")]
    public GameObject hotZone;
    public GameObject triggerArea;
    public Transform leftLimit;
    public Transform rightLimit;
    #endregion

    private void Awake()
    {
        SelectTarget();
    }

    private void FixedUpdate()
    {
        Move();

        if (!InsideOfLimits()&&!inRange)
        {
            SelectTarget();
        }
        if(inRange)
        {
            //�U������
        }

    }

    void AttackMode()
    {
        //�G�ƃv���C���[�̋�������
        distance = Vector2.Distance(transform.position, target.position);

        //ray cast�Œ������Player���邩�ǂ����m�F����
        //�m�F�o������ Attack.cs��N_Attack���J�n����(����Ɋւ��Ă�EnemyAI��Sprict�𓝍����邩��)
        //Cooldown���͂���
        //FixedUpdate���AinRange��true�̌���A�U����������B
    }

    void Move()
    {
        //�^�[�Q�b�g�ʒu���ꎞ�ϐ��Ɋi�[���A�G��X���݂̂ňړ�������
        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y,transform.position.z);
        //�v���C���[��X��Y�ʒu��Y�Ɠ�����
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, M_Speed * Time.deltaTime);
    }


    /// <summary>
    /// �����̃��~�b�g�������őI������
    /// </summary>
    public void SelectTarget()
    {
        //�����~�b�g�܂ł̋������v�Z
        float distanceToLeft = Vector3.Distance(transform.position, leftLimit.position);
        //�E���~�b�g�܂ł̋������v�Z
        float distanceToRight = Vector3.Distance(transform.position, rightLimit.position);

        //���~�b�g���r���A�����������ق����ړ��^�[�Q�b�g�Ƃ���
        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }
        Flip();
    }
    /// <summary>
    /// ���ݍs���͈͂̒��ɓ����Ă���̂����`�F�b�N
    /// </summary>
    /// <returns>�����Ă����true �����ĂȂ����false</returns>
    private bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    /*
    IEnumerator De()
    {
        while (true)
        {
            //Flip();
            yield return new WaitForSeconds(1);
        }

    }*/

    /// <summary>
    /// ��]���K�v���ǂ����𔻒f��
    /// ��]������
    /// </summary>
    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            rotation.y = 0;
            direction = -1;
        }
        else
        {
            rotation.y = 180.0f;
            direction = 1;
        }
        transform.eulerAngles = rotation;
    }

    public int getDirection()
    {
        return direction;
    }
}
