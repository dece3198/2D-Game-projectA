using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (!playerController.isMove)
        {
            if(playerController.isPlayerMove)
            {
                if (playerController.isWateringCan)
                {
                    WateringCan();
                }
                else if (playerController.isHoe)
                {
                    Hoe();
                }
            }
        }
    }


    private void WateringCan()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("WateringCan");
            TileManager.instance.ChangeWetTile();
            StartCoroutine(WateringCanCo());
        }
    }

    private void Hoe()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Hoe");
            TileManager.instance.ChangeTile();
            StartCoroutine(WateringCanCo());
        }
    }

    private IEnumerator WateringCanCo()
    {
        playerController.isPlayerMove = false;
        yield return new WaitForSeconds(1);
        playerController.isPlayerMove = true;
    }
}
