using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EquipmentController : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerController;
    [SerializeField] private GameObject handItemParent;
    public ItemPickUp[] handItems;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        handItems = handItemParent.GetComponentsInChildren<ItemPickUp>();
        for(int i = 0; i < handItems.Length; i++)
        {
            handItems[i].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (!playerController.isMove)
        {
            if(playerController.isPlayerMove)
            {
                if (playerController.curSlot.slot.item != null)
                {
                    playerController.EquipmentChange(playerController.curSlot.slot.item.equipmentType);

                    if (playerController.equipmentType == EquipmentType.Seed)
                    {
                        Seed();
                    }

                    if (Input.GetButtonDown("Fire1"))
                    {
                        switch (playerController.equipmentType)
                        {
                            case EquipmentType.WateringCan: WateringCan(); break;
                            case EquipmentType.Hoe: Hoe(); break;
                            case EquipmentType.Axe: Axe(); break;
                        }
                    }
                }
                else
                {
                    playerController.EquipmentChange(EquipmentType.None);
                    if(playerController.curItem != null)
                    {
                        playerController.curItem.SetActive(false);
                        playerController.curItem = null;
                    }
                }
            }
        }
    }

    private void Seed()
    {
        playerController.mouseSelect.gameObject.SetActive(true);

        for (int i = 0; i < handItems.Length; i++)
        {
            if (handItems[i].item == playerController.curSlot.slot.item)
            {
                playerController.curItem = handItems[i].gameObject;
                animator.SetTrigger("Item");
                handItems[i].gameObject.SetActive(true);
                return;
            }
        }
    }


    private void WateringCan()
    {
        animator.SetTrigger("WateringCan");
        TileManager.instance.ChangeWetTile();
        StartCoroutine(EquipmentCo());
    }

    private void Hoe()
    {
        animator.SetTrigger("Hoe");
        TileManager.instance.ChangeTile();
        StartCoroutine(EquipmentCo());
    }

    private void Axe()
    {
        animator.SetTrigger("Axe");
        StartCoroutine(EquipmentCo());
    }

    private IEnumerator EquipmentCo()
    {
        playerController.isPlayerMove = false;
        yield return new WaitForSeconds(1);
        playerController.isPlayerMove = true;
    }
}
