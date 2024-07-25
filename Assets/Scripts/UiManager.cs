using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    private bool inventoryActiveSelf = false;

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
            }
        }
    }
}
