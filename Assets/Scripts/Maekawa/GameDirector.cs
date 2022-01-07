using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : SingletonMonoBehaviour<GameDirector>
{
    public Vector3 respownPoint = new Vector3();
    private int _karmaPoint = 0;
    private List<GameObject> _enemies = new List<GameObject>(100);
    public Difficulty difficulty = Difficulty.Easy;
    public GameState gameState = GameState.Active;

    public enum Difficulty
    {
        Easy,
        Normal,
        Hard
    }


    public enum GameState
    {
        None,
        Active,
        Idle,
        Dead
    }

    public void AddKarmaPoint(int addPoint)
    {
        _karmaPoint += addPoint;
        Debug.Log($"カルマポイント{addPoint}増加");
    }
    
    public void Respown()
    {
        Player.Instance.gameObject.transform.position = respownPoint;
        Player.Instance.Init();
    }
}
