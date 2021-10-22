using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckZone : MonoBehaviour
{
    [SerializeField]
    private BoxCollider BC;
    [SerializeField]
    private int Num_of_Player;
    private Enemy enemy;
    private void Start()
    {
        //Enemy��Script���擾
        enemy = this.gameObject.transform.parent.gameObject.GetComponent<Enemy>();
        //Boxcollider�擾
        BC = this.gameObject.GetComponent<BoxCollider>();
        //Box�R���C�_�[
        BC.center = new Vector3(enemy.direction*Num_of_Player / 2, BC.center.y, BC.center.z);
        BC.size = new Vector3(Num_of_Player,BC.size.y,BC.size.z);
    }

}
