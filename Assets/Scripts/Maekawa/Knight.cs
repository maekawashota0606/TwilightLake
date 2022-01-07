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
            // –ò‘1‚Â‚ğÁ–Å
            ItemManager.Instance.DeleteItem(Item.ItemType.Herb, 1);
        }
    }

    public override bool PreBranching()
    {
        // –ò‘‚ğ1‚ÂˆÈã‚Á‚Ä‚¢‚é‚©
        return 0 <= ItemManager.Instance.CheckHaveItem(Item.ItemType.Herb, 1);
    }
}
