using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField,Header("ナイフの飛行速度")]
    private float flyspeed = 5.0f;

    private void FixedUpdate()
    {
        transform.localPosition = new Vector3(transform.localPosition.x + flyspeed*-1, 0, 0);
    }
}
