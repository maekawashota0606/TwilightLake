using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    #region private Variables
    //-----private-----//
    [SerializeField,Header("移動速度")]
    private float M_Speed;

    private int HP;
    private float distance; //敵とプレイヤーの距離を保存する


    /// <summary>
    /// <para>向き</para>
    /// <para>-1か1 にしかならないように設定する</para>
    /// </summary>
    private int direction;
    #endregion

    #region Public Variables
    //------public------//
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange; //プレーヤーが範囲内にあるかどうかを確認します
    [Header("ここから下は動かさないでください")]
    public GameObject hotZone;
    public GameObject triggerArea;
    public Transform leftLimit;
    public Transform rightLimit;
    #endregion

    private void Awake()
    {
        SelectTarget();
    }

    private void FixedUpdate()
    {
        Move();

        if (!InsideOfLimits()&&!inRange)
        {
            SelectTarget();
        }
        if(inRange)
        {
            //攻撃処理
        }

    }

    void AttackMode()
    {
        //敵とプレイヤーの距離を代入
        distance = Vector2.Distance(transform.position, target.position);

        //ray castで直線上にPlayerいるかどうか確認する
        //確認出来たら Attack.csのN_Attackを開始する(これに関してはEnemyAIとSprictを統合するかも)
        //Cooldownをはさむ
        //FixedUpdate中、inRangeがtrueの限り、攻撃し続ける。
    }

    void Move()
    {
        //ターゲット位置を一時変数に格納し、敵はX軸のみで移動させる
        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y,transform.position.z);
        //プレイヤーのXとY位置はYと等しい
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, M_Speed * Time.deltaTime);
    }


    /// <summary>
    /// 方向のリミットを自動で選択する
    /// </summary>
    public void SelectTarget()
    {
        //左リミットまでの距離を計算
        float distanceToLeft = Vector3.Distance(transform.position, leftLimit.position);
        //右リミットまでの距離を計算
        float distanceToRight = Vector3.Distance(transform.position, rightLimit.position);

        //リミットを比較し、距離が遠いほうを移動ターゲットとする
        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }
        Flip();
    }
    /// <summary>
    /// 現在行動範囲の中に入っているのかをチェック
    /// </summary>
    /// <returns>入っていればtrue 入ってなければfalse</returns>
    private bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    /*
    IEnumerator De()
    {
        while (true)
        {
            //Flip();
            yield return new WaitForSeconds(1);
        }

    }*/

    /// <summary>
    /// 回転が必要かどうかを判断し
    /// 回転させる
    /// </summary>
    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            rotation.y = 0;
            direction = -1;
        }
        else
        {
            rotation.y = 180.0f;
            direction = 1;
        }
        transform.eulerAngles = rotation;
    }

    public int getDirection()
    {
        return direction;
    }
}
