using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CopySlot : MonoBehaviour
{
    public Slot slot;
    public Image itemImage;
    public int itemCount;
    [SerializeField] private TextMeshProUGUI countText;

    private void FixedUpdate()
    {
        if (slot.item != null)
        {
            AddItem();
        }
        else
        {
            if (itemImage.sprite != null)
            {
                SetColor(0);
                itemImage.sprite = null;
                countText.gameObject.SetActive(false);
            }
        }
    }

    private void SetColor(float alpha)
    {
        Color color = itemImage.color;
        color.a = alpha;
        itemImage.color = color;
    }

    private void AddItem()
    {
        itemImage.sprite = slot.item.itemImage;
        itemCount = slot.itemCount;

        if(slot.item.itemType != ItemType.Equipment)
        {
            countText.gameObject.SetActive(true);
            countText.text = itemCount.ToString();
        }
        else
        {
            countText.gameObject.SetActive(false);
        }
        SetColor(1);
    }
}
