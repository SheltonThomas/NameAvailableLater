using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryItems : MonoBehaviour
{
    // Makes an inventory for the player.
    [SerializeField]
    private List<GameObject> inventory;

    // Keeps track of the items stacked in the inventory;
    [SerializeField]
    private List<int> itemStack;

    // Gets the UI for the inventory.
    [SerializeField]
    private InventoryBehavior inventoryUI;

    private void Start()
    {
        // Initializes the player inventory.
        inventory = new List<GameObject>();
        // Initializes the item stacks.
        itemStack = new List<int>();
        // Sets the UI's instance of the player's inventory to the current script.
        inventoryUI.playerInventory = this;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If collided with something with the tag named Item
        if (other.gameObject.tag == "Item")
        {
            // Use to check to see if the player already has the item in their inventory.
            CheckStacks(other, out bool itemInInventory);

            // If the item isn't already in the player's inventory, then add it to the player's inventory.
            if (!itemInInventory)
            {
                AddItemToInventory(other);
            }

            // Destroys the object that was hit.
            Destroy(other.gameObject);
            // Marks that the inventory has to be updated.
            inventoryUI.needToUpdateInventoryUI = true;
        }
    }

    private void CheckStacks(Collider2D other, out bool itemInInventory)
    {
        // Keeps track of how many times the foreach has looped to get the position of the item in the list.
        int iteration = 0;
        bool setItemInInventory = false;
        // Loops to see if the item is in the player's inventory.
        foreach (GameObject inventoryItem in inventory)
        {
            // If the name of the object hit is the same as the name of the object the current looped item.
            if (inventoryItem.name == other.gameObject.name)
            {
                // Increases the stack at the current index.
                itemStack[iteration]++;
                // Sets that the item was in the player's inventory.
                setItemInInventory = true;
                // Breaks out of the loop.
                break;
            }
            iteration++;
        }

        //Shoudln't need to be like this but have to do this because I'm trying to send out the value.
        itemInInventory = false;

        if(setItemInInventory)
        {
            itemInInventory = true;
        }
    }

    private void AddItemToInventory(Collider2D other)
    {
        // Adds the item to the player's inventory.
        inventory.Add(GameVariables.prefabs[other.gameObject.name]);
        // Sets that items stack to 1.
        itemStack.Add(1);
    }

    public int Count 
    {
        // Returns the amount of items in the inventory from other scripts.
        get
        {
            return inventory.Count;
        }
    }

    public GameObject this[int index]
    {
        // Returns the item at the index of the sent in index when in other scripts.
        get
        {
            return inventory[index];
        }
        // Sets the item at the index of the sent in index when in other scripts.
        set
        {
            inventory[index] = value;
        }
    }

    // Returns the intentory's items when in other scripts.
    public List<GameObject> GetInventoryItems()
    {
        return inventory;
    }

    // Returns the item stacks when in other scripts.
    public List<int> GetItemStacks()
    {
        return itemStack;
    }
}
