using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour, IDamagable
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
}
