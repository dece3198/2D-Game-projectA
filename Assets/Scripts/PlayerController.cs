using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    private Animator animator;
    private Vector2 inputVec;
    public Vector3 LastMove;
    public bool isMove;
    public bool isPlayerMove = true;
    [SerializeField] private  ItemPickUp[] handItems;
    [SerializeField] private CopySlot[] copySlots;
    public GameObject curItem;
    public CopySlot curSlot;
    public MouseSelect mouseSelect;
    public bool isWateringCan = false;
    public bool isHoe = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        curSlot = copySlots[0];
        curSlot.GetComponent<Outline>().enabled = true;
        
    }

    private void Update()
    {
        if(isPlayerMove)
        {
            PlayerMove();
        }
        ChangeSlot(KeyCode.Alpha1, 0);
        ChangeSlot(KeyCode.Alpha2, 1);
        ChangeSlot(KeyCode.Alpha3, 2);
        ChangeSlot(KeyCode.Alpha4, 3);
        ChangeSlot(KeyCode.Alpha5, 4);
        ChangeSlot(KeyCode.Alpha6, 5);
        ChangeSlot(KeyCode.Alpha7, 6);
        ChangeSlot(KeyCode.Alpha8, 7);
        ChangeSlot(KeyCode.Alpha9, 8);
        ChangeSlot(KeyCode.Alpha0, 9);
        ChangeSlot(KeyCode.KeypadPeriod, 10);
        ChangeSlot(KeyCode.KeypadEquals, 11);


        if(curSlot != null)
        {
            if (curSlot.slot.item != null)
            {
                if(curSlot.slot.item.itemType != ItemType.Equipment)
                {
                    for (int i = 0; i < handItems.Length; i++)
                    {
                        if (curSlot.slot.item == handItems[i].item)
                        {
                            if (!handItems[i].gameObject.activeSelf)
                            {
                                curItem = handItems[i].gameObject;
                                if(handItems[i].item.itemType == ItemType.Seed)
                                {
                                    mouseSelect.gameObject.SetActive(true);
                                }
                                else
                                {
                                    mouseSelect.gameObject.SetActive(false);
                                }
                                curItem.SetActive(true);
                                animator.SetTrigger("Item");
                            }
                        }
                    }
                }
                else
                {
                    curItem = null;
                    switch(curSlot.slot.item.equipmentType)
                    {
                        case EquipmentType.WateringCan: isWateringCan = true; break;
                        case EquipmentType.Hoe: isHoe = true; break;
                    }
                }
            }
            else
            {
                if(curItem != null)
                {
                    curItem.SetActive(false);
                    curItem = null;
                    mouseSelect.gameObject.SetActive(false);
                }
            }
        }

    }

    private void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    private void LateUpdate()
    {
        if (inputVec.x != 0)
        {
            sprite.flipX = inputVec.x < 0;
        }
    }

    private void PlayerMove()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        isMove = inputVec.x != 0 || inputVec.y != 0;
        
        if(curItem != null)
        {
            animator.SetBool("ItemOff", false);
            animator.SetBool("ItemIsMove", isMove);
        }
        else
        {
            animator.SetBool("ItemOff", true);
            animator.SetBool("IsMove", isMove);
        }

        if (isMove)
        {
            LastMove.x = inputVec.x;
            LastMove.y = inputVec.y;
            animator.SetFloat("MoveX", inputVec.x);
            animator.SetFloat("MoveY", inputVec.y);
        }
        else
        {
            animator.SetFloat("IdleX", LastMove.x);
            animator.SetFloat("IdleY", LastMove.y);
        }
    }

    private void ChangeSlot(KeyCode code, int count)
    {
        if(Input.GetKeyDown(code))
        {
            isWateringCan = false;
            isHoe = false;
            mouseSelect.gameObject.SetActive(false);

            if (curSlot != null)
            {
                curSlot.GetComponent<Outline>().enabled = false;
            }

            if(curItem != null)
            {
                curItem.SetActive(false);
            }
            else
            {
                curItem = null;
            }
            if (copySlots[count].slot.item == null)
            {
                curItem = null;
            }
            curSlot = copySlots[count];
            curSlot.GetComponent<Outline>().enabled = true;
        }
    }
}
