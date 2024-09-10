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
    private bool isEquipment = true;
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
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
        if (isEquipment)
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
                if (playerController.curItem != null)
                {
                    playerController.mouseSelect.gameObject.SetActive(false);
                    playerController.curItem.SetActive(false);
                    playerController.curItem = null;
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
                if(playerController.isItem)
                {
                    animator.SetTrigger("Item");
                    playerController.isItem = false;
                }
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
        audioSource.PlayOneShot(audioClips[0]);
        TileManager.instance.ChangeTile();
        StartCoroutine(EquipmentCo());
    }

    private void Axe()
    {
        animator.SetTrigger("Axe");
        audioSource.PlayOneShot(audioClips[0]);
        StartCoroutine(EquipmentCo());
    }

    private IEnumerator EquipmentCo()
    {
        isEquipment = false;
        playerController.isPlayerMove = false;
        GameManager.instance.PlayerStamina -= 1;
        GameManager.instance.Stamina();
        yield return new WaitForSeconds(1);
        playerController.isPlayerMove = true;
        isEquipment = true;
    }
}
