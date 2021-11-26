using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nimaime : MonoBehaviour
{
    //private GameObject player;
    private GameObject camera;
    //private Vector3 startPlayerOffset;
    private Vector3 startCameraPos;
    private Vector3 start2maimePos;
    [SerializeField] private float RATE;
    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //startPlayerOffset = player.transform.position;
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        startCameraPos = camera.transform.position;
        start2maimePos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 plus = (camera.transform.position - startCameraPos) * RATE;
        this.transform.position = start2maimePos + plus;
    }
}
