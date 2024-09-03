using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap[] tileMap;
    [SerializeField] private TileBase[] tileBase;
    int rand;
    int randCount;

    private void Start()
    {
        for(int i = 0; i < 35; i++)
        {
            for(int j = 0; j < 22; j++)
            {
                rand = Random.Range(0, 100);
                randCount = Random.Range(0, tileBase.Length);
                if(rand < 50)
                {
                    if (randCount == 2)
                    {
                        tileMap[2].SetTile(new Vector3Int(i, j, 0), tileBase[2]);
                    }
                    else
                    {
                        for (int k = 0; k < tileMap.Length - 1; k++)
                        {
                            tileMap[k].SetTile(new Vector3Int(i, j, 0), tileBase[randCount]);
                        }
                    }
                }
            }
        }
    }
}
