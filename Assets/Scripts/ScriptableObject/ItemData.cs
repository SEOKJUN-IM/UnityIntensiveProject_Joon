using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipable,
    Consumable
}

public enum EquipableType
{
    Attack,
    Defense
}

public enum ConsumableType
{
    Health,
    Critical
}

[Serializable]
public class ItemDataEquipable
{
    public EquipableType equipableType;
    public int itemValue;
}

[Serializable]
public class ItemDataConsumable
{
    public ConsumableType consumableType;
    public int itemValue;
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

    [Header("Equipable")]
    public ItemDataEquipable[] equipables;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;
}
