using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Tilemaps;

[System.Serializable]
public class PlayerData
{
    public string saveName;
    public string name;
    public bool isStart = true;
    public float gold = 500;
    public float minute;
    public int hour = 6;
    public int day = 1;
    public int month = 3;
    public float playerStamina = 100;
    public Vector3 playerPos;
    public string dayString = "¿ù";

    public List<int> slotNumber = new List<int>();
    public List<int> itemNumber = new List<int>();
    public List<int> itemCount = new List<int>();

    public List<int> cropsNumber = new List<int>();
    public List<int> seedCount = new List<int>();
    public List<int> seedLife = new List<int>();
    public List<Vector3> cropsPos = new List<Vector3>();

    public List<int> tiles = new List<int>();
    public List<Vector3Int> tilePos = new List<Vector3Int>();

    public List<Vector3Int> farmLandPos = new List<Vector3Int>();
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public PlayerData curData = new PlayerData();
    public string path;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this);

        path = Application.persistentDataPath + "/";
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(curData);
        File.WriteAllText(path + curData.saveName, data);
    }

    public void DeleteData(string name)
    {
        File.Delete(path + name);
    }

    public void LoadData(string name)
    {
        if (File.Exists(path + name))
        {
            string data = File.ReadAllText(path + name);
            curData = JsonUtility.FromJson<PlayerData>(data);
        }
    }
}
