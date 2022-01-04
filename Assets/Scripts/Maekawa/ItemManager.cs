using UnityEngine;

public class ItemManager : SingletonMonoBehaviour<ItemManager>
{
    [SerializeField]
    private Item _defaultItem = null;
    [SerializeField]
    private Item[] _itemSlots = new Item[_SLOT_SIZE];
    //private Dictionary<Item.ItemType, int> _ItemsDict = new Dictionary<Item.ItemType, int>();
    // �I�𒆂̃A�C�e��
    private int _currentIndex = 0;
    public const int _SLOT_SIZE = 3;

    private void Start()
    {
        // ��������A�C�e��������
        for (int i = 0; i < _SLOT_SIZE; i++)
            _itemSlots[i] = _defaultItem;

        HUDManager.Instance.DrawInventory(_itemSlots, _currentIndex);
    }

    public void GetItem(Item item)
    {
        // �����A�C�e�������łɎ����Ă��邩�`�F�b�N(���͖��Ȃ�)
        int targetNum = CheckHaveItem(item.itemType, 1);
        // �C���x���g���ɋ󂫂����邩�`�F�b�N
        int targetNum2 = CheckHaveItem(Item.ItemType.None, 0);

        // �����A�C�e���������Ă���ꍇ
        if (0 <= targetNum)
        {
            AddItem(targetNum, item.quantity);
            return;
        }
        // �C���x���g���ɋ󂫂�����ꍇ
        else if (0 <= targetNum)
        {
            SetItem(targetNum, item);
            return;
        }
        // ��2�ɊY�����Ȃ��ꍇ
        else
        {
            // �I�𒆂̃A�C�e�����̂Ă�
            RemoveItem(_currentIndex);
            SetItem(_currentIndex, item);
        }

        HUDManager.Instance.DrawInventory(_itemSlots, _currentIndex);
    }

    public void UseItem()
    {
        if (_itemSlots[_currentIndex].itemType == Item.ItemType.None)
            Debug.Log("�A�C�e�����Z�b�g����Ă��܂���");
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
        Debug.Log($"�Z�b�g����Ă���A�C�e��:{_itemSlots[_currentIndex].itemType}");
    }

    public void ItemChangeLeft()
    {
        _currentIndex += _SLOT_SIZE - 1;
        _currentIndex %= _SLOT_SIZE;
        HUDManager.Instance.DrawInventory(_itemSlots, _currentIndex);
        Debug.Log(_currentIndex);
        Debug.Log($"�Z�b�g����Ă���A�C�e��:{_itemSlots[_currentIndex].itemType}");
    }

    private void SetItem(int idx, Item item)
    {
        if (item.itemType == Item.ItemType.None)
            Debug.LogError("�����ȃA�C�e���ł�");
        else
        {
            _itemSlots[idx] = item;
            Debug.Log($"{_itemSlots[idx].itemType}��`{_itemSlots[idx].quantity}���肵��");
        }
    }

    private void AddItem(int idx, int quantity)
    {
        _itemSlots[idx].quantity += quantity;
        Debug.Log($"{_itemSlots[idx].itemType}��`{quantity}���肵��");
    }

    private void RemoveItem(int idx)
    {
        // TODO:�ςȈʒu�ɒu�����̂ŗv�C��
        Debug.Log($"{_itemSlots[idx].itemType}{_itemSlots[idx].quantity}���̂Ă�");
        _itemSlots[idx].gameObject.transform.position = Player.Instance.transform.position + (Vector3.down * 0.75f);
        _itemSlots[idx] = _defaultItem;
    }

    /// <summary>
    /// �w��̃A�C�e����K�v�������Ă��邩
    /// </summary>
    /// <param name="type">�w�肷��A�C�e��</param>
    /// <param name="requiredNum">�A�C�e���̕K�v��(�ȏ�)</param>
    /// <returns>����Ă���Ȃ炻�̃X���b�g�ԍ��A����Ă��Ȃ��Ȃ�-1</returns>
    public int CheckHaveItem(Item.ItemType type, int requiredNum)
    {
        for (int i = 0; i < _SLOT_SIZE; i++)
        {
            if (_itemSlots[i].itemType == type && requiredNum <= _itemSlots[i].quantity)
                return i;
        }
        // �w�肳�ꂽ�A�C�e�����Ȃ��Ȃ�
        return -1;
    }

    public void DeleteItem(Item.ItemType type, int deleteNum)
    {
        int targetNum = CheckHaveItem(type, deleteNum);

        // �w��̃A�C�e���������Ă���Ȃ�
        if (0 <= targetNum)
        {
            //�@�������ȉ��Ȃ�
            if (_itemSlots[targetNum].quantity <= deleteNum)
                _itemSlots[targetNum].quantity -= deleteNum;
            // ��������葽���v�����ꂽ�Ȃ�0
            else
                _itemSlots[targetNum].quantity = 0;

            // ��ɂȂ������������A�C�e�����Z�b�g
            if (_itemSlots[targetNum].quantity < 1)
                _itemSlots[targetNum] = _defaultItem;

            HUDManager.Instance.DrawInventory(_itemSlots, _currentIndex);
        }
        else
            Debug.LogError("�w�肳�ꂽ�A�C�e���������Ă��܂���");
    }
}