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
    }

    public void GetItem(Item item)
    {
        // �����A�C�e�������łɎ����Ă��邩�`�F�b�N
        for (int i = 0; i < _SLOT_SIZE; i++)
        {
            // �󂫂͈�U�X���[
            if (_itemSlots[i].itemType == Item.ItemType.None)
            {
                continue;
            }
            // ���łɎ����Ă���A�C�e���Ȃ�X�g�b�N
            else if (_itemSlots[i].itemType == item.itemType)
            {
                AddItem(i, item.quantity);
                return;
            }
        }

        // �����A�C�e���������Ă��Ȃ��ꍇ
        // �C���x���g���ɋ󂫂����邩�`�F�b�N
        for (int i = 0; i < _SLOT_SIZE; i++)
        {
            // ��Ȃ����
            if (_itemSlots[i].itemType == Item.ItemType.None)
            {
                SetItem(i, item);
                return;
            }
        }

        // �󂫂��Ȃ��ꍇ
        // �I�𒆂̃A�C�e�����̂Ă�
        RemoveItem(_currentIndex);
        SetItem(_currentIndex, item);
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
    }

    public void ItemChangeRight()
    {
        _currentIndex++;
        _currentIndex %= _SLOT_SIZE;
        Debug.Log($"�Z�b�g����Ă���A�C�e��:{_itemSlots[_currentIndex].itemType}");
    }

    public void ItemChangeLeft()
    {
        _currentIndex += _SLOT_SIZE - 1;
        _currentIndex %= _SLOT_SIZE;
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
}