using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    public GameObject information;
    [SerializeField] private GameObject inventory;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemType;
    [SerializeField] private TextMeshProUGUI itemPrice;
    [SerializeField] private TextMeshProUGUI itemdata;
    private bool inventoryActiveSelf = false;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            inventoryActiveSelf = !inventoryActiveSelf;
            if(inventoryActiveSelf)
            {
                inventory.SetActive(true);
            }
            else
            {
                inventory.SetActive(false);
                information.SetActive(false);
            }
        }
    }

    public void Information(Item item)
    {
        itemName.text = item.information.name;
        itemType.text = item.information.type;
        itemPrice.text = item.price.ToString();
        itemdata.text = item.information.data;
    }
}
