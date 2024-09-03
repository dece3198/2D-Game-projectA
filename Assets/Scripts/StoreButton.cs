using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreButton : MonoBehaviour
{
    public Item item;

    public void ShopButton()
    {
        if(StoreManager.instance.storeSlot.item == item)
        {
            StoreManager.instance.storeSlot.PlusCount(1);
            return;
        }
        StoreManager.instance.storeSlot.AddItem(item);
        StoreManager.instance.storeSlot.SetColor(1);
        StoreManager.instance.isButton = true;
    }


}
