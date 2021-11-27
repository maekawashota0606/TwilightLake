using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : SingletonMonoBehaviour<GameDirector>
{
    private Player _player = null;
    private MyPhysics.BoxObject _obj1 = new MyPhysics.BoxObject();
    private MyPhysics.BoxObject _obj2 = new MyPhysics.BoxObject();
    [HideInInspector]
    public List<TestEnemy> enemies = new List<TestEnemy>();
    [HideInInspector]
    public List<Ground> grounds = new List<Ground>();

    private void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        GameObject[] grd = GameObject.FindGameObjectsWithTag("Ground");
        for (int i = 0; i < grd.Length; i++)
            grounds.Add(grd[i].GetComponent<Ground>());
    }

    private void Update()
    {
        if (enemies.Count <= 0)
            return;

        _obj1 = new MyPhysics.BoxObject(_player.center, _player.height, _player.width);
        _obj2 = new MyPhysics.BoxObject(enemies[0].center, enemies[0].height, enemies[0].width);

        //if (MyPhysics.IsBoxHit(_obj1, _obj2))
            //_player.RecieveDamage(10);
    }
}
