using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaCheck : MonoBehaviour
{
    /// <summary>
    /// Enemy–{‘Ì
    /// </summary>
    private EnemyAI enemyParent;

    private void Awake()
    {
        enemyParent = GetComponentInParent<EnemyAI>();
    }
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            enemyParent.target = collider.transform;
            enemyParent.inRange = true;
            enemyParent.isAttackMode = true;
            enemyParent.hotZone.SetActive(true);
        }
    }
}
