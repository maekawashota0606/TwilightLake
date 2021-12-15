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
    [SerializeField]
    private Sprite[] _itemImages = new Sprite[(int)Item.ItemType.ItemCount];

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void DrawHPGauge(int HP, int maxHP)
    {
        _HPGauge.fillAmount = (float)HP / maxHP;
    }

    public void DrawInventory(Item[] items)
    {
        for(int i = 0; i < ItemManager._SLOT_SIZE; i++)
        {
            _itemSlots[i].sprite = _itemImages[(int)items[i].itemType];
        }
    }
}
