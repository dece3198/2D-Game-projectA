using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Seed : MonoBehaviour
{
    [SerializeField] private GameObject crops;
    [SerializeField] private Sprite growthA;
    [SerializeField] private Sprite growthB;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile dieCrops;
    [SerializeField] private int curCount;
    public int CurCount
    {
        get { return curCount; }
        set
        {
            curCount = value;

            if (curCount >= (growthCount / 3))
            {
                spriteRenderer.sprite = growthA;
            }
            if (curCount >= ((growthCount / 3) * 2))
            {
                spriteRenderer.sprite = growthB;
            }
            if (curCount >= growthCount)
            {
                GameObject _crops = Instantiate(crops);
                _crops.transform.position = transform.position;
                Destroy(gameObject);
            }
        }
    }

    public int growthCount;
    public int life = 0;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        if(tilemap == null)
        {
            tilemap = GameObject.Find("Plant Object").GetComponent<Tilemap>();
        }

        if(life < 0)
        {
            Vector3Int currentCell = tilemap.WorldToCell(transform.position);
            tilemap.SetTile(currentCell, dieCrops);
            Destroy(gameObject);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            CurCount++;
        }
    }
}
