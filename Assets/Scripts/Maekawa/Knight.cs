using UnityEngine;
public class Knight : NPC
{
    public override void OnBranched()
    {
        // ��1������
        ItemManager.Instance.DeleteItem(Item.ItemType.Herb, 1);
    }

    public override bool PreBranching()
    {
        // �򑐂�1�ȏ㎝���Ă��邩
        Debug.Log(ItemManager.Instance.CheckHaveItem(Item.ItemType.Herb, 1));
        return 0 <= ItemManager.Instance.CheckHaveItem(Item.ItemType.Herb, 1);
    }
}
