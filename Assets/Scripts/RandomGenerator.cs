using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap[] tileMap;
    [SerializeField] private TileBase[] tileBase;
    [SerializeField] private Tilemap farmLandTileMap;
    [SerializeField] private TileBase farmLandTileBase;
    int rand;
    int randCount;

    public Dictionary<int, TileBase> tileDic = new Dictionary<int, TileBase>();
    public Dictionary<TileBase, int> tileNumberDic = new Dictionary<TileBase, int>();

    private void Awake()
    {
        for (int i = 0; i < tileBase.Length; i++)
        {
            tileDic.Add(i, tileBase[i]);
            tileNumberDic.Add(tileBase[i], i);
        }
    }

    private void Start()
    {
        if(DataManager.instance.curData.isStart)
        {
            for (int i = 0; i < 35; i++)
            {
                for (int j = 0; j < 22; j++)
                {
                    rand = Random.Range(0, 100);
                    randCount = Random.Range(0, tileBase.Length);
                    if (rand < 50)
                    {
                        if (randCount == 2)
                        {
                            tileMap[2].SetTile(new Vector3Int(i, j, 0), tileBase[2]);
                            DataManager.instance.curData.tiles.Add(tileNumberDic[tileBase[2]]);
                            DataManager.instance.curData.tilePos.Add(new Vector3Int(i, j, 0));
                        }
                        else
                        {
                            for (int k = 0; k < tileMap.Length - 1; k++)
                            {
                                tileMap[k].SetTile(new Vector3Int(i, j, 0), tileBase[randCount]);
                                DataManager.instance.curData.tiles.Add(tileNumberDic[tileBase[randCount]]);
                                DataManager.instance.curData.tilePos.Add(new Vector3Int(i, j, 0));
                            }
                        }
                    }
                }
            }
        }
        else
        {
            for(int i = 0; i < DataManager.instance.curData.tiles.Count; i++)
            {
                if (DataManager.instance.curData.tiles[i] == tileNumberDic[tileBase[2]])
                {
                    tileMap[2].SetTile(DataManager.instance.curData.tilePos[i], tileBase[2]);
                }
                else
                {
                     for(int j = 0; j < tileMap.Length - 1; j++)
                    {
                        tileMap[j].SetTile(DataManager.instance.curData.tilePos[i], tileDic[DataManager.instance.curData.tiles[i]]);
                    }
                }
            }

            if(DataManager.instance.curData.farmLandPos.Count > 0)
            {
                for(int i = 0; i < DataManager.instance.curData.farmLandPos.Count; i++)
                {
                    farmLandTileMap.SetTile(DataManager.instance.curData.farmLandPos[i], farmLandTileBase);
                }
            }
        }
    }
}
