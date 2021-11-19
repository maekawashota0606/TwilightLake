using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Timer timer;

    Rigidbody rb;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("イベント発生");
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //X軸の移動量を受け取る
        float hrtl = Input.GetAxis("Horizontal");
        //移動量を加えるPOWER
        rb.velocity = new Vector2(hrtl, 0);
        
            Debug.Log(hrtl);

    }

    void FixedUpdate()
    {
        
    }
}
