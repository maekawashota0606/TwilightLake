public class Knight : NPC
{
    public override void ChangeState(int state)
    {
        base.ChangeState(state);

        if (eventState == eventFinishState[eventIndex])
            GameDirector.Instance.AddKarmaPoint(addKarmaPoint[eventIndex]);
    }

    public override void OnBranched(bool answer)
    {
        if(answer)
        {
            // 薬草1つを消滅
            ItemManager.Instance.DeleteItem(Item.ItemType.Herb, 1);
        }
    }

    public override bool PreBranching()
    {
        // 薬草を1つ以上持っているか
        return 0 <= ItemManager.Instance.CheckHaveItem(Item.ItemType.Herb, 1);
    }
}
