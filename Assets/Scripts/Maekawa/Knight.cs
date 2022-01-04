using UnityEngine;
public class Knight : NPC
{
    public override void OnBranched()
    {
        // 薬草1つを消滅
        ItemManager.Instance.DeleteItem(Item.ItemType.Herb, 1);
    }

    public override bool PreBranching()
    {
        // 薬草を1つ以上持っているか
        Debug.Log(ItemManager.Instance.CheckHaveItem(Item.ItemType.Herb, 1));
        return 0 <= ItemManager.Instance.CheckHaveItem(Item.ItemType.Herb, 1);
    }
}
