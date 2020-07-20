using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryItems : MonoBehaviour
{
    // Makes an inventory for the player.
    [SerializeField]
    private List<GameObject> inventory;
    [SerializeField]
    private List<int> itemStack;
    [SerializeField]
    private InventoryBehavior inventoryUI;

    private void Start()
    {
        inventory = new List<GameObject>();
        itemStack = new List<int>();
        inventoryUI.playerInventory = this;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Item")
        {
            bool itemInInventory = false;
            int iteration = 0;
            foreach (GameObject inventoryItem in inventory)
            {
                if (inventoryItem.name == other.gameObject.name)
                {
                    itemStack[iteration]++;
                    Destroy(other.gameObject);
                    itemInInventory = true;
                    break;
                }
                iteration++;
            }
            if(!itemInInventory)
            {
                inventory.Add(GameVariables.prefabs[other.gameObject.name]);
                itemStack.Add(1);
                Destroy(other.gameObject);
            }
            inventoryUI.needToUpdateInventoryUI = true;
        }
    }

    public int Count 
    {
        get
        {
            return inventory.Count;
        }
    }

    public GameObject this[int index]
    {
        get
        {
            return inventory[index];
        }
        set
        {
            inventory[index] = value;
        }
    }

    public List<GameObject> GetInventoryItems()
    {
        return inventory;
    }

    public List<int> GetItemStacks()
    {
        return itemStack;
    }
}
