using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropsManager : MonoBehaviour
{
    public static CropsManager instance;
    public List<Seed> Seeds = new List<Seed>();
    [SerializeField] private Tilemap  farmLandTileMap;

    private void Awake()
    {
        instance = this;
    }

    public void ResetSeed()
    {
        if(Seeds.Count > 0)
        {
            for (int i = 0; i < Seeds.Count; i++)
            {
                Vector3Int currentCell = farmLandTileMap.WorldToCell(Seeds[i].transform.position);

                if (farmLandTileMap.GetColor(currentCell) == Color.white)
                {
                    Seeds[i].life--;
                }
                else
                {
                    farmLandTileMap.SetColor(currentCell, Color.white);
                    Seeds[i].CurCount++;
                }
            }
        }
    }
}
