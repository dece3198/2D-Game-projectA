using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public PlayerController playerController;
    public Tilemap groundTilmap;
    public Tilemap farmRandTilemap;
    public TileBase farmRandTile;
    public Tile previousTile;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ChangeTile();
        }
    }

    public void ChangeTile()
    {
        Vector3Int currentCell = groundTilmap.WorldToCell(playerController.transform.position + playerController.LastMove);
        if (previousTile == groundTilmap.GetTile(currentCell))
        {
            if (farmRandTile != farmRandTilemap.GetTile(currentCell))
            {
                farmRandTilemap.SetTile(currentCell, farmRandTile);
            }
        }
    }

    public void DeleteTile()
    {
        Vector3Int currentCell = farmRandTilemap.WorldToCell(playerController.transform.position + playerController.LastMove);

        if (farmRandTile == farmRandTilemap.GetTile(currentCell))
        {
            farmRandTilemap.SetTile(currentCell, null);
        }
    }

    public void ChangeWetTile()
    {
        Vector3Int currentCell = farmRandTilemap.WorldToCell(playerController.transform.position + playerController.LastMove);

        if (farmRandTile == farmRandTilemap.GetTile(currentCell))
        {
            Color color = new Color(211f / 255f, 179f / 255f, 165f / 255f, 1);
            farmRandTilemap.SetColor(currentCell, color);
        }
    }
}
