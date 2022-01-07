using UnityEngine;

public class Herb : Item
{
    [SerializeField, Tooltip("�񕜗�(%)")]
    private float _healRatio = 0.30f;

    public override void ItemEffect()
    {
        Debug.Log("��");
        Player.Instance.Heal(_healRatio);
    }
}
