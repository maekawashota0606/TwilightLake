using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : SingletonMonoBehaviour<HUDManager>
{
    [SerializeField]
    private Image _HPGauge = null;
    [SerializeField]
    private Image[] _itemSlots = new Image[ItemManager._SLOT_SIZE];
    private Sprite[] _itemImages = new Sprite[(int)Item.ItemType.ItemCount];

    private void Start()
    {
        for(int i = 0; i < (int)Item.ItemType.ItemCount; i++)
            _itemImages[i] = Resources.Load<Sprite>("UI/Items/Item_" + i.ToString());
    }

    public void DrawHPGauge(int HP, int maxHP)
    {
        _HPGauge.fillAmount = (float)HP / maxHP;
    }

    public void DrawInventory(Item[] items, int currntindex)
    {
        for(int i = 0; i < ItemManager._SLOT_SIZE; i++)
        {
            int idx = (currntindex + i) % ItemManager._SLOT_SIZE;

            if (items[idx].itemType == Item.ItemType.None)
                _itemSlots[i].color = Color.clear;
            else
            {
                _itemSlots[i].color = Color.white;
                _itemSlots[i].sprite = _itemImages[(int)items[idx].itemType];
            }
        }
    }
}
