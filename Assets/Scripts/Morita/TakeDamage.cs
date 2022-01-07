using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour, IDamageble
{
    private EnemyAI enemyAI;
    private Enemy enemy;
    private void Awake()
    {
        enemy = transform.GetComponentInParent<Enemy>();
        enemyAI = transform.GetComponentInParent<EnemyAI>();
    }

    public void AddDamage(int damage)
    {
        enemyAI.StartCoroutine("Hurt");
        enemy.HitPoint -= damage;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            Player.Instance.AddDamage(10, transform.position);
    }
}
