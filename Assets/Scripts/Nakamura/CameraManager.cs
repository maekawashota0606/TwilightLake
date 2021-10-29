using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Vector3 cameraPos;
    Vector3 playerPos;

    void Start()
    {
        cameraPos = transform.position;
    }

    void Update()
    {
        cameraPos = transform.position;
        playerPos = GameObject.Find("Player").transform.position;
        if(playerPos.x > 0)
        {
            cameraPos.x = playerPos.x;
            transform.position = cameraPos;
        }
    }
}
