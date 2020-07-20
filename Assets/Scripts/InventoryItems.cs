using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryItems : MonoBehaviour
{
    // Makes an inventory for the player.
    public List<GameObject> Inventory;

    private void Start()
    {
        Inventory = new List<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Item")
        {
            Inventory.Add(GameVariables.prefabs[other.gameObject.name]);
            Destroy(other.gameObject);
        }
    }

    public int Count {
        get
        {
            return Inventory.Count;
        }
    }

    public GameObject this[int index]
    {
        get
        {
            return Inventory[index];
        }
        set
        {
            Inventory[index] = value;
        }
    }

    public void RemoveItem(string itemName)
    {
        List<GameObject> newInventory = new List<GameObject>();

        foreach(GameObject item in Inventory)
        {
            if(item.name == itemName)
            {
                continue;
            }
            newInventory.Add(item);
        }

        Inventory = newInventory;
    }
}
