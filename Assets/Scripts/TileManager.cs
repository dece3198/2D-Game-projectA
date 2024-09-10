using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public static TileManager instance;
    public PlayerController playerController;
    public Tilemap groundTilMap;
    public Tilemap farmLandTileMap;
    public Tilemap[] objectTileMap;
    public TileBase farmLandTile;
    public Tile previousTile;
    [SerializeField] private GameObject log;
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;


    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        instance = this;
    }

    public void ChangeTile()
    {
        Vector3Int currentCell = groundTilMap.WorldToCell(playerController.transform.position + playerController.LastMove);
        if (previousTile == farmLandTileMap.GetTile(currentCell))
        {
            if (farmLandTile != farmLandTileMap.GetTile(currentCell))
            {
                farmLandTileMap.SetTile(currentCell, farmLandTile);
            }
        }
    }

    public void DeleteTile()
    {
        Vector3Int currentCell = farmLandTileMap.WorldToCell(playerController.transform.position + playerController.LastMove);

        if (farmLandTile == farmLandTileMap.GetTile(currentCell))
        {
            farmLandTileMap.SetTile(currentCell, null);
        }
    }

    public void ChangeWetTile()
    {
        Vector3Int currentCell = farmLandTileMap.WorldToCell(playerController.transform.position + playerController.LastMove);

        if (farmLandTile == farmLandTileMap.GetTile(currentCell))
        {
            Color color = new Color(211f / 255f, 179f / 255f, 165f / 255f, 1);
            farmLandTileMap.SetColor(currentCell, color);
        }
    }

    public void LogAtkTile()
    {
        for(int i = 0; i < objectTileMap.Length; i++)
        {
            Vector3Int currentCell = objectTileMap[i].WorldToCell(playerController.transform.position + playerController.LastMove);
            
            if (objectTileMap[i].GetTile(currentCell) != null)
            {

                if(objectTileMap[i] != objectTileMap[2])
                {
                    audioSource.PlayOneShot(audioClips[0]);
                    Camera.main.GetComponent<CameraShake>().Shake();
                }

                objectTileMap[i].SetTile(currentCell, null);
                if(i == 1)
                {
                    GameObject _log = Instantiate(log);
                    _log.transform.position = playerController.transform.position + playerController.LastMove;
                    _log.GetComponent<Rigidbody2D>().gravityScale = 1;
                    _log.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 3f,ForceMode2D.Impulse);
                    StartCoroutine(GravityCo(_log));
                }
                return;
            }
        }

    }

    public IEnumerator GravityCo(GameObject obj)
    {
        obj.GetComponent<ItemPickUp>().isGet = false;
        yield return new WaitForSeconds(0.65f);
        obj.GetComponent<Rigidbody2D>().gravityScale = 0;
        obj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        obj.GetComponent<ItemPickUp>().isGet = true;
    }
}

