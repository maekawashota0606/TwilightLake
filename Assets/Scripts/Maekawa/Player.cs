using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 1;
    [SerializeField]
    private float _gravity = 1;
    private Rigidbody _rb = null;
    private float _jumpPower = 10;
    private bool _isMoveRight = false;
    private bool _isMoveLeft = false;
    private bool _isMoveUp = false;
    private bool _isLanding = false;
    private const string _GROUND_TAG = "Ground";

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _isLanding = JudgeLanding();

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

        if (Input.GetKeyDown(KeyCode.Space) && _isLanding)
            _isMoveUp = true;
    }

    private void FixedUpdate()
    {
        float speedX = 0;
        float speedY = 0;

        if (_isMoveLeft)
            speedX = -_moveSpeed;
        else if (_isMoveRight)
            speedX = _moveSpeed;

        speedY = _isLanding ? 0 : -_gravity;
        _rb.velocity = new Vector3(speedX, speedY, 0);
        
        if (_isMoveUp)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, _jumpPower, 0);
            _isMoveUp = false;
        }
    }

    private bool JudgeLanding()
    {
        bool isHitGround = false;

        // 雑にオフセットを指定
        Vector3 StartPos = transform.position + new Vector3(-0.37f, 0.1f, 0);
        Vector3 rayDirection = Vector3.down;
        RaycastHit hit;
        Vector3 boxSize = new Vector3(1.0f, 0.5f, 1);
        float distance = 1f;
        //　Cubeのレイを飛ばしターゲットと接触しているか判定
        if (Physics.BoxCast(StartPos, boxSize / 2, rayDirection, out hit, Quaternion.identity, distance))
        {
            if (hit.transform.gameObject.CompareTag(_GROUND_TAG))
            {
                isHitGround = true;
                Debug.Log("hit");
            }
        }

        return isHitGround;
    }

    #region forDebug
    private void OnDrawGizmos()
    {
        // 雑にオフセットを指定
        Vector3 StartPos = transform.position + new Vector3(-0.37f, 0.1f, 0);
        Vector3 rayDirection = Vector3.down;
        RaycastHit hit;
        float distance = 1f;
        Vector3 boxSize = new Vector3(0.5f, 0.25f, 1);
        //　Cubeのレイを飛ばしターゲットと接触しているか判定
        if (Physics.BoxCast(StartPos, boxSize, rayDirection, out hit, Quaternion.identity, distance))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(StartPos + (rayDirection * distance), boxSize * 2);
        }
    }
    #endregion
}
