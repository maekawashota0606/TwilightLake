using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : SingletonMonoBehaviour<ItemManager>
{
    [SerializeField]
    private Item _defaultItem = null;
    private const int _MAX_HOLD_NUM = 3;
    private List<Item> _playersItems = new List<Item>();
    //private Dictionary<Item.ItemType, int> _ItemsDict = new Dictionary<Item.ItemType, int>();
    private int _currentIndex = 0;
    private void Start()
    {

    }

    public bool GetItem(Item item)
    {
        bool isGot = true;

        bool isContain = false;
        foreach(Item items in _playersItems)
        {
            // すでに持っているアイテムなら追加
            if (items != null && items.itemType == item.itemType)
            {
                items.quantity += item.quantity;
                isContain = true;
                break;
            }
        }

        if(!isContain)
        {
            // アイテム所持枠がマックスなら
            if(_MAX_HOLD_NUM <= _playersItems.Count)
            {
                RemoveItem(_currentIndex);
                _playersItems.Insert(_currentIndex, item);
            }
            else
            {
                _playersItems.Add(item);
            }
        }

        // TODO:上限を設定する
        // 複数取得した場合に上限を超えるなら上限まで取得し、残りを場に置く

        return isGot;
    }

    private void UpdateItem()
    {

    }

    private void UseItem(int idx)
    {
        if (idx < 0 || _playersItems.Count < idx)
        {
            Debug.LogError("指定されたインデックスは存在しません");
            return;
        }

        if(0 < _playersItems[idx].quantity)
        {
            _playersItems[idx].quantity--;
            _playersItems[idx].ItemEffect();
        }

        if (_playersItems[idx].quantity < 1)
            _playersItems.RemoveAt(idx);
    }

    private void RemoveItem(int idx)
    {
        _playersItems.RemoveAt(idx);
        // 変な位置に置かれるかも？
        _playersItems[idx].gameObject.transform.position = Player.Instance.transform.position;
    }
}
