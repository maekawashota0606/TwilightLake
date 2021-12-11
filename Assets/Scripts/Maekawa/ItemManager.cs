using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : SingletonMonoBehaviour<ItemManager>
{
    [SerializeField]
    private Item _defaultItem = null;
    private const int _MAX_HOLD_NUM = 3;
    [SerializeField]
    private Item[] _inventory = new Item[_MAX_HOLD_NUM];
    //private Dictionary<Item.ItemType, int> _ItemsDict = new Dictionary<Item.ItemType, int>();
    private int _currentIndex = 0;// ��

    private void Start()
    {
        // ��������A�C�e��������
        for (int i = 0; i < _MAX_HOLD_NUM; i++)
            _inventory[i] = _defaultItem;
    }

    public void GetItem(Item item)
    {
        // �����A�C�e�������łɎ����Ă��邩�`�F�b�N
        for (int i = 0; i < _MAX_HOLD_NUM; i++)
        {
            // �󂫂͈�U�X���[
            if (_inventory[i].itemType == Item.ItemType.None)
            {
                continue;
            }
            // ���łɎ����Ă���A�C�e���Ȃ�X�g�b�N
            else if (_inventory[i].itemType == item.itemType)
            {
                AddItem(i, item.quantity);
                return;
            }
        }

        // �����A�C�e���������Ă��Ȃ��ꍇ
        // �C���x���g���ɋ󂫂����邩�`�F�b�N
        for (int i = 0; i < _MAX_HOLD_NUM; i++)
        {
            // ��Ȃ����
            if (_inventory[i].itemType == Item.ItemType.None)
            {
                SetItem(i, item);
                return;
            }
        }

        // �󂫂��Ȃ��ꍇ
        // �I�𒆂̃A�C�e�����̂Ă�
        RemoveItem(_currentIndex);
        SetItem(_currentIndex, item);

        // TODO:�����ݒ肷��
        // �����擾�����ꍇ�ɏ���𒴂���Ȃ����܂Ŏ擾���A�c�����ɒu��
    }

    private void UseItem(int idx)
    {
        //if (idx < 0 || _inventory.Count < idx)
        //{
        //    Debug.LogError("�w�肳�ꂽ�C���f�b�N�X�͑��݂��܂���");
        //    return;
        //}

        //if(0 < _inventory[idx].quantity)
        //{
        //    _inventory[idx].quantity--;
        //    _inventory[idx].ItemEffect();
        //}

        //if (_inventory[idx].quantity < 1)
        //    _inventory.RemoveAt(idx);
    }

    private void SetItem(int idx, Item item)
    {
        if (item.itemType == Item.ItemType.None)
            Debug.LogError("�����ȃA�C�e���ł�");
        else
            _inventory[idx] = item;
    }

    private void AddItem(int idx, int quantity)
    {
        _inventory[idx].quantity += quantity;
    }

    private void RemoveItem(int idx)
    {
        // �ςȈʒu�ɒu�����̂ŗv�C��
        _inventory[idx].gameObject.transform.position = Player.Instance.transform.position + Vector3.down;
        _inventory[idx] = _defaultItem;
    }
}
