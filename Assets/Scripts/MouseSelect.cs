using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseSelect : MonoBehaviour
{
    public static MouseSelect instance;
    private new SpriteRenderer renderer;
    [SerializeField] private PlayerController playerController;
    public bool isSelect = true;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        instance = this;
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
            Vector3Int currentCell = TileManager.instance.groundTilMap.WorldToCell(mPos);
            if (TileManager.instance.farmLandTile == TileManager.instance.farmLandTileMap.GetTile(currentCell))
            {
                if (isSelect)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        GameObject seed = Instantiate(playerController.curItem.GetComponent<ItemPickUp>().item.seed);
                        seed.transform.position = mPos;
                        CropsManager.instance.Seeds.Add(seed.GetComponent<Seed>());
                        playerController.curSlot.slot.PlusCount(-1);
                    }
                    renderer.color = Color.white;
                }
                else
                {
                    renderer.color = Color.red;
                }
            }
            else
            {
                renderer.color = Color.red;
            }
        }
    }
}
