using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerController;
    private TileManager tileManager;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        tileManager = GetComponent<TileManager>();
    }

    private void Update()
    {
        if (playerController.isWateringCan)
        {
            if (!playerController.isMove)
            {
                WateringCan();
            }
        }
    }


    private void WateringCan()
    {
        if(playerController.isPlayerMove)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                animator.SetTrigger("WateringCan");
                tileManager.ChangeWetTile();
                StartCoroutine(WateringCanCo());
            }
        }
    }

    private IEnumerator WateringCanCo()
    {
        playerController.isPlayerMove = false;
        yield return new WaitForSeconds(1.5f);
        playerController.isPlayerMove = true;
    }
}
