using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField]
    private Enemy enemy;
    [SerializeField]
    private float flyspeed = 5.0f;

    private void FixedUpdate()
    {
        transform.localPosition = new Vector3(transform.localPosition.x + flyspeed * enemy.direction, 0, 0);
    }
}
