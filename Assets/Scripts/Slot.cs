using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler, IPointerDownHandler
{
    public Item item;
    public Image itemImage;
    public int itemCount;
    [SerializeField] private TextMeshProUGUI countText;
    public bool isSlotActiveSelf;
    public EquipmentType slotType;

    public void AddItem(Item _item, int count = 1)
    {
        item = _item;
        itemCount = count;
        itemImage.sprite = item.itemImage;
        if(countText != null)
        {
            if (item.itemType != ItemType.Equipment)
            {
                countText.gameObject.SetActive(true);
                countText.text = itemCount.ToString();
            }
            else
            {
                countText.gameObject.SetActive(false);
            }
        }
        SetColor(1);
    }

    public void PlusCount(int count)
    {
        itemCount += count;
        countText.text = itemCount.ToString();
        if(itemCount <= 0)
        {
            ClearSlot();
        }
    }

    public void SetColor(float alpha)
    {
        Color color = itemImage.color;
        color.a = alpha;
        itemImage.color = color;
    }

    public void ClearSlot()
    {
        item = null;
        itemImage.sprite = null;
        itemCount = 0;
        SetColor(0);
        if (countText != null)
        {
            countText.gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            if(isSlotActiveSelf)
            {
                if (eventData.button == PointerEventData.InputButton.Left)
                {
                    DragSlot.instance.dragSlot = this;
                    DragSlot.instance.DragSetImage(itemImage);
                    DragSlot.instance.transform.position = eventData.position;
                }
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            if(isSlotActiveSelf)
            {
                if (eventData.button == PointerEventData.InputButton.Left)
                {
                    DragSlot.instance.transform.position = eventData.position;
                }
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
        {
            if (isSlotActiveSelf)
            {
                if (slotType != EquipmentType.None)
                {
                    if (DragSlot.instance.dragSlot.item.equipmentType == slotType)
                    {
                        ChangeSlot();
                    }
                }
                else
                {
                    ChangeSlot();
                }
            }
        }
    }

    private void ChangeSlot()
    {
        Item _tempItem = item;
        int _tempItemCount = itemCount;

        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);

        if (DragSlot.instance.dragSlot.item == _tempItem)
        {
            if (DragSlot.instance.dragSlot.item.itemType != ItemType.Equipment)
            {
                if (DragSlot.instance.dragSlot != this)
                {
                    PlusCount(_tempItemCount);
                    DragSlot.instance.dragSlot.ClearSlot();
                    return;
                }
            }
        }

        if (_tempItem != null)
        {
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount);
        }
        else
        {
            DragSlot.instance.dragSlot.ClearSlot();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(StoreManager.instance.isButton)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                AddItem(StoreManager.instance.storeSlot.item, StoreManager.instance.storeSlot.itemCount);
                StoreManager.instance.isButton = false;
                StoreManager.instance.storeSlot.ClearSlot();
            }
        }
    }
}
