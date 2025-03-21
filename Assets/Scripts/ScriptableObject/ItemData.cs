using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipable,
    Consumable
}

public enum ConsumableType
{
    Health,
    Critical
}

[Serializable]
public class ItemDataConsumable
{
    public ConsumableType consumableType;
    public float itemValue;
} 

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string itemDataName;
    public string itemDataInfo;
    public ItemType itemDataType;
    public Sprite itemDataIcon;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;
}
