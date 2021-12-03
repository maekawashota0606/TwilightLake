using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField,Header("�i�C�t�̔�s���x")]
    private float flyspeed = 5.0f;
    [SerializeField, Header("�^����_���[�W")]
    private int Damage;
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
        //Layer 3��Ground Layer 6��Player
        if (collision.gameObject.layer == 3 || collision.gameObject.layer == 6)
        {
            if(collision.gameObject.layer==6)
            {
                Player.Instance.AddDamage(Damage);
            }
            Destroy(this.gameObject);
        }
    }
}
