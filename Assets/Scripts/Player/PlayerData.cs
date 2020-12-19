using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float health;
    public string[] itemNames;
    public int[] itemStacks;
    public string[] slotType;
    public int[] itemSlot;
    public string weaponSlot;
    public string armorSlot;

    //Finish implementing equppied weapon and armor saving and loading
    public PlayerData(Stats info, InventoryItems inventoryItems)
    {
        health = info.GetPlayerHealth();
        if (info.EquippedWeapon != null) weaponSlot = info.EquippedWeapon.name;
        if (info.EquippedArmor != null) armorSlot = info.EquippedArmor.name;
        InventoryItems items = inventoryItems;
        int itemsInInventory = items.Count;
        itemNames = new string[itemsInInventory];
        itemStacks = new int[itemsInInventory];
        itemSlot = new int[itemsInInventory];

        for(int i = 0; i < itemsInInventory; i++)
        {
            if (items[i] == null) continue;

            Item itemData = items[i].GetComponent<Item>();

            itemNames[i] = items[i].name;
            itemStacks[i] = (int)items.GetItemStacks()[i];
            itemSlot[i] = itemData.Slot;
        }
    }
}
