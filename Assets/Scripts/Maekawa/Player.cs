using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rb = null;
    [SerializeField]
    private float _moveSpeed = 1;
    [SerializeField]
    private float _jumpPower = 10;
    private bool _isMoveRight = false;
    private bool _isMoveLeft = false;
    private bool _isMoveUp = false;
    private bool _isGround = false;
    private Ray _ray; // 飛ばすレイ
    private float _distance = 0.5f; // レイを飛ばす距離
    private RaycastHit _hit; // レイが何かに当たった時の情報
    private Vector3 _rayPosition; // レイを発射する位置

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _rayPosition = transform.position;
        _ray = new Ray(_rayPosition, Vector3.down);
        Debug.DrawRay(_ray.origin, _ray.direction * _distance, Color.red);

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
        else
        {
            _isMoveRight = false;
            _isMoveLeft = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && _isGround)
            _isMoveUp = true;
        else
            _isMoveUp = false;
    }

    private void FixedUpdate()
    {
        float speedX = 0;

        if (_isMoveLeft)
            speedX = -_moveSpeed;
        else if (_isMoveRight)
            speedX = _moveSpeed;

        _rb.velocity = new Vector3(speedX, _rb.velocity.y, 0);

        if (_isMoveUp)
            _rb.velocity = new Vector3(_rb.velocity.x, _jumpPower, 0);
    }

    private void JudgeGround()
    {
        //if()
    }
}
