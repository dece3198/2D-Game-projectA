using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum SeedType
{
    PumpkinSeed,TomatoSeed
}

public class Seed : MonoBehaviour
{
    [SerializeField] private Sprite growthA;
    [SerializeField] private Sprite growthB;
    [SerializeField] private Sprite cropsImage;
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
                spriteRenderer.sprite = cropsImage;
                this.enabled = false;
                StartCoroutine(BoxCO());
            }
        }
    }

    public int growthCount;
    public int life = 0;
    public SeedType seedType;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }


    private void Update()
    {
        if(tilemap == null)
        {
            tilemap = GameObject.Find("Plant Object").GetComponent<Tilemap>();
        }

        if (life < 0)
        {
            CropsManager.instance.ReMoveSeed(this);
            Vector3Int currentCell = tilemap.WorldToCell(transform.position);
            tilemap.SetTile(currentCell, dieCrops);
            Destroy(gameObject);
        }
    }

    private IEnumerator BoxCO()
    {
        yield return new WaitForSeconds(1f);
        boxCollider.enabled = true;
    }
}
