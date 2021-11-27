using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Boomerang : MonoBehaviour
{
    [SerializeField,Header("Œo—R‚·‚éposition")]
    private Vector3[] pos;

    private void Start()
    {
        transform.DOLocalPath(pos, 2f, PathType.CatmullRom)
				.SetEase(Ease.InOutSine)
				.SetLookAt(0.01f,Vector3.forward)
				.SetOptions(true);
    }
}
