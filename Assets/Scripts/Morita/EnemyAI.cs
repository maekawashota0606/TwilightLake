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
    [SerializeField,Header("移動速度")]
    private float M_Speed;
    [SerializeField,Header("見える範囲")]
    private float SeeDistance;
    [SerializeField, Header("この距離に入ったら移動停止")]
    private float Attack_Distance;
    [SerializeField, Header("クールダウン時間(0だったらデフォルトクールダウン時間が適応される)")]
    private float CoolDownTime;
    [SerializeField, Header("攻撃されたときの硬直時間")]
    private float HurtTime;

    private int HP;
    private float distance; //敵とプレイヤーの距離を保存する
    private EnemyAttack e_attack;
    private Animator animator;
    private bool canMove;
    private Rigidbody rb;
    #endregion

    #region Public Variables
    //------public------//
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange; //プレーヤーが範囲内にあるかどうかを確認します
    [HideInInspector, Tooltip("向き,-1か1 にしかならないように設定する")] public int direction;
    //Debug用
    public bool isGround;
    //------取得ゾーン-----//
    [Header("ここから下は動かさないでください")]
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
        //敵とプレイヤーの距離を代入
        distance = Vector2.Distance(transform.position, target.position);

        //ray castで直線上にPlayerいるかどうか確認する
        if (CanSeePlayer())
        {
            //確認出来たら Attack.csのN_Attackを開始する(これに関してはEnemyAIとSprictを統合するかも)
            StartCoroutine(e_attack.N_Attack(CoolDownTime,cast.position));
        }
        //Cooldownをはさむ
        //FixedUpdate中、inRangeがtrueの限り、攻撃し続ける。
    }

    /// <summary>
    /// Playerを検知したら色が変更される
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
    /// targetに向かって移動
    /// </summary>
    void Move()
    {
        //ターゲット位置を一時変数に格納し、敵はX軸のみで移動させる
        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y,transform.position.z);
        //プレイヤーのXとY位置はYと等しい
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
    /// 方向のリミットを自動で選択する
    /// </summary>
    public void SelectTarget()
    {
        //左リミットまでの距離を計算
        float distanceToLeft = Vector3.Distance(transform.position, leftLimit.position);
        //右リミットまでの距離を計算
        float distanceToRight = Vector3.Distance(transform.position, rightLimit.position);

        //リミットを比較し、距離が遠いほうを移動ターゲットとする
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
    /// 現在行動範囲の中に入っているのかをチェック
    /// </summary>
    /// <returns>入っていればtrue 入ってなければfalse</returns>
    private bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    /// <summary>
    /// 回転が必要かどうかを判断し
    /// 回転させる
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
    /// Enemyの向きを取得
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

        //当たってるobjがplayerかどうかの判断
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
        //ロジック止める動作
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
