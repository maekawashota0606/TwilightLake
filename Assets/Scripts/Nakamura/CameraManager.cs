using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Vector3 cameraPos;
    Vector3 playerPos;
    private GameObject player;

    void Start()
    {
        cameraPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        cameraPos = transform.position;
        playerPos = player.transform.position;
        if(playerPos.x > -4.5)
        {
            cameraPos.x = playerPos.x;
            transform.position = cameraPos;
        }
        if(playerPos.y < 0)
        {
            cameraPos.y = (float)(playerPos.y + 2.5);
            transform.position = cameraPos;
        }else if(playerPos.y > 0)
        {
            cameraPos.y = (float)(playerPos.y + 2.5);
            transform.position = cameraPos;
        }
    }
}
