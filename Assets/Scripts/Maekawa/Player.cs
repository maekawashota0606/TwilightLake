using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SingletonMonoBehaviour<Player>
{
    [SerializeField]
    private int _hp = 100;
    [SerializeField]
    private int _attackPower = 10;
    [SerializeField]
    private float _moveSpeed = 1;
    [SerializeField]
    private float _maxJumpTime = 0.75f;
    [SerializeField]
    private float _maxJumpHeight = 2.5f;
    private Animator _animator = null;
    private bool _isMoveRight = false;
    private bool _isMoveLeft = false;
    private bool _isMoveUp = false;
    private bool _isJumping = false;
    private bool _isLeaveGround = false;
    private float _jumpedDistanceY = 0;
    private float _jumpedTimeCount = 0;
    private bool _isJumpEnd = false;

    private enum AttackType : byte
    {
        None,
        OnGround,
        Air
    }

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        #region
        // ↓
        Vector3 origin = transform.position + new Vector3(0, 0, 0);
        Vector3 boxSize = new Vector3(0.75f, 0.1f, 1);
        int layerMask = 1 << 6;
        bool isLanding = Physics.BoxCast(origin, boxSize / 2, Vector3.down, Quaternion.identity, 1, layerMask);
        // ←
        origin = transform.position + new Vector3(-0.25f, 0, 0);
        boxSize = new Vector3(0.5f, 1.25f, 1);
        bool hitWallLeft = Physics.CheckBox(origin, boxSize / 2,Quaternion.identity, layerMask);
        // →
        origin = transform.position + new Vector3(0.25f, 0, 0);
        // 左右でboxSizeは同じ
        bool hitWallRight = Physics.CheckBox(origin, boxSize / 2, Quaternion.identity, layerMask);
        #endregion

        #region
        _isMoveRight = false;
        _isMoveLeft = false;
        _isMoveUp = false;

        if (Input.GetKey(KeyCode.A))
        {
            _isMoveRight = false;
            _isMoveLeft = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _isMoveRight = true;
            _isMoveLeft = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isLanding)
            _isMoveUp = true;
        #endregion

        #region
        // 左右移動
        float speedX = 0, speedY = 0;

        if (_isMoveLeft && !hitWallLeft)
            speedX = -_moveSpeed * Time.deltaTime;
        else if (_isMoveRight && !hitWallRight)
            speedX = _moveSpeed * Time.deltaTime;
        // 重力
        if(!isLanding)
            speedY = Physics.gravity.y * Time.deltaTime;

        // ジャンプ開始
        if (_isMoveUp)
        {
            _isJumping = true;
            _animator.SetTrigger("Jump");
        }
        // ジャンプ中
        if (_isJumping)
        {
            _jumpedTimeCount += Time.deltaTime;

            // ジャンプ開始後、地面から離れたなら
            if (!isLanding)
                _isLeaveGround = true;

            // 一度飛んだ後、着地したならジャンプ終了
            if (_isLeaveGround && isLanding || _isJumpEnd)
                EndJump();
            else
                speedY = Jump(_jumpedTimeCount) - _jumpedDistanceY;

            _jumpedDistanceY += speedY;
        }

        //
        Vector3 move = new Vector3(speedX, speedY, 0);
        transform.Translate(move);
        #endregion

        #region
        if (Input.GetMouseButtonDown(0))
            Attack(AttackType.OnGround);
        #endregion
    }

    private void FixedUpdate()
    {

    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position + new Vector3(0, 0.1f, 0);
        Vector3 boxSize = new Vector3(0.5f, 0.05f, 1);
        int layerMask = 1 << 6;
        bool isLanding = Physics.BoxCast(origin, boxSize / 2, Vector3.down, Quaternion.identity, 1, layerMask);
        //Vector3 origin = transform.position + new Vector3(-0.25f, 0f, 0);
        //Vector3 boxSize = new Vector3(0.5f, 1.25f, 1);
        //Gizmos.color = Color.red;
        Gizmos.DrawWireCube(origin + (Vector3.down * 1), boxSize);
    }
#endif

    private float Jump(float deltaTime)
    {
        float y = -20 * Mathf.Pow(deltaTime - _maxJumpTime, 2) + _maxJumpHeight;
        if (deltaTime >= _maxJumpTime)
            _isJumpEnd = true;

        //Debug.Log(y);
        return y;
    }

    private void EndJump()
    {
        _isLeaveGround = false;
        _isJumping = false;
        _isJumpEnd = false;
        _jumpedTimeCount = 0;
        _jumpedDistanceY = 0;
        _animator.SetTrigger("Fall");
    }

    private void Attack(AttackType type)
    {
        _animator.SetInteger("Attack", (int)type);
    }

    private void EndAttack()
    {
        _animator.SetInteger("Attack", (int)AttackType.None);
    }


    // 攻撃用侵入判定
    private void OnTriggerEnter(Collider other)
    {
        // 出入りすると複数回呼ばれかねないので
        // 敵に無敵時間を作るか重複させないようにする
        IDamagable damagable = other.GetComponent<IDamagable>();

        if (damagable != null)
            damagable.AddDamage(_attackPower);
    }
}
