using UnityEngine;

public class ItemManager : SingletonMonoBehaviour<ItemManager>
{
    [SerializeField]
    private Item _defaultItem = null;
    private const int _MAX_HOLD_NUM = 3;
    [SerializeField]
    private Item[] _inventory = new Item[_MAX_HOLD_NUM];
    //private Dictionary<Item.ItemType, int> _ItemsDict = new Dictionary<Item.ItemType, int>();
    // �I�𒆂̃A�C�e��
    private int _currentIndex = 0;

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
    }

    public void UseItem()
    {
        if (_inventory[_currentIndex].itemType == Item.ItemType.None)
            Debug.Log("�A�C�e�����Z�b�g����Ă��܂���");
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