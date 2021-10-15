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
    private float _gravity = 1;
    private float _jumpPower = 10;
    private bool _isMoveRight = false;
    private bool _isMoveLeft = false;
    private bool _isMoveUp = false;
    private bool _isGrounding = false;
    private const string _GROUND_TAG = "Ground";

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _isGrounding = JudgeGround();

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

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounding)
            _isMoveUp = true;
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
        {
            _rb.velocity = new Vector3(_rb.velocity.x, _jumpPower, 0);
            _isMoveUp = false;
        }
    }

    private bool JudgeGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        float distance = 0.5f;
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);

        bool isGround = false;
        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.collider.CompareTag(_GROUND_TAG))
            {
                Debug.Log("grounding");
                isGround = true;
            }
        }

        //bool isGround = false;
        //if (Physics.BoxCast(transform.position, Vector3.one * transform.lossyScale.x, transform.up, out hit, transform.rotation))
        //{
        //    Gizmos.DrawRay(transform.position, Vector3.down * hit.distance);
        //    if (hit.collider.CompareTag(_GROUND_TAG))
        //    {
        //        Debug.Log("grounding");
        //        isGround = true;
        //    }
        //}


        return isGround;
    }
}
