using UnityEngine;

public class TestEnemy : Object, IDamagable
{
    private int _hp = 100;
    public SpriteRenderer sr = null;

    void IDamagable.AddDamage(int damage)
    {
        _hp -= damage;
        sr.color = new Color(Random.value, Random.value, Random.value);
        Debug.Log(_hp);
    }

    private void Start()
    {
        //GameDirector.Instance.enemies.Add(gameObject.GetComponent<TestEnemy>());
    }
    private void Update()
    {
        center = transform.position;
        if (_hp <= 0)
            Destroy(gameObject.transform.root.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            Player_.Instance.AddDamage(10, transform.position);
    }
}
