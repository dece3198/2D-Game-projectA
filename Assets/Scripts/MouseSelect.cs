using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseSelect : MonoBehaviour
{
    private new SpriteRenderer renderer;
    [SerializeField] private PlayerController playerController;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mPos = new Vector2(Mathf.Round(mPos.x), Mathf.Round(mPos.y));
        transform.position = mPos;

        if(Mathf.Abs(transform.localPosition.x) > 1.5f || Mathf.Abs(transform.localPosition.y) > 1.5f)
        {
            renderer.color = Color.red;
        }
        else
        {
            Vector3Int currentCell = TileManager.instance.groundTilmap.WorldToCell(mPos);
            if (TileManager.instance.farmLandTile == TileManager.instance.farmLandTilemap.GetTile(currentCell))
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    GameObject seed = Instantiate(playerController.curItem.GetComponent<ItemPickUp>().item.seed);
                    seed.transform.position = mPos;
                    playerController.curSlot.slot.PlusCount(-1);
                }
                renderer.color = Color.white;
            }
            else
            {
                renderer.color = Color.red;
            }
        }
    }
}
