using UnityEngine;

public class Item : MonoBehaviour, IInspectible
{
    [SerializeField, Tooltip("��")]
    public int quantity = 1;
    public ItemType itemType = ItemType.None;
    public enum ItemType
    {
        None = -1,
        Herb,
        Hoge,
        Foo,
        Fuga,
        ItemCount
    }

    public virtual void ItemEffect()
    {
        Debug.LogError("�I�[�o�[���C�h����Ă��܂���B");
    }

    public void Inspected()
    {
        ItemManager.Instance.GetItem(this);
        transform.position = new Vector3(999, 999);
    }
}
