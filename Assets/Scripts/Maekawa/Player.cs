using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int _hp = 100;
    [SerializeField]
    private int _attackPower = 10;
    [SerializeField]
    private float _moveSpeed = 1;
    [SerializeField]
    private float _maxJumpTime = 1;
    [SerializeField]
    private float _maxJumpHeight = 2.5f;
    private Rigidbody _rb = null;
    private BoxCollider _hitBox = null;
    private Animator _animator = null;
    private bool _isMoveRight = false;
    private bool _isMoveLeft = false;
    private bool _isMoveUp = false;
    private bool _isJumping = false;
    private enum AttackType : byte
    {
        None,
        OnGround,
        Air
    }

    void Start()
    {
        _animator = GetComponent<Animator>();
        _hitBox = GetComponent<BoxCollider>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        #region
        // ↓
        Vector3 origin = transform.position + new Vector3(0, 0.35f, 0);
        Vector3 boxSize = new Vector3(1, 0.5f, 1);
        bool isLanding = IsHitGround(origin, boxSize, Vector3.down);
        // ←
        origin = transform.position + new Vector3(0.7f, 0, 0);
        boxSize = new Vector3(0.5f, 1.5f, 1);
        bool hitWallLeft = IsHitGround(origin, boxSize, Vector3.left);
        // →
        origin = transform.position + new Vector3(-0.7f, 0, 0);
        // 左右でboxSizeは同じ
        bool hitWallRight = IsHitGround(origin, boxSize, Vector3.right);
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
            speedX = -_moveSpeed;
        else if (_isMoveRight && !hitWallRight)
            speedX = _moveSpeed;

        // ジャンプ
        if (_isMoveUp)
            _isJumping = true;

        if (_isJumping)
            Jump(ref speedY);
        else if (!isLanding)
            speedY = Physics.gravity.y;
        //
        Vector3 move = new Vector3(speedX, speedY, 0);
        transform.Translate(move * Time.deltaTime);
        #endregion

        #region
        if (Input.GetMouseButtonDown(0))
            Attack(AttackType.OnGround);
        #endregion
        //_animator.SetBool("Landing", isLanding);
    }

    private void FixedUpdate()
    {

    }

    private bool IsHitGround(Vector3 origin, Vector3 boxSize, Vector3 direction)
    {
        bool isHitGround = false;
        RaycastHit hit;
        float distance = 1.0f;
        //
        if (Physics.BoxCast(origin, boxSize / 2, direction, out hit, Quaternion.identity, distance))
            if (hit.transform.gameObject.CompareTag(GameManager.Instance.groundTag))
                isHitGround = true;

        return isHitGround;
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // ↓
        //Vector3 StartPos = transform.position + new Vector3(0, 0.35f, 0);
        //Vector3 boxSize = new Vector3(1.0f, 0.5f, 1);
        //Vector3 rayDirection = Vector3.down;
        // ↑
        Vector3 StartPos = transform.position + new Vector3(0, -0.35f, 0);
        Vector3 boxSize = new Vector3(1.0f, 0.5f, 1);
        Vector3 rayDirection = Vector3.up;
        // ←
        //Vector3 StartPos = transform.position + new Vector3(0.7f, 0, 0);
        //Vector3 boxSize = new Vector3(0.5f, 1.5f, 1);
        //Vector3 rayDirection = Vector3.left;
        // →
        //Vector3 StartPos = transform.position + new Vector3(-0.7f, 0, 0);
        //Vector3 boxSize = new Vector3(0.5f, 1.5f, 1);
        //Vector3 rayDirection = Vector3.right;
        //　Cubeのレイを飛ばしターゲットと接触しているか判定
        RaycastHit hit;
        float distance = 1f;

        if (Physics.BoxCast(StartPos, boxSize / 2, rayDirection, out hit, Quaternion.identity, distance))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(StartPos + (rayDirection * distance), boxSize);
        }
    }
    #endif

    private float _jumpedDistance = 0;
    private void Jump(ref float speedY)
    {
        if(_jumpedDistance > _maxJumpHeight)
        {
            _isJumping = false;
            _jumpedDistance = 0;
            _animator.SetTrigger("Fall");
        }
        else
        {
            _jumpedDistance += _maxJumpHeight / _maxJumpTime * Time.deltaTime;
            speedY = _maxJumpHeight / _maxJumpTime;
        }
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
