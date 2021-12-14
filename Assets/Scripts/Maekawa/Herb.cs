using UnityEngine;

public class Herb : Item
{
    [SerializeField, Tooltip("‰ñ•œ—Ê(%)")]
    private float _healRatio = 0.30f;

    public override void ItemEffect()
    {
        Debug.Log("‰ñ•œ");
        Player.Instance.Heal(_healRatio);
    }
}
