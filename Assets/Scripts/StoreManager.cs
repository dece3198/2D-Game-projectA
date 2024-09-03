using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public static StoreManager instance;
    public Slot storeSlot;
    public bool isButton = false;


    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(isButton)
        {
            if(storeSlot.item != null)
            {
                Vector2 mousePos = Input.mousePosition;
                storeSlot.transform.position = mousePos;
            }
        }
    }
}
