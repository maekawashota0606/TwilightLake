using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 出入りすると複数回呼ばれかねないので
        // 敵に無敵時間を作るか重複させないようにする
        IDamageble damagable = other.GetComponent<IDamageble>();

        if (damagable != null)
            damagable.AddDamage(10);
    }
}
