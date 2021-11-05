using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // �o���肷��ƕ�����Ă΂ꂩ�˂Ȃ��̂�
        // �G�ɖ��G���Ԃ���邩�d�������Ȃ��悤�ɂ���
        IDamagable damagable = other.GetComponent<IDamagable>();

        if (damagable != null)
            damagable.AddDamage(10);
    }
}
