using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Vector3 cameraPos;
    // Start is called before the first frame update
    void Start()
    {
        cameraPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (PlayerTest.pos.x >= 2)
        {
            cameraPos.x = PlayerTest.pos.x;
            transform.position = cameraPos;
        }*/
    }
}
