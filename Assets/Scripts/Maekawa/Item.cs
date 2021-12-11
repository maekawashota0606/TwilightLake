using UnityEngine;

public class Item : MonoBehaviour, IInspectible
{
    [NonReorderable]
    public int quantity = 1;
    [SerializeField]
    public ItemType itemType = ItemType.None;
    public enum ItemType
    {
        None = -1,
        Herb,
        Hoge,
        Foo,
        ItemCount
    }

    public virtual void ItemEffect()
    {
        Debug.LogError("オーバーライドされていません。");
    }

    public void Inspected()
    {
        if(ItemManager.Instance.GetItem(this))
            transform.position = new Vector3(999, 999);
    }
}
