using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyAI : MonoBehaviour
{
    enum State
    {
        Idel = 0x01,
        Move = 0x02,
        Attack = 0x04,
        Hurt = 0x08
    }
    [SerializeField]
    State state;
    #region private Variables
    //-----private-----//
    [SerializeField,Header("�ړ����x")]
    private float M_Speed;
    [SerializeField,Header("������͈�")]
    private float SeeDistance;
    [SerializeField, Header("���̋����ɓ�������ړ���~")]
    private float Attack_Distance;
    [SerializeField, Header("�N�[���_�E������(0��������f�t�H���g�N�[���_�E�����Ԃ��K�������)")]
    private float CoolDownTime;
    [SerializeField, Header("�U�����ꂽ�Ƃ��̍d������")]
    private float HurtTime;

    private int HP;
    private float distance; //�G�ƃv���C���[�̋�����ۑ�����
    private EnemyAttack e_attack;
    private Animator animator;
    private bool canMove;
    private Rigidbody rb;
    #endregion

    #region Public Variables
    //------public------//
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange; //�v���[���[���͈͓��ɂ��邩�ǂ������m�F���܂�
    [HideInInspector, Tooltip("����,-1��1 �ɂ����Ȃ�Ȃ��悤�ɐݒ肷��")] public int direction;
    //Debug�p
    public bool isGround;
    //------�擾�]�[��-----//
    [Header("�������牺�͓������Ȃ��ł�������")]
    public GameObject hotZone;
    public GameObject triggerArea;
    public Transform leftLimit;
    public Transform rightLimit;
    [SerializeField]
    private Transform cast;
    [SerializeField]
    private LayerMask groundLayer;
    [HideInInspector]public bool isAttackMode = false;
    #endregion

    private void Awake()
    {
        e_attack = this.transform.GetComponent<EnemyAttack>();
        rb = this.gameObject.GetComponent<Rigidbody>();
        animator = transform.GetComponent<Animator>();

    }

    private void Start()
    {
        SelectTarget();
    }
    private void FixedUpdate()
    {
        if (!InsideOfLimits()&&!inRange)
        {
            SelectTarget();
        }
        if (inRange&&CanSeePlayer())
        {
            AddAttackFlag();
        }

        CheckCanMove();

        if((state & State.Hurt) == State.Hurt)
        { 
        
        }
        else
        {
            if ((state & State.Attack) == State.Attack)
            {
                AttackMode();
            }
            if ((state & State.Move) == State.Move)
            {
                Move();
            }
        }

        c_AttackMode();
    }

    void AttackMode()
    {
        //�G�ƃv���C���[�̋�������
        distance = Vector2.Distance(transform.position, target.position);

        //ray cast�Œ������Player���邩�ǂ����m�F����
        if (CanSeePlayer())
        {
            //�m�F�o������ Attack.cs��N_Attack���J�n����(����Ɋւ��Ă�EnemyAI��Sprict�𓝍����邩��)
            StartCoroutine(e_attack.N_Attack(CoolDownTime,cast.position));
        }
        //Cooldown���͂���
        //FixedUpdate���AinRange��true�̌���A�U����������B
    }

    /// <summary>
    /// Player�����m������F���ύX�����
    /// </summary>
    void c_AttackMode()
    {
        var render = this.GetComponent<Renderer>();
        if (isAttackMode==false)
        {
            render.material.color = Color.red;
            
        }
        else
        {
            render.material.color = Color.yellow;
        }
    }

    /// <summary>
    /// target�Ɍ������Ĉړ�
    /// </summary>
    void Move()
    {
        //�^�[�Q�b�g�ʒu���ꎞ�ϐ��Ɋi�[���A�G��X���݂̂ňړ�������
        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y,transform.position.z);
        //�v���C���[��X��Y�ʒu��Y�Ɠ�����
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, M_Speed * Time.deltaTime);
    }

    void CheckCanMove()
    {
        if(inRange&!CanSeePlayer())
        {
            AddMoveFlag();
        }
        if((state & State.Attack)==State.Attack && CanSeePlayer())
        {
            DelMoveFlag();
        }
        if(!inRange)
        {
            AddMoveFlag();
        }
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

    /// <summary>
    /// Enemy�̌������擾
    /// </summary>
    /// <returns></returns>
    public int getDirection()
    {
        return direction;
    }

    private bool CanSeePlayer()
    {
        bool val = false;
        Ray ray;
        ray = new Ray(cast.position, new Vector3(direction,0,0));
        RaycastHit Hitinfo;

        //�������Ă�obj��player���ǂ����̔��f
        if(Physics.Raycast(ray, out Hitinfo, SeeDistance))
        {
            if(Hitinfo.collider.gameObject.tag =="Player")
            {
                val = true;
            }
        }
        Debug.DrawRay(cast.position, new Vector3(direction * SeeDistance, 0, 0), color: Color.red);
        return val;
    }
    public IEnumerator Hurt()
    {
        //���W�b�N�~�߂铮��
        state = state | State.Hurt;
        yield return new WaitForSeconds(HurtTime);
        state = state & ~State.Hurt;
    }

    private void AddIdelFlag()
    {
        state = state | State.Idel;
    }
    private void DelIdelFalg()
    {
        state = state & ~State.Idel;
    }
    private void AddMoveFlag()
    {
        state = state | State.Move;
    }
    private void DelMoveFlag()
    {
        state = state & ~State.Move;
    }
    private void AddAttackFlag()
    {
        state = state | State.Attack;
    }
    private void DelAttackFlag()
    {
        state = state & ~State.Attack;
    }
    private void AddHurtFlag()
    {
        state = state | State.Hurt;
    }
    private void DelHurtFlag()
    {
        state = State.Hurt;
    }
}
