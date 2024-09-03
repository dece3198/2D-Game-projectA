using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public enum PlayerEquipmentType
{
    None, WateringCan, Hoe, Axe
}


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
    public bool isItem = true;
    [SerializeField] private CopySlot[] copySlots;
    public GameObject curItem;
    public CopySlot curSlot;
    public EquipmentType equipmentType;
    public MouseSelect mouseSelect;
    

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
    }

    private void FixedUpdate()
    {

        if (isPlayerMove)
        {
            Vector2 nextVec = inputVec.normalized * speed * Time.deltaTime;
            rigid.MovePosition(rigid.position + nextVec);
        }
        PlayerMove();
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
            if(curSlot != null)
            {
                if(curItem != null)
                {
                    curItem.SetActive(false);
                    curItem = null;
                }
                mouseSelect.gameObject.SetActive(false);
                curSlot.GetComponent<Outline>().enabled = false;
            }
            copySlots[count].GetComponent<Outline>().enabled = true;
            curSlot = copySlots[count];
            isItem = true;
        }
    }

    public void EquipmentChange(EquipmentType type)
    {
        equipmentType = type;
    }
}
