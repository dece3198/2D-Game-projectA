using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreButton : MonoBehaviour
{
    public Item item;
    [SerializeField] private int price;

    public void ShopButton()
    {
        if(GameManager.instance.gold < price)
        {
            return;
        }

        if(StoreManager.instance.storeSlot.item == item)
        {
            StoreManager.instance.storeSlot.PlusCount(1);
            GameManager.instance.gold -= price;
            return;
        }
        StoreManager.instance.storeSlot.AddItem(item);
        StoreManager.instance.storeSlot.SetColor(1);
        StoreManager.instance.isButton = true;
        GameManager.instance.gold -= price;
    }
}
