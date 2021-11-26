using System.Collections;
using UnityEngine;

public class Player : Object
{
    [SerializeField]
    private int _HP = 100;
    [SerializeField]
    private int _attackPower = 10;
    [SerializeField]
    private float _moveVelocityX = 10.0f;
    [SerializeField]
    private float _initialVelocityY = 0.1f;
    [SerializeField]
    private float _maxTimeJump = 0.75f;
    [SerializeField]
    private float _maxHeightJump = 2.5f;
    [SerializeField]
    private float _maxTimeReceptionJump = 0.1f;
    [SerializeField]
    private float _miniJumpRatio = 0.5f;
    [SerializeField]
    private float _avoidTime = 0.2f;
    [SerializeField]
    private float _avoidDistance = 2.0f;
    [SerializeField]
    private float _rigidFallTime = 1.5f;
    [SerializeField]
    private float _rigidFallDistance = 5.0f;
    [SerializeField]
    private float _invalidTime = 1.5f;
    [SerializeField]
    private float _FlickeringTime = 0.2f;
    [SerializeField]
    private float _offsetX = 0.25f;
    [SerializeField]
    private float _offsetY = -0.5f;
    private Animator _animator = null;
    private SpriteRenderer _spriteRenderer = null;
    private CapsuleCollider _capsuleCollider = null;
    private float _direction = 1;
    private Vector3 _velocity = Vector3.zero;
    private bool _isMoveRight = false;
    private bool _isMoveLeft = false;
    private bool _isMoveUp = false;
    private bool _isJumping = false;
    private bool _isLeaveGround = false;
    private float _jumpedDistanceY = 0;
    private float _currentjumpedTime = 0;
    private bool _isJumpEnd = false;
    private bool _isInvalid = false;
    private bool _isInvincible = false;
    private float _currentInvalidTime = 0;
    private float _avoidedDistanceX = 0;
    private float _currentAvoidedTime = 0;
    private bool _isAvoiding = false;
    private bool _isAvoidEnd = false;
    private float _coliderHeight = 0;
    private float _CurrentRigidTime = 0;
    private bool _isRigid = false;
    private float _lastPosY = 0;
    private float _currentFallDistance = 0;
    private bool _isLanding = false;
    private bool _lastIsLanding = false;
    private PlayerState _playerState = PlayerState.Idle;
    private float _currentJumpInputTime = 0;
    private float _jumpRatio = 1;

    private enum PlayerState : byte
    {
        Idle,
        Move,
        Jump,
        Attack,
        Dameged,
        Avoid,
    }

    private enum AttackType : byte
    {
        None,
        OnGround,
        Air
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _coliderHeight = _capsuleCollider.height;
        _isInvalid = true;
    }

    private void Update()
    {
        # region　硬直処理
        // 硬直中
        if (_isRigid)
        {
            _CurrentRigidTime += Time.deltaTime;
            if (_CurrentRigidTime >= _rigidFallTime)
            {
                _CurrentRigidTime = 0;
                _isRigid = false;
            }
            else
                return;
        }
        //着地したとき
        if (!_lastIsLanding && _isLanding)
        {
            if (_currentFallDistance >= _rigidFallDistance)
                _isRigid = true;
        }
        // 落下中なら
        if (!_isLanding && _lastPosY > transform.position.y)
            _currentFallDistance += _lastPosY - transform.position.y;
        else
            _currentFallDistance = 0;
        _lastPosY = transform.position.y;
        #endregion

        #region 入力関連
        // 同時入力を無視
        if (!Input.GetButton("Left") || !Input.GetButton("Right"))
        {
            if (Input.GetButton("Left"))
                _isMoveLeft = true;
            else if (Input.GetButton("Right"))
                _isMoveRight = true;
        }

        // しゃがみ
        if (Input.GetButtonDown("Down") && _isLanding)
        {
            _animator.SetTrigger("Squat");
            _capsuleCollider.height = _coliderHeight / 2;
            _capsuleCollider.center = new Vector3(0, -0.5f, 0);
        }

        // しゃがみ中
        if (Input.GetButton("Down") && _isLanding)
        {
            _animator.SetBool("IsSquating", true);
        }
        else
        {
            _capsuleCollider.height = _coliderHeight;
            _capsuleCollider.center = Vector3.zero;
            _animator.SetBool("IsSquating", false);
        }

        // 攻撃
        if (Input.GetButtonDown("Attack"))
            Attack(AttackType.OnGround);
        // 回避
        else if (Input.GetButtonDown("Avoid"))
        {
            _isAvoiding = true;
            _animator.SetTrigger("Avoid");
            _playerState = PlayerState.Avoid;
        }

        // ジャンプ
        if (Input.GetButtonDown("Jump") && _isLanding)
            _isMoveUp = true;

        if(_isMoveUp)
        {
            if (Input.GetButtonUp("Jump") || _currentJumpInputTime > _maxTimeReceptionJump)
            {
                _isJumping = true;
                _isMoveUp = false;
                _currentJumpInputTime = 0;
                _lastJumpingPosY = transform.position.y;
                _F = _initialVelocityY;
                _animator.SetTrigger("Jump");
                // 小/大ジャンプ判定
                _jumpRatio = Input.GetButtonUp("Jump") ? _miniJumpRatio : 1;
            }
            else
                _currentJumpInputTime += Time.deltaTime;
        }
        #endregion

        //　無敵時間経過処理
        if (_isInvalid)
            InvalidTimeCount();
    }

