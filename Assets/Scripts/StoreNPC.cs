using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreNPC : MonoBehaviour
{
    [SerializeField] private GameObject store;
    [SerializeField] private EquipmentController equipmentController;
    [SerializeField] private Transform storePos;
    [SerializeField] private Transform inventoryPos;

    public void OnMouseOver()
    {
        GameManager.instance.MouseCursorChangeTalk();
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(null,Vector2.zero,CursorMode.ForceSoftware);
    }

    public void OnMouseDown()
    {
        store.SetActive(true);
        equipmentController.enabled = false;
        InventoryManager.instance.slotsParent.transform.parent.gameObject.SetActive(true);
        InventoryManager.instance.slotsParent.transform.position = storePos.position;
    }

    private void Update()
    {
        if(store.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                store.SetActive(false);
                equipmentController.enabled = true;
                InventoryManager.instance.slotsParent.transform.position = inventoryPos.position;
                InventoryManager.instance.slotsParent.transform.parent.gameObject.SetActive(false);
                if(StoreManager.instance.storeSlot.item != null)
                {
                    GameManager.instance.gold += (StoreManager.instance.storeSlot.item.price * StoreManager.instance.storeSlot.itemCount);
                    StoreManager.instance.storeSlot.ClearSlot();
                    StoreManager.instance.storeSlot.SetColor(0);
                    StoreManager.instance.isButton = false;
                }
            }
        }
    }
}
