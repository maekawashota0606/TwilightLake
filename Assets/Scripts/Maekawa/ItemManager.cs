using UnityEngine;

public class ItemManager : SingletonMonoBehaviour<ItemManager>
{
    [SerializeField]
    private Item _defaultItem = null;
    [SerializeField]
    private Item[] _itemSlots = new Item[_SLOT_SIZE];
    //private Dictionary<Item.ItemType, int> _ItemsDict = new Dictionary<Item.ItemType, int>();
    // 選択中のアイテム
    private int _currentIndex = 0;
    public const int _SLOT_SIZE = 3;

    private void Start()
    {
        // 空を示すアイテムを入れる
        for (int i = 0; i < _SLOT_SIZE; i++)
            _itemSlots[i] = _defaultItem;
    }

    public void GetItem(Item item)
    {
        // 同名アイテムをすでに持っているかチェック
        for (int i = 0; i < _SLOT_SIZE; i++)
        {
            // 空きは一旦スルー
            if (_itemSlots[i].itemType == Item.ItemType.None)
            {
                continue;
            }
            // すでに持っているアイテムならストック
            else if (_itemSlots[i].itemType == item.itemType)
            {
                AddItem(i, item.quantity);
                return;
            }
        }

        // 同名アイテムを持っていない場合
        // インベントリに空きがあるかチェック
        for (int i = 0; i < _SLOT_SIZE; i++)
        {
            // 空なら入手
            if (_itemSlots[i].itemType == Item.ItemType.None)
            {
                SetItem(i, item);
                return;
            }
        }

        // 空きもない場合
        // 選択中のアイテムを捨てる
        RemoveItem(_currentIndex);
        SetItem(_currentIndex, item);
    }

    public void UseItem()
    {
        if (_itemSlots[_currentIndex].itemType == Item.ItemType.None)
            Debug.Log("アイテムがセットされていません");
        else
        {
            _itemSlots[_currentIndex].ItemEffect();
            _itemSlots[_currentIndex].quantity--;

            if (_itemSlots[_currentIndex].quantity < 1)
                _itemSlots[_currentIndex] = _defaultItem;
        }
    }

    public void ItemChangeRight()
    {
        _currentIndex++;
        _currentIndex %= _SLOT_SIZE;
        Debug.Log($"セットされているアイテム:{_itemSlots[_currentIndex].itemType}");
    }

    public void ItemChangeLeft()
    {
        _currentIndex += _SLOT_SIZE - 1;
        _currentIndex %= _SLOT_SIZE;
        Debug.Log($"セットされているアイテム:{_itemSlots[_currentIndex].itemType}");
    }

    private void SetItem(int idx, Item item)
    {
        if (item.itemType == Item.ItemType.None)
            Debug.LogError("無効なアイテムです");
        else
        {
            _itemSlots[idx] = item;
            Debug.Log($"{_itemSlots[idx].itemType}を`{_itemSlots[idx].quantity}個入手した");
        }
    }

    private void AddItem(int idx, int quantity)
    {
        _itemSlots[idx].quantity += quantity;
        Debug.Log($"{_itemSlots[idx].itemType}を`{quantity}個入手した");
    }

    private void RemoveItem(int idx)
    {
        // TODO:変な位置に置かれるので要修正
        Debug.Log($"{_itemSlots[idx].itemType}{_itemSlots[idx].quantity}個を捨てた");
        _itemSlots[idx].gameObject.transform.position = Player.Instance.transform.position + (Vector3.down * 0.75f);
        _itemSlots[idx] = _defaultItem;
    }
}