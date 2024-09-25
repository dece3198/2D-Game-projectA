using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropsManager : MonoBehaviour
{
    public static CropsManager instance;
    public List<Seed> Seeds = new List<Seed>();
    [SerializeField] private Tilemap  farmLandTileMap;
    public Dictionary<int, GameObject> cropsDic = new Dictionary<int, GameObject>();
    public Dictionary<SeedType, int> seedDic = new Dictionary<SeedType, int>();
    public GameObject[] crops;

    private void Awake()
    {
        instance = this;

        for(int i = 0; i < crops.Length; i++)
        {
            cropsDic.Add(i, crops[i]);
            seedDic.Add(crops[i].GetComponent<Seed>().seedType, i);
        }
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

    public void ReMoveSeed(Seed seed)
    {
        for(int i = 0; i < Seeds.Count;i++)
        {
            if(seed == Seeds[i])
            {
                Seeds.RemoveAt(i);
            }
        }
    }
}
