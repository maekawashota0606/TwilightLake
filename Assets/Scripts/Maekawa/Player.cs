using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SingletonMonoBehaviour<Player>
{
    [SerializeField]
    private int _HP = 100;
    [SerializeField]
    private int _attackPower = 10;
    [SerializeField]
    private float _horizontalVelocity = 10.0f;
    [SerializeField]
    private float _initialVelocity = 0.1f;
    [SerializeField]
    private float _maxJumpTime = 0.75f;
    [SerializeField]
    private float _maxJumpHeight = 2.5f;
    [SerializeField]
    private float _avoidTime = 0.2f;
    [SerializeField]
    private float _avoidDistance = 2.0f;
    [SerializeField]
    private float _invalidTime = 1.5f;
    [SerializeField]
    private float _FlickeringTime = 0.2f;
    private Animator _animator = null;
    private SpriteRenderer  _spriteRenderer = null;
    private bool _isMoveRight = false;
    private bool _isMoveLeft = false;
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
    private PlayerState _playerState = PlayerState.Idle;

    private enum PlayerState : byte
    {
       Idle,
       Move,
       Attack,
       Jump,
       Avoid
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
        _isInvalid = true;
    }

    private void Update()
    {
        #region
        // ��
        bool isLanding = CheckLanding();
        _animator.SetBool("Landing", isLanding);
        // ��
        Vector3 origin = transform.position + new Vector3(-0.25f, 0, 0);
        Vector3 boxSize = new Vector3(0.5f, 1.25f, 1);
        int layerMask = 1 << 6;
        bool hitWallLeft = Physics.CheckBox(origin, boxSize / 2,Quaternion.identity, layerMask);
        // ��
        origin = transform.position + new Vector3(0.25f, 0, 0);
        // ���E��boxSize�͓���
        bool hitWallRight = Physics.CheckBox(origin, boxSize / 2, Quaternion.identity, layerMask);
        #endregion

        #region
        _isMoveRight = false;
        _isMoveLeft = false;

       if(!Input.GetButton("Left") || !Input.GetButton("Right"))
       {
            if (Input.GetButton("Left"))
            {
                _isMoveRight = false;
                _isMoveLeft = true;
            }
            else if (Input.GetButton("Right"))
            {
                _isMoveRight = true;
                _isMoveLeft = false;
            }
        }

        // �U��
        if (Input.GetButtonDown("Attack"))
            Attack(AttackType.OnGround);
        // �W�����v
        else if (Input.GetButtonDown("Jump") && isLanding)
        {
            _isJumping = true;
            _animator.SetTrigger("Jump");
        }
        // ���
        else if (Input.GetButtonDown("Avoid"))
        {
            _isAvoiding = true;
            _animator.SetTrigger("Avoid");
            _playerState = PlayerState.Avoid;
        }
        #endregion

        #region
        // ���E�ړ�
        float moveX = 0, moveY = 0;

        if (_isMoveLeft)
            moveX = -_horizontalVelocity * Time.deltaTime;
        else if (_isMoveRight)
            moveX = _horizontalVelocity * Time.deltaTime;
        // �d��
        if(_playerState < PlayerState.Avoid && !isLanding)
            moveY = Physics.gravity.y * Time.deltaTime;



        // �W�����v��
        if (_isJumping)
        {
            _currentjumpedTime += Time.deltaTime;
            moveY = Jump(_currentjumpedTime);

            // �W�����v�J�n��A�n�ʂ��痣�ꂽ�Ȃ�
            if (!isLanding)
                _isLeaveGround = true;
            // ��x��񂾌�A���n�����Ȃ�W�����v�I��
            if (_isLeaveGround && isLanding || _isJumpEnd)
                EndJump();
        }
        //
        if (_isAvoiding)
        {
            _currentAvoidedTime += Time.deltaTime;
            moveX = Avoid(_currentAvoidedTime);

            if (_isAvoidEnd)
                EndAvoid();
        }

        // �ړ�
        if (hitWallLeft)
            moveX = Mathf.Clamp(moveX, 0, 100);
        if (hitWallRight)
            moveX = Mathf.Clamp(moveX, - 100, 0);
        Vector3 move = new Vector3(moveX, moveY, 0);
        transform.Translate(move);
        #endregion

        //�@���G���Ԍo�ߏ���
        if (_isInvalid)
        InvalidTimeCount();
    }

    private bool CheckLanding()
    {
        Vector3 origin = transform.position + new Vector3(0, -0.25f, 0);
        Vector3 boxSize = new Vector3(0.1f, 1f, 1);
        float distance = 0.1f;
        int layerMask = 1 << 6;
        return Physics.BoxCast(origin, boxSize / 2, Vector3.down, Quaternion.identity, distance, layerMask);
    }

    #region �W�����v����
    private float Jump(float deltaTime)
    {
        if (deltaTime >= _maxJumpTime)
        {
            _isJumpEnd = true;
            return 0;
        }
            
        //float y = -20 * Mathf.Pow(deltaTime - _maxJumpTime, 2) + _maxJumpHeight;
        float y =  _initialVelocity +  Mathf.Lerp(0, _maxJumpHeight, deltaTime / _maxJumpTime);
        float moveY = y - _jumpedDistanceY;
        _jumpedDistanceY += moveY;

        return moveY < 0 ? 0 : moveY;
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

    #region �U������
    private void Attack(AttackType type)
    {
        _animator.SetInteger("Attack", (int)type);
    }

    // animator����Ăяo��
    private void EndAttack()
    {
        _animator.SetInteger("Attack", (int)AttackType.None);
    }

    public void AddDamage()
    {

    }

    #endregion

    #region �������
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

        return moveX < 0 ? 0 : moveX;
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
    public void RecieveDamage(int damage)
    {
        if (_isInvalid || _isInvincible)
            return;

        _HP -= damage;
        Debug.Log($"{damage}�_���[�W���󂯂�(�c��HP:{_HP})");
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