using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ : MonoBehaviour
{
    [SerializeField, Tooltip("重力の倍率")]
    private float _gravityRatio = 1;
    [SerializeField, Tooltip("移動速度"), Header("ForDesigner")]
    private float _velocityX = 7.5f;
    //[SerializeField]
    //private float _maxVelocityX = 2.5f;
    [SerializeField, Tooltip("Y軸最大速度")]
    private float _maxVelocityY = 10;
    [SerializeField, Tooltip("ジャンプ力")]
    private float _jumpPower = 15;
    [SerializeField, Tooltip("ジャンプの最高高度")]
    private float _maxHeightJump = 2.5f;
    [SerializeField, Tooltip("回避で加える力")]
    private float _avoidPower = 100;
    [SerializeField, Header("ForEngineer")]
    private Vector3 _centerOffset = new Vector3(0, -0.5f);
    private Rigidbody _rb = null;
    private Animator _animator = null;
    private SpriteRenderer _spriteRenderer = null;
    private BoxCollider _boxCollider = null;
    private bool _isInputRight = false;
    private bool _isInputLeft = false;
    private bool _isInputUp = false;
    private bool _isInputDowm = false;
    private bool _isInputAvoid = false;
    private int _direction = 1;
    private bool _isUseGravity = true;
    private bool _isLanding = false;
    private bool _isInvincible = false;
    private bool _isJumping = false;
    private float _startJumpPositionY = 0;
    private ActionState _actionState = ActionState.Idle;
    private enum ActionState
    {
        Idle,
        Jump,
        Avoid,
    }


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        // 同時入力を無視
        if (!Input.GetButton("Left") || !Input.GetButton("Right"))
        {
            if (Input.GetButton("Left"))
            {
                _isInputLeft = true;
                _direction = -1;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (Input.GetButton("Right"))
            {
                _isInputRight = true;
                _direction = 1;
                transform.localScale = Vector3.one;
            }
        }

        // ジャンプ
        if (Input.GetButtonDown("Jump"))
            _isInputUp = true;

        //// 攻撃
        //if (Input.GetButtonDown("Attack"))
        //    Attack(AttackType.OnGround);

        // 回避
        if (Input.GetButtonDown("Avoid"))
            _isInputAvoid = true;

        //// しゃがみ
        //if (Input.GetButtonDown("Down") && _isLanding)
        //{
        //    _animator.SetTrigger("Squat");
        //    _capsuleCollider.height = _coliderHeight / 2;
        //    _capsuleCollider.center = new Vector3(0, -0.5f, 0);
        //}

        //// しゃがみ中
        //if (Input.GetButton("Down") && _isLanding)
        //{
        //    _animator.SetBool("IsSquating", true);
        //}
        //else
        //{
        //    _capsuleCollider.height = _coliderHeight;
        //    _capsuleCollider.center = Vector3.zero;
        //    _animator.SetBool("IsSquating", false);
        //}
    }

    private void FixedUpdate()
    {
        // 接地検知
        _isLanding = CheckLanding();
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
                // ジャンプの終わりを検知(天井などに当たった場合着地できなくなるので)
                if (_maxHeightJump < transform.position.y - _startJumpPositionY)
                    EndJump();

                Move();

                if (_isInputAvoid)
                    Avoid();
                break;
            case ActionState.Avoid:
                break;
            default:
                Move();
                // ジャンプ(接地していなければスルー)
                if (_isInputUp && _isLanding)
                    jump();

                if (_isInputAvoid)
                    Avoid();
                // 最大速度を制御
                //if (_maxVelocityX < Mathf.Abs(_rb.velocity.x))
                //    _rb.velocity = new Vector3(_maxVelocityX * (int)_direction, _rb.velocity.y);
                //if (_maxVelocityY < Mathf.Abs(_rb.velocity.y))
                //    _rb.velocity = new Vector3(_rb.velocity.x, _maxVelocityY * _rb.velocity.y / Mathf.Abs(_rb.velocity.y));
                break;
        }

        // 入力変数初期化
        _isInputRight = false;
        _isInputLeft = false;
        _isInputUp = false;
        _isInputDowm = false;
        _isInputAvoid = false;
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
    }

    private void jump()
    {
        _actionState = ActionState.Jump;
        _animator.SetTrigger("Jump");
        _startJumpPositionY = transform.position.y;
        _isUseGravity = false;
        _isJumping = true;
        _rb.AddForce(_jumpPower * Vector3.up, ForceMode.VelocityChange);
    }

    private void EndJump()
    {
        _actionState = ActionState.Idle;
        _isJumping = false;
        _isUseGravity = true;
        _rb.velocity = new Vector3(_rb.velocity.x, 0);
    }

    private void Avoid()
    {
        _animator.SetTrigger("Avoid");
        _actionState = ActionState.Avoid;
        _isUseGravity = false;
        _rb.velocity = Vector3.zero;
        _rb.AddForce(Vector3.right * _avoidPower * _direction);
    }

    private void EndAvoid()
    {
        _isUseGravity = true;
        _rb.velocity = new Vector3(0, _rb.velocity.y);
    }

    private void ChangeState(ActionState state)
    {
        _actionState = state;
    }

    private void ActivateInvincible()
    {
        _isInvincible = true;
    }

    private void DeactivateInvincible()
    {
        // TODO:被ダメージによる無敵時間などがある場合は処理しないように
        _isInvincible = false;
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
