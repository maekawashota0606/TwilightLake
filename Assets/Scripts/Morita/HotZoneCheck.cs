using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotZoneCheck : MonoBehaviour
{
    private EnemyAI enemyParent;
    private bool inRange;

    private void Awake()
    {
        enemyParent = GetComponentInParent<EnemyAI>();
    }
    private void Update()
    {
        if(inRange)
        {
            enemyParent.Flip(); 
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        inRange = false;
        gameObject.SetActive(false);
        enemyParent.triggerArea.SetActive(true);
        enemyParent.inRange = false;
        enemyParent.isAttackMode = false;
        enemyParent.SelectTarget();
    }
}
