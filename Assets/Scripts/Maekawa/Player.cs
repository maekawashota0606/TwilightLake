using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SingletonMonoBehaviour<Player>
{
    [SerializeField, Tooltip("HP"), Header("ForDesigner")]
    private int _HP = 100;
    [SerializeField, Tooltip("重力の倍率")]
    private float _gravityRatio = 1;
    [SerializeField, Tooltip("移動速度")]
    private float _velocityX = 7.5f;
    //[SerializeField]
    //private float _maxVelocityX = 2.5f;
    //[SerializeField, Tooltip("Y軸最大速度")]
    //private float _maxVelocityY = 10;
    [SerializeField, Tooltip("ジャンプ力")]
    private float _jumpPower = 15;
    [SerializeField, Tooltip("大ジャンプの最高高度")]
    private float _maxHeightMaxJump = 2.5f;
    [SerializeField, Tooltip("小ジャンプの最高高度")]
    private float _maxHeightMiniJump = 1.5f;
    [SerializeField, Tooltip("ジャンプ入力の最大受付時間")]
    private float _maxTimeReceptionJump = 0.15f;
    [SerializeField, Tooltip("回避で加える力")]
    private float _avoidPower = 100;
    [SerializeField, Tooltip("被ダメージ時の無敵時間")]
    private float _invincibleTime = 1.5f;
    [SerializeField, Tooltip("被ダメージ時に吹き飛ばされる力")]
    private float _knockBackPower = 1000;
    [SerializeField, Tooltip("落下硬直が発生する距離")]
    private float _freezeFallDisitance = 3;
    [SerializeField, Tooltip("落下硬直時間")]
    private float _fallFreezeTime = 1f;
    [SerializeField, Header("ForEngineer")]
    private Vector3 _centerOffset = new Vector3(0, -0.5f);
    private Rigidbody _rb = null;
    private Animator _animator = null;
    private SpriteRenderer _spriteRenderer = null;
    private BoxCollider _boxCollider = null;
    private bool _isInputRight = false;
    private bool _isInputLeft = false;
    private bool _isInputDowm = false;
    private bool _isInputJump = false;
    private bool _isLastInputJump = false;
    private bool _isInputAttack = false;
    private bool _isInputAvoid = false;
    private int _direction = 1;
    private bool _isUseGravity = true;
    private bool _isLanding = false;
    private bool _isLastLanding = false;
    private bool _isInvincible = false;
    private bool _isAvoidInvincible = false;
    private float _currentJumpInputTime = 0;
    private float _maxHeightJump = 0;
    private float _startJumpPositionY = 0;
    private bool _isFreeze = false;
    private float _currentFreezeTime = 0;
    private float _lastPosY = 0;
    private float _currentFallDistance = 0;
    private float _currentInvincibleTime = 0;
    private Vector3 _defaultColliderSize = Vector3.zero;
    private Vector3 _colliderOffset = new Vector3(0, -0.1f);
    private ActionState _actionState = ActionState.Idle;
    private enum ActionState
    {
        Idle,
        Jump,
        Attack,
        Avoid,
        Damaged
    }


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider>();
        _defaultColliderSize = _boxCollider.size;
    }

    private void Update()
    {
        //
        _isLastLanding = _isLanding;
        _isLanding = CheckLanding();
        //
        CountInvincibleTime();

        //
        if (_isFreeze)
        {
            _currentFreezeTime += Time.deltaTime;
            if (_currentFreezeTime >= _fallFreezeTime)
            {
                _currentFreezeTime = 0;
                _isFreeze = false;
            }
            else
                return;
        }
        //着地したとき
        if (!_isLastLanding && _isLanding)
        {
            if (_currentFallDistance >= _freezeFallDisitance)
                _isFreeze = true;
        }
        // 落下中なら
        else if (!_isLanding && _lastPosY > transform.position.y)
            _currentFallDistance += _lastPosY - transform.position.y;
        else
            _currentFallDistance = 0;

        _lastPosY = transform.position.y;


        // しゃがみ
        if (Input.GetButtonDown("Down") && _isLanding)
        {
            _animator.SetTrigger("Squat");
            _boxCollider.size = new Vector3(_defaultColliderSize.x, _defaultColliderSize.y / 2);
            _boxCollider.center = new Vector3(0, -0.65f, 0);
        }

        // しゃがみ中
        if (Input.GetButton("Down") && _isLanding)
        {
            _animator.SetBool("IsSquating", true);
        }
        else
        {
            _boxCollider.size = _defaultColliderSize;
            _boxCollider.center = _colliderOffset;
            _animator.SetBool("IsSquating", false);
        }

        // 同時入力を無視
        if (!Input.GetButton("Left") || !Input.GetButton("Right"))
        {
            if (Input.GetButton("Left"))
            {
                _isInputLeft = true;
                _direction = -1;
            }
            else if (Input.GetButton("Right"))
            {
                _isInputRight = true;
                _direction = 1;
            }
        }

        // ジャンプ
        if (Input.GetButton("Jump"))
            _isInputJump = true;

        // 攻撃
        if (Input.GetButtonDown("Attack"))
            _isInputAttack = true;

        // 回避
        if (Input.GetButtonDown("Avoid"))
            _isInputAvoid = true;
    }

    private void FixedUpdate()
    {
        // 接地検知
        _animator.SetBool("IsLanding", _isLanding);

        // 落下検知
        if (_rb.velocity.y < 0 && !_isLanding)
            _animator.SetBool("IsFalling", true);
        else
            _animator.SetBool("IsFalling", false);

        AddGravity();

        switch(_actionState)
        {
            case ActionState.Jump:
                _isUseGravity = false;
                // ジャンプの終わりを検知(天井などに当たった場合着地できなくなるので要修正)
                if (_maxHeightJump < transform.position.y - _startJumpPositionY)
                    EndJump();

                if (_isInputAttack)
                    Attack();
                else if (_isInputAvoid)
                    Avoid();
                else
                    Move();
                break;
            case ActionState.Attack:
                _isUseGravity = true;
                break;
            case ActionState.Avoid:
                _isUseGravity = false;
                //
                break;
            case ActionState.Damaged:
                _isUseGravity = true;
                break;
            default:
                _isUseGravity = true;

                if (_isInputAttack)
                    Attack();
                else if (InputJump())
                    Jump();
                else if (_isInputAvoid)
                    Avoid();
                else
                    Move();
                break;
        }

        // 入力変数初期化
        _isInputRight = false;
        _isInputLeft = false;
        _isInputDowm = false;
        _isLastInputJump = _isInputJump;
        _isInputJump = false;
        _isInputAttack = false;
        _isInputAvoid = false;
    }

    public void AddDamage(int damage)
    {
        _HP -= damage;
    }

    public void AddDamage(int damage, Vector3 enemyPosition)
    {
        // 無敵なら処理しない
        if (_isInvincible || _isAvoidInvincible)
            return;

        // ダメージを受ける
        _HP -= damage;
        // 一定時間無敵になる
        _isInvincible = true;
        _actionState = ActionState.Damaged;
        _animator.SetTrigger("Damage");

        // 攻撃側との距離を計算
        //Vector3 dir = (transform.position - enemyPosition).normalized;
        Vector3 dir = transform.position - enemyPosition;

        // 左右にノックバック(敵と反対方向)
        if (dir.x > 0)
            dir = Vector3.right;
        else
            dir = Vector3.left;

        // 上下に吹き飛んだほうが見栄えがいいかも？
        _rb.velocity = Vector3.zero;
        _rb.AddForce(dir * _knockBackPower, ForceMode.VelocityChange);

        // 死亡処理
        if (_HP <= 0)
            Debug.Log("GameOver");
    }

    private void CountInvincibleTime()
    {
        if (_isInvincible)
        {
            _currentInvincibleTime += Time.deltaTime;
            if (_invincibleTime <= _currentInvincibleTime)
            {
                _isInvincible = false;
                _currentInvincibleTime = 0;
            }               
        }
    }

    private void AddGravity()
    {
        if (_isUseGravity)
            _rb.AddForce(Physics.gravity * _gravityRatio, ForceMode.Acceleration);
    }

    /// <summary>
    /// BoxCastを使用し接地しているならtrueを返します
    /// </summary>
    /// <returns></returns>
    private bool CheckLanding()
    {
        Vector3 origin = transform.position + _centerOffset;
        Vector3 boxSize = new Vector3(0.75f, 0.1f, 1);
        float distance = 0.75f;
        // Groundレイヤーを指定
        int layerMask = 1 << 3;

        return Physics.BoxCast(origin, boxSize / 2, Vector3.down, Quaternion.identity, distance, layerMask);
    }

    private void Move()
    {
        if (_isInputRight)
            _rb.velocity = new Vector3(_velocityX, _rb.velocity.y);
        else if (_isInputLeft)
            _rb.velocity = new Vector3(_velocityX * -1, _rb.velocity.y);
        else
            _rb.velocity = new Vector3(0, _rb.velocity.y);

        transform.localScale = new Vector3(_direction, 1, 1);
    }

    private bool InputJump()
    {
        bool isJump = false;

        // 押しっぱなし防止
        if (!_isLanding)
        {
            _currentJumpInputTime = 0;
            return false;
        }

        if (_isInputJump)
        {
            _currentJumpInputTime += Time.deltaTime;
            // 大ジャンプ
            if (_currentJumpInputTime > _maxTimeReceptionJump)
            {
                _maxHeightJump = _maxHeightMaxJump;
                isJump = true;
            }
        }
        // 小ジャンプ
        else if (_isLastInputJump && !_isInputJump)
        {
            _maxHeightJump = _maxHeightMiniJump;
            isJump = true;
        }

        return isJump;
    }

    private void Jump()
    {
        _actionState = ActionState.Jump;
        _currentJumpInputTime = 0;
        _animator.SetTrigger("Jump");
        _startJumpPositionY = transform.position.y;
        //_isUseGravity = false;
        _rb.AddForce(_jumpPower * Vector3.up, ForceMode.VelocityChange);
    }

    private void EndJump()
    {
        _actionState = ActionState.Idle;
        //_isUseGravity = true;
        _rb.velocity = new Vector3(_rb.velocity.x, 0);
    }

    private void Attack()
    {
        _actionState = ActionState.Attack;
        _rb.velocity = Vector3.zero;
        _animator.SetInteger("Attack", 1);
    }

    private void EndAttack()
    {
        //_isUseGravity = true;
        _actionState = ActionState.Idle;
        _animator.SetInteger("Attack", 0);
    }

    private void Avoid()
    {
        _animator.SetTrigger("Avoid");
        _actionState = ActionState.Avoid;
        //_isUseGravity = false;
        _rb.velocity = Vector3.zero;
        _rb.AddForce(Vector3.right * _avoidPower * _direction, ForceMode.VelocityChange);
    }

    private void EndAvoid()
    {
        //_isUseGravity = true;
        _rb.velocity = Vector3.zero;
    }

    private void ActivateInvincible()
    {
        _isAvoidInvincible = true;
    }

    private void DeactivateInvincible()
    {
        _isAvoidInvincible = false;
    }

    private void EndDamaged()
    {
        _actionState = ActionState.Idle;
        _rb.velocity = new Vector3(0, _rb.velocity.y);
    }

    private void ChangeState(ActionState state)
    {
        _actionState = state;
    }



    // forDebug BoxCast可視化用
    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position + _centerOffset;
        Vector3 boxSize = new Vector3(1f, 0.1f, 1);
        float distance = 0.75f;
        Gizmos.DrawWireCube(origin + Vector3.down * distance, boxSize);
    }
}
