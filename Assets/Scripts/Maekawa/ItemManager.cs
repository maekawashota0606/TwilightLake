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

        HUDManager.Instance.DrawInventory(_itemSlots, _currentIndex);
    }

    public void GetItem(Item item)
    {
        // 同名アイテムをすでに持っているかチェック(個数は問わない)
        int targetNum = CheckHaveItem(item.itemType, 1);
        // インベントリに空きがあるかチェック
        int targetNum2 = CheckHaveItem(Item.ItemType.None, 0);

        // 同名アイテムを持っている場合
        if (0 <= targetNum)
        {
            AddItem(targetNum, item.quantity);
            return;
        }
        // インベントリに空きがある場合
        else if (0 <= targetNum)
        {
            SetItem(targetNum, item);
            return;
        }
        // ↑2つに該当しない場合
        else
        {
            // 選択中のアイテムを捨てる
            RemoveItem(_currentIndex);
            SetItem(_currentIndex, item);
        }

        HUDManager.Instance.DrawInventory(_itemSlots, _currentIndex);
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
        HUDManager.Instance.DrawInventory(_itemSlots, _currentIndex);
    }

    public void ItemChangeRight()
    {
        _currentIndex++;
        _currentIndex %= _SLOT_SIZE;
        HUDManager.Instance.DrawInventory(_itemSlots, _currentIndex);
        Debug.Log(_currentIndex);
        Debug.Log($"セットされているアイテム:{_itemSlots[_currentIndex].itemType}");
    }

    public void ItemChangeLeft()
    {
        _currentIndex += _SLOT_SIZE - 1;
        _currentIndex %= _SLOT_SIZE;
        HUDManager.Instance.DrawInventory(_itemSlots, _currentIndex);
        Debug.Log(_currentIndex);
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

    /// <summary>
    /// 指定のアイテムを必要数もっているか
    /// </summary>
    /// <param name="type">指定するアイテム</param>
    /// <param name="requiredNum">アイテムの必要数(以上)</param>
    /// <returns>足りているならそのスロット番号、足りていないなら-1</returns>
    public int CheckHaveItem(Item.ItemType type, int requiredNum)
    {
        for (int i = 0; i < _SLOT_SIZE; i++)
        {
            if (_itemSlots[i].itemType == type && requiredNum <= _itemSlots[i].quantity)
                return i;
        }
        // 指定されたアイテムがないなら
        return -1;
    }

    public void DeleteItem(Item.ItemType type, int deleteNum)
    {
        int targetNum = CheckHaveItem(type, deleteNum);

        // 指定のアイテムをもっているなら
        if (0 <= targetNum)
        {
            //　所持数以下なら
            if (_itemSlots[targetNum].quantity <= deleteNum)
                _itemSlots[targetNum].quantity -= deleteNum;
            // 所持数より多く要求されたなら0
            else
                _itemSlots[targetNum].quantity = 0;

            // 空になったら空を示すアイテムをセット
            if (_itemSlots[targetNum].quantity < 1)
                _itemSlots[targetNum] = _defaultItem;

            HUDManager.Instance.DrawInventory(_itemSlots, _currentIndex);
        }
        else
            Debug.LogError("指定されたアイテムを持っていません");
    }
}