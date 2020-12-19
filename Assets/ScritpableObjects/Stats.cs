using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Info")]
public class Stats : ScriptableObject
{
    private float _health = 100;
    [SerializeField]
    private InventoryItems _inventoryItems;

    public void SavePlayer()
    {
        SaveSystem.SavePlayer();
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        _health = data.health;

        _inventoryItems.Clear();

        for(int i = 0; i < data.itemNames.Length; i++)
        {
            if (data.itemNames[i] == null) continue;
            _inventoryItems.AddItemToInventoryAtIndex(GameVariables.prefabs[data.itemNames[i]], data.itemStacks[i], data.itemSlot[i]);
        }

        if (data.weaponSlot != null)  _inventoryItems.EquippedWeapon = GameVariables.prefabs[data.weaponSlot];

        else  _inventoryItems.EquippedWeapon = null;


        if (data.armorSlot != null) _inventoryItems.EquippedArmor = GameVariables.prefabs[data.armorSlot];

        else _inventoryItems.EquippedWeapon = null;

        _inventoryItems.UpdateInventory();

        GameObject.Find("Inventory").GetComponent<InventoryBehavior>().needToUpdateInventoryUI = true;
    }

    public float GetPlayerHealth()
    {
        return _health;
    }

    public InventoryItems GetInventory()
    {
        return _inventoryItems;
    }

    public GameObject EquippedWeapon
    {
        get
        {
            return _inventoryItems.EquippedWeapon;
        }
    }

    public GameObject EquippedArmor
    {
        get
        {
            return _inventoryItems.EquippedArmor;
        }
    }
}
