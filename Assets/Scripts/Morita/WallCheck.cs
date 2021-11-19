using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    private EnemyAI enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<EnemyAI>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Wall")
        {

        }
    }
}
