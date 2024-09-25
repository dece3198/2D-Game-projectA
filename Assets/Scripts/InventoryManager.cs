using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public GameObject slotsParent;
    public Slot[] slots;
    public Dictionary<int, Item> itemDic = new Dictionary<int, Item>();
    public Dictionary<Item, int> itemNumberDic = new Dictionary<Item, int>();
    [SerializeField] private Item[] items;

    private void Awake()
    {
        instance = this;

        for(int i = 0; i < items.Length; i++)
        {
            itemDic.Add(i, items[i]);
            itemNumberDic.Add(items[i], i);
        }
    }

    private void Start()
    {
        slots = slotsParent.GetComponentsInChildren<Slot>();
        
        if (DataManager.instance.curData.isStart)
        {
            for (int i = 0; i < 3; i++)
            {
                slots[i].AddItem(items[i]);
            }
        }

        GameManager.instance.LoadData();
    }

    public void AcquireItem(Item _item, int count = 1)
    {
        if(_item.itemType != ItemType.Equipment)
        {
            for(int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item == _item)
                    {
                        slots[i].PlusCount(count);
                        return;
                    }
                }
            }
        }

        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                if (slots[i].isSlotActiveSelf)
                {
                    slots[i].AddItem(_item, count);
                    return;
                }
            }
        }
    }
}
