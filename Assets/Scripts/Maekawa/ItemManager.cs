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
            // ���łɎ����Ă���A�C�e���Ȃ�ǉ�
            if (items != null && items.itemType == item.itemType)
            {
                items.quantity += item.quantity;
                isContain = true;
                break;
            }
        }

        if(!isContain)
        {
            // �A�C�e�������g���}�b�N�X�Ȃ�
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

        // TODO:�����ݒ肷��
        // �����擾�����ꍇ�ɏ���𒴂���Ȃ����܂Ŏ擾���A�c�����ɒu��

        return isGot;
    }

    private void UpdateItem()
    {

    }

    private void UseItem(int idx)
    {
        if (idx < 0 || _playersItems.Count < idx)
        {
            Debug.LogError("�w�肳�ꂽ�C���f�b�N�X�͑��݂��܂���");
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
        // �ςȈʒu�ɒu����邩���H
        _playersItems[idx].gameObject.transform.position = Player.Instance.transform.position;
    }
}
