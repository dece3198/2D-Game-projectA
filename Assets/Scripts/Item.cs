using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipment, Used, Ingredient, ETC
}

public enum EquipmentType
{
    None, Pitching, Armor, Shoes, Necklace, Ring, Sword, Seed, WateringCan, Hoe, Axe
}

[System.Serializable]
public class Information
{
    public string name;
    public string type;
    [SerializeField, TextArea(2, 5)]
    public string data;
}

[CreateAssetMenu(fileName = "New Item" , menuName = "New Item/ Item")]
public class Item : ScriptableObject
{
    public Sprite itemImage;
    public GameObject itemPrefab;
    public GameObject seed;
    public ItemType itemType;
    public EquipmentType equipmentType;
    public Information information;
    public int price;
}
