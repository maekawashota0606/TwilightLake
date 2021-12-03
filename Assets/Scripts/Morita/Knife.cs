using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField,Header("ナイフの飛行速度")]
    private float flyspeed = 5.0f;
    private EnemyAI enemyAI;
    private void Start()
    {
        enemyAI = this.GetComponentInParent<EnemyAI>();
    }
    private void FixedUpdate()
    {
        transform.position += new Vector3(flyspeed * enemyAI.direction, 0, 0);
    }
    private void OnTriggerEnter(Collider collision)
    {
        //Layer 3はGround Layer 6はPlayer
        if (collision.gameObject.layer == 3 || collision.gameObject.layer == 6)
        {
            Destroy(this.gameObject);
        }
    }
}
