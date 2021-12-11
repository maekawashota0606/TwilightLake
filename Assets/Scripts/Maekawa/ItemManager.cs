using UnityEngine;

public class ItemManager : SingletonMonoBehaviour<ItemManager>
{
    [SerializeField]
    private Item _defaultItem = null;
    private const int _MAX_HOLD_NUM = 3;
    [SerializeField]
    private Item[] _inventory = new Item[_MAX_HOLD_NUM];
    //private Dictionary<Item.ItemType, int> _ItemsDict = new Dictionary<Item.ItemType, int>();
    // 選択中のアイテム
    private int _currentIndex = 0;

    private void Start()
    {
        // 空を示すアイテムを入れる
        for (int i = 0; i < _MAX_HOLD_NUM; i++)
            _inventory[i] = _defaultItem;
    }

    public void GetItem(Item item)
    {
        // 同名アイテムをすでに持っているかチェック
        for (int i = 0; i < _MAX_HOLD_NUM; i++)
        {
            // 空きは一旦スルー
            if (_inventory[i].itemType == Item.ItemType.None)
            {
                continue;
            }
            // すでに持っているアイテムならストック
            else if (_inventory[i].itemType == item.itemType)
            {
                AddItem(i, item.quantity);
                return;
            }
        }

        // 同名アイテムを持っていない場合
        // インベントリに空きがあるかチェック
        for (int i = 0; i < _MAX_HOLD_NUM; i++)
        {
            // 空なら入手
            if (_inventory[i].itemType == Item.ItemType.None)
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
        if (_inventory[_currentIndex].itemType == Item.ItemType.None)
            Debug.Log("アイテムがセットされていません");
        else
        {
            _inventory[_currentIndex].ItemEffect();
            _inventory[_currentIndex].quantity--;

            if (_inventory[_currentIndex].quantity < 1)
                _inventory[_currentIndex] = _defaultItem;
        }
    }

    public void ItemChangeRight()
    {
        _currentIndex++;
        _currentIndex %= _MAX_HOLD_NUM;
        Debug.Log(_currentIndex);
    }

    public void ItemChangeLeft()
    {
        _currentIndex += _MAX_HOLD_NUM - 1;
        _currentIndex %= _MAX_HOLD_NUM;
        Debug.Log(_currentIndex);
    }

    private void SetItem(int idx, Item item)
    {
        if (item.itemType == Item.ItemType.None)
            Debug.LogError("無効なアイテムです");
        else
            _inventory[idx] = item;
    }

    private void AddItem(int idx, int quantity)
    {
        _inventory[idx].quantity += quantity;
    }

    private void RemoveItem(int idx)
    {
        // 変な位置に置かれるので要修正
        _inventory[idx].gameObject.transform.position = Player.Instance.transform.position + Vector3.down;
        _inventory[idx] = _defaultItem;
    }
}