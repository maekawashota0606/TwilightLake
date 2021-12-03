using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Boomerang : MonoBehaviour
{
    [SerializeField,Header("�o�R����position")]
    private Vector3[] pos;
    [SerializeField]
    private int Damage;

    private void Start()
    {
        transform.DOLocalPath(pos, 2f, PathType.CatmullRom)
                .SetEase(Ease.InOutSine)
                .SetLookAt(0.01f, Vector3.forward)
                .SetOptions(true)
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                });
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player.Instance.AddDamage(Damage);
            //�����Ƀ_���[�W��^����֐������
            this.gameObject.SetActive(false);
        }
    }
}
