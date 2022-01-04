using UnityEngine;
public class Knight : NPC
{
    public override void OnBranched()
    {
        // –ò‘1‚Â‚ğÁ–Å
        ItemManager.Instance.DeleteItem(Item.ItemType.Herb, 1);
    }

    public override bool PreBranching()
    {
        // –ò‘‚ğ1‚ÂˆÈã‚Á‚Ä‚¢‚é‚©
        Debug.Log(ItemManager.Instance.CheckHaveItem(Item.ItemType.Herb, 1));
        return 0 <= ItemManager.Instance.CheckHaveItem(Item.ItemType.Herb, 1);
    }
}