    private void FixedUpdate()
    {
        //
        _velocity = Vector3.zero;
        _lastIsLanding = _isLanding;
        _isLanding = CheckLanding();
        _animator.SetBool("IsLanding", _isLanding);

        // ←
        Vector3 origin = transform.position + new Vector3(-_offsetX, 0, 0);
        Vector3 boxSize = new Vector3(0.5f, 1.25f, 1);
        int layerMask = 1 << 3;
        bool hitWallLeft = Physics.CheckBox(origin, boxSize / 2, Quaternion.identity, layerMask);
        // →
        origin = transform.position + new Vector3(_offsetX, 0, 0);
        // 左右でboxSizeは同じ
        bool hitWallRight = Physics.CheckBox(origin, boxSize / 2, Quaternion.identity, layerMask);

        // 重力
        if (_playerState < PlayerState.Avoid && !_isLanding)
            AddGravity();
        // 左右移動
        if (_isMoveLeft)
            _velocity.x = _moveVelocityX * -1;
        else if(_isMoveRight)
            _velocity.x = _moveVelocityX;
        _isMoveRight = false;
        _isMoveLeft = false;

        // ジャンプ
        if (_isJumping)
        {
            _currentjumpedTime += Time.deltaTime;
            Jump(_currentjumpedTime, _jumpRatio);

            // ジャンプ開始後、地面から離れたなら
            if (!_isLanding)
                _isLeaveGround = true;
            // 一度飛んだ後、着地したならジャンプ終了
            if (_isLeaveGround && _isLanding || _isJumpEnd)
                EndJump();
        }
        // 回避
        if (_isAvoiding)
        {
            _currentAvoidedTime += Time.deltaTime;
            //moveX = Avoid(_currentAvoidedTime);

            if (_isAvoidEnd)
                EndAvoid();
        }

        // 壁判定
        if (hitWallLeft)
            _velocity.x = Mathf.Clamp(_velocity.x, 0, 100);
        if (hitWallRight)
            _velocity.x = Mathf.Clamp(_velocity.x, -100, 0);
        // しゃがみ中なら
        if (_animator.GetBool("IsSquating"))
            _velocity.x = 0;
        // 移動
        if (_playerState < PlayerState.Attack)
            Move();

        //
        SetCenter();
        MyPhysics.BoxObject playerObj = new MyPhysics.BoxObject(center, height, width);
        foreach (Ground ground in GameDirector.Instance.grounds)
        {
            MyPhysics.BoxObject groundObj = new MyPhysics.BoxObject(ground.center, ground.height, ground.width);
            if (MyPhysics.IsBoxHit(playerObj, groundObj))
            {
                transform.Translate(MyPhysics.ComputeShiftPosition(playerObj, groundObj));
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position + new Vector3(0, _offsetY, 0);
        Vector3 boxSize = new Vector3(1f, 0.1f, 1);
        float distance = 0.25f;
        int layerMask = 1 << 3;
        Gizmos.DrawWireCube(origin + Vector3.down * distance, boxSize);
        Gizmos.color = new Color(1, 0, 0);
        DrawOutLine();

        if (Physics.BoxCast(origin, boxSize / 2, Vector3.down, Quaternion.identity, distance, layerMask))
            Debug.Log("hit");
    }

    private bool CheckLanding()
    {
        Vector3 origin = transform.position + new Vector3(0, _offsetY, 0);
        Vector3 boxSize = new Vector3(0.75f, 0.1f, 1);
        float distance = 0.25f;
        int layerMask = 1 << 3;
        return Physics.BoxCast(origin, boxSize / 2, Vector3.down, Quaternion.identity, distance, layerMask);
    }

    private void Move()
    {
        _direction = _velocity.x > 0 ? 1 : -1;
        transform.localScale = new Vector3(_direction, 1, 1);
        transform.Translate(_velocity * Time.deltaTime);
    }

    private void AddGravity()
    {
        _velocity.y = Physics.gravity.y;
    }
    #region ジャンプ処理
    private float _lastJumpingPosY = 0;
    private float _F = 0;
    private void Jump(float deltaTime, float jumpRatio = 1)
    {
        if (deltaTime >= _maxTimeJump * jumpRatio)
        {
            _isJumpEnd = true;
            return;
        }

        //float y = -20 * Mathf.Pow(deltaTime - _maxJumpTime, 2) + _maxJumpHeight;
        //float moveY = y - _jumpedDistanceY;
        //_jumpedDistanceY += moveY;
        //float y =  (_initialVelocityY +  Mathf.Lerp(0, _maxHeightJump - _initialVelocityY, deltaTime / _maxTimeJump / jumpRatio));

        float tempY = transform.position.y;
        float y = (transform.position.y - _lastJumpingPosY) + _F;
        _F = _initialVelocityY / 8;
        transform.Translate(new Vector3(0, y));
        _lastJumpingPosY = tempY;
    }

    private void EndJump()
    {
        _isLeaveGround = false;
        _isJumping = false;
        _isJumpEnd = false;
        _currentjumpedTime = 0;
        _jumpedDistanceY = 0;
        _animator.SetTrigger("Fall");
    }
    #endregion

    #region 攻撃処理
    private void Attack(AttackType type)
    {
        _animator.SetInteger("Attack", (int)type);
        _playerState = PlayerState.Attack;
    }

    // animatorから呼び出し
    private void EndAttack()
    {
        _playerState = PlayerState.Idle;
        _animator.SetInteger("Attack", (int)AttackType.None);
    }

    public void AddDamage()
    {

    }
    #endregion

    #region 回避処理
    private void ActivateInvincible()
    {
        _isInvincible = true;
    }

    private void DeactivateInvincible()
    {
        _animator.SetTrigger("Fall");
        _isInvincible = false;
    }

    private float Avoid(float deltaTime)
    {
        if (deltaTime >= _avoidTime)
        {
            _isAvoidEnd = true;
            return 0;
        }

        float moveX = _avoidDistance / _avoidTime * deltaTime - _avoidedDistanceX;
        _avoidedDistanceX += moveX;

        return moveX * _direction;
    }

    private void EndAvoid()
    {
        _avoidedDistanceX = 0;
        _currentAvoidedTime = 0;
        _isAvoiding = false;
        _isAvoidEnd = false;
        _playerState = PlayerState.Idle;
    }
    #endregion
    public void RecieveDamage(int damage, Vector3 Attacker)
    {
        if (_isInvalid || _isInvincible)
            return;

        _HP -= damage;
        Debug.Log($"{damage}ダメージを受けた(残りHP:{_HP})");
        _isInvalid = true;
        StartCoroutine(Flickering());
    }

    private void InvalidTimeCount()
    {
        _currentInvalidTime += Time.deltaTime;

        if (_currentInvalidTime >= _invalidTime)
        {
            _currentInvalidTime = 0;
            _isInvalid = false;
        }
    }

    private IEnumerator Flickering()
    {
        bool isTransparent = false;

        while(_isInvalid)
        {
            if (isTransparent)
            {
                _spriteRenderer.color = new Color(1, 1, 1, 1);
                isTransparent = false;
            }
            else
            {
                _spriteRenderer.color = new Color(1, 1, 1, 0.25f);
                isTransparent = true;
            }
            yield return new WaitForSeconds(_FlickeringTime);
        }
        _spriteRenderer.color = new Color(1, 1, 1, 1);

        yield break;
    }
}