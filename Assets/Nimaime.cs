using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nimaime : MonoBehaviour
{
    private GameObject player;
    private GameObject camera;
    private Vector3 startPlayerOffset;
    private Vector3 startCameraPos;
    [SerializeField] private float RATE;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectFindWithTag("Player");
        startPlayerOffset = player.transform.position;
        startCameraPos = GameObject.FindGameObjectFindWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
