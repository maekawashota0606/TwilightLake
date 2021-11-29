using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : SingletonMonoBehaviour<GameDirector>
{
    private Player _player = null;
    public List<TestEnemy> enemies = new List<TestEnemy>(); 
    MyPhysics.Object obj1 = new MyPhysics.Object();
    MyPhysics.Object obj2 = new MyPhysics.Object();

    private void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
    void Update()
    {
        obj1 = new MyPhysics.Object(_player.center, _player.height, _player.width);
        obj2 = new MyPhysics.Object(enemies[0].center, enemies[0].height, enemies[0].width);

        if (MyPhysics.IsBoxHit(obj1, obj2))
            _player.RecieveDamage(10);
    }
}
