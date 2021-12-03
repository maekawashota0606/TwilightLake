using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField]
    private float HitPoint;

    public void AddDamage(int damage)
    {
        HitPoint -= damage;
    }
}
