using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ : MonoBehaviour
{
    [SerializeField, Tooltip("移動速度"), Header("ForDesigner")]
    private float _VelocityX = 7.5f;
    //[SerializeField]
    //private float _maxVelocityX = 2.5f;
    [SerializeField, Tooltip("ジャンプ力")]
    private float _jumpPower = 15;
    [SerializeField, Tooltip("ジャンプの最高高度")]
    private float _maxHeightJump = 2.5f;
    [SerializeField, Header("ForEngineer")]
    private Vector3 _centerOffset = new Vector3(0, -0.5f);
    private Rigidbody _rb = null;
    private Animator _animator = null;
    private SpriteRenderer _spriteRenderer = null;
    private BoxCollider _boxCollider = null;
    private bool _isUseGravity = true;
    private bool _isInputRight = false;
    private bool _isInputLeft = false;
    private bool _isInputUp = false;
    private bool _isInputDowm = false;
    private bool _isLanding = false;
    private bool _isJumping = false;
    private float _startJumpPositionY = 0;
    private Direction _direction = Direction.None;
    private enum Direction
    {
        Left = -1,
        None,
        Right
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
                _direction = Direction.Left;
                transform.localScale = new Vector3(-1, 1, 1);
            }

            else if (Input.GetButton("Right"))
            {
                _direction = Direction.Right;
                transform.localScale = Vector3.one;
            }
        }

        // ジャンプ
        if (Input.GetButtonDown("Jump"))
            _isInputUp = true;
        
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

        //// 攻撃
        //if (Input.GetButtonDown("Attack"))
        //    Attack(AttackType.OnGround);
        //// 回避
        //else if (Input.GetButtonDown("Avoid"))
        //{
        //    _isAvoiding = true;
        //    _animator.SetTrigger("Avoid");
        //    _playerState = PlayerState.Avoid;
        //}

        //if (_isMoveUp)
        //{
        //    if (Input.GetButtonUp("Jump") || _currentJumpInputTime > _maxTimeReceptionJump)
        //    {
        //        _isJumping = true;
        //        _isMoveUp = false;
        //        _currentJumpInputTime = 0;
        //        _animator.SetTrigger("Jump");
        //        _jumpRatio = Input.GetButtonUp("Jump") ? _miniJumpRatio : 1;
        //    }
        //    else
        //        _currentJumpInputTime += Time.deltaTime;
        //}
    }

    private void FixedUpdate()
    {
        // 接地検知
        _isLanding = CheckLanding();

        AddGravity();

        // 横移動
        _rb.velocity = new Vector3(_VelocityX * (int)_direction, _rb.velocity.y);
        //_rb.AddForce(Vector3.right * _movePower * (int)_direction, ForceMode.Acceleration);
        //switch (_direction)
        //{
        //    case Direction.Right:
        //        break;
        //    case Direction.Left:
        //        break;
        //    default:
        //        break;
        //}

        // ジャンプ(接地していなければスルー)
        if (_isInputUp && _isLanding)
            jump();

        //  ジャンプ中
        if (_isJumping)
        {
            /* TODO:animationの終了で判定 */
            // ジャンプの終わりを検知
            if (_maxHeightJump < transform.position.y - _startJumpPositionY)
            {
                _isJumping = false;
                _isUseGravity = true;
                _rb.velocity = new Vector3(_rb.velocity.x, 0);
            }
        }

        // 最大速度を制御
        //if (_maxVelocityX < Mathf.Abs(_rb.velocity.x))
        //    _rb.velocity = new Vector3(_maxVelocityX * (int)_direction, _rb.velocity.y);

        // 変数初期化
        _direction = Direction.None;
        _isInputUp = false;
        _isInputDowm = false;
    }

    private void AddGravity()
    {
        if (_isUseGravity)
            _rb.AddForce(Physics.gravity * 2, ForceMode.Acceleration);
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

    private void jump()
    {
        _startJumpPositionY = transform.position.y;
        _isUseGravity = false;
        _isJumping = true;
        _rb.AddForce(_jumpPower * Vector3.up, ForceMode.VelocityChange);
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
