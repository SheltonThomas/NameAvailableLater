using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Item List")]
public class InventoryItems : ScriptableObject
{
    [SerializeField]
    // Makes an inventory for the player.
    private List<GameObject> inventory = new List<GameObject>();

    // Keeps track of the items stacked in the inventory;
    [SerializeField]
    private List<int> itemStack = new List<int>();
    public InventoryBehavior inventoryUI;

    // Equipped weapon and armor.
    public GameObject EquippedWeapon { get; set; }
    public GameObject EquippedArmor { get; set; }

    public void AddItemToInventory(GameObject other)
    {
        // Checks to see if the item is in the inventory.
        int? slotToPlaceItem = null;
        slotToPlaceItem = CheckInventory(GameVariables.prefabs[other.name]);
        // If the item isn't in the inventory, return.
        if (slotToPlaceItem == null)
            return;

        // If the slot isn't empty, increase that slots stack count.
        if (inventory[(int)slotToPlaceItem] != null)
        {
            itemStack[(int)slotToPlaceItem]++;
            inventory[(int)slotToPlaceItem].GetComponent<Item>().Slot = (int)slotToPlaceItem;
            UpdateInventory();
            return;
        }
        // Adds the item to the player's inventory.
        inventory[(int)slotToPlaceItem] = (GameVariables.prefabs[other.name]);
        // Sets that items stack to 1.
        itemStack[(int)slotToPlaceItem] = 1;
        inventory[(int)slotToPlaceItem].GetComponent<Item>().Slot = (int)slotToPlaceItem;
        UpdateInventory();
    }

    public void AddItemToInventoryAtIndex(GameObject other, int stackAmount, int index)
    {
        itemStack[index]++;
        inventory[index] = GameVariables.prefabs[other.name];
        inventory[index].GetComponent<Item>().Slot = index;
        itemStack[index] = stackAmount;
    }

    public void Clear()
    {
        inventory = new List<GameObject>();
        itemStack = new List<int>();
        for (int i = 0; i < inventoryUI.amountOfSlots; i++)
        {
            inventory.Add(null);
            itemStack.Add(0);
        }
        UpdateInventory();
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

    // Checks to see where the item needs to be placed in the inventory.
    private int? CheckInventory(GameObject objectToLookFor)
    {
        // Checks to see if the item is in the inventory.
        CheckForItemInInventory(objectToLookFor, out int? iterator);

        // If the returned iterator isn't null, return the index.
        if(iterator != null)
        {
            return iterator;
        }

        // Sets iterator to 0 for later use.
        iterator = 0;
        foreach(GameObject inventorySlot in inventory)
        {
            // If the slot is null, return the index.
            if(inventorySlot == null)
            {
                return iterator;
            }
            // Else increment the iterator.
            iterator++;
        }
        // If the foreach ends, then return that it can't place the item in the inventory.
        return null;
    }

    // Checks to see if the item is in the inventory.
    private void CheckForItemInInventory(GameObject objectToLookFor, out int? objectIndex)
    {
        // Used to say if the object is found or not.
        bool objectFound = false;
        // Used to get the index of the item.
        int? iterator = 0;
        foreach(GameObject inventorySlot in inventory)
        {
            // If the slot doesn't have anything in it.
            if(inventorySlot == null)
            {
                // Increment the iterator.
                iterator++;
                continue;
            }
            // If the item in the slot is the item we are looking for.
            if(inventorySlot == objectToLookFor)
            {
                // Set that the item was found.
                objectFound = true;
                break;
            }
            else
            {
                iterator++;
            }
        }

        // If the item wasn't found.
        if(!objectFound)
            // Set the iterator to null.
            iterator = null;

        // Set the object index to the iterator.
        objectIndex = iterator;
    }

    public void UpdateInventory()
    {
        inventoryUI.needToUpdateInventoryUI = true;
    }
}
