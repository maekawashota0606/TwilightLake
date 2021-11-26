using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Timer timer;

    [SerializeField]
    private float _moveSpeed = 1.0f;//毎秒動かす距離

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            float moveSpeed = _moveSpeed * Time.unscaledDeltaTime;
            this.transform.Translate(-moveSpeed, 0.0f, 0.0f);
        }

        if (Input.GetKey(KeyCode.D))
        {
            float moveSpeed = _moveSpeed * Time.unscaledDeltaTime;
            this.transform.Translate(moveSpeed, 0.0f, 0.0f);
        }
    }

    void FixedUpdate()
    {
        
    }
}
