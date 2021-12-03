using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimeZone
{
    moring,
    noon,
    night
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform StartPosition;

    //�Q�[���̃|�C���g
    private static int point;
    //�Z�b�^�[�ƃQ�b�^�[
    public static int Point
    {
        get
        {
            return point;
        }
        set
        {
            point = value;
            switch(point)
            {
                case 0:
                    timezone = TimeZone.moring;
                    break;
                case 10:
                    timezone = TimeZone.noon;
                    break;
                case 25:
                    timezone = TimeZone.night;
                    break;
            }
        }
    }

    //�R���e�j���[������W���L��
    private static Vector3 C_position;

    public static TimeZone timezone;

    private void Start()
    {
        C_position = StartPosition.position;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Point++;
            Debug.Log(Point);
        }
    }

    public static void respawn(Transform index)
    {
        index.position = C_position;
    }
    public static void UpdatePosition(Vector3 index)
    {
        C_position = index;
    }
}
