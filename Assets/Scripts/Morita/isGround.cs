using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isGround : MonoBehaviour
{
    private EnemyAI enemy;
    //[SerializeField]
    //private LayerMask groundLayer;
    private void Start()
    {
        enemy = GetComponentInParent<EnemyAI>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            enemy.isGround = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Ground")
        {
            enemy.isGround = false;
        }
    }
}
