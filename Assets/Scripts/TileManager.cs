using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public static TileManager instance;
    public PlayerController playerController;
    public Tilemap groundTilmap;
    public Tilemap farmLandTilemap;
    public TileBase farmLandTile;
    public Tile previousTile;
    public float time;

    private void Awake()
    {
        time = 3600;
        playerController = GetComponent<PlayerController>();
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ChangeTile();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            time -= 3580;
        }
    }

    public void ChangeTile()
    {
        Vector3Int currentCell = groundTilmap.WorldToCell(playerController.transform.position + playerController.LastMove);
        if (previousTile == groundTilmap.GetTile(currentCell))
        {
            if (farmLandTile != farmLandTilemap.GetTile(currentCell))
            {
                farmLandTilemap.SetTile(currentCell, farmLandTile);
            }
        }
    }

    public void DeleteTile()
    {
        Vector3Int currentCell = farmLandTilemap.WorldToCell(playerController.transform.position + playerController.LastMove);

        if (farmLandTile == farmLandTilemap.GetTile(currentCell))
        {
            farmLandTilemap.SetTile(currentCell, null);
        }
    }

    public void ChangeWetTile()
    {
        Vector3Int currentCell = farmLandTilemap.WorldToCell(playerController.transform.position + playerController.LastMove);

        if (farmLandTile == farmLandTilemap.GetTile(currentCell))
        {
            Color color = new Color(211f / 255f, 179f / 255f, 165f / 255f, 1);
            StartCoroutine(WetTileCo(currentCell, color));
        }
    }

    private IEnumerator WetTileCo(Vector3Int cell, Color _color)
    {
        farmLandTilemap.SetColor(cell, _color);
        yield return new WaitForSeconds(time);
        farmLandTilemap.SetColor(cell, Color.white);
    }
}
