using UnityEngine;

public class TestEnemy : MonoBehaviour, IDamagable
{
    private int _hp = 100;
    public SpriteRenderer sr = null;

    void IDamagable.AddDamage(int damage)
    {
        _hp -= damage;
        sr.color = new Color(Random.value, Random.value, Random.value);
        Debug.Log(_hp);
    }

    private void Update()
    {
        if (_hp <= 0)
            Destroy(this.gameObject);
    }
}
