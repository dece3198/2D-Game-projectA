using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipment, Used, Ingredient, ETC
}

public enum EquipmentType
{
    None, Pitching, Armor, Shoes, Necklace, Ring, Sword, WateringCan
}

[CreateAssetMenu(fileName = "New Item" , menuName = "New Item/ Item")]
public class Item : ScriptableObject
{
    public Sprite itemImage;
    public GameObject itemPrefab;
    public ItemType itemType;
    public EquipmentType equipmentType;
}
