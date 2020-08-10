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
    private List<int?> itemStack;

    // Gets the UI for the inventory.
    private InventoryBehavior inventoryUI;

    // Equipped weapon and armor.
    public GameObject EquippedWeapon { get; set; }
    public GameObject EquippedArmor { get; set; }

    private void Start()
    {
        // Gets the inventory UI.
        inventoryUI = GameObject.Find("Inventory").GetComponent<InventoryBehavior>();
        // Initializes the player inventory.
        inventory = new List<GameObject>();
        // Initializes the item stacks.
        itemStack = new List<int?>();
        // Sets the UI's instance of the player's inventory to the current script.
        inventoryUI.playerInventory = this;
        // Sets the slots of the inventory to null.
        for(int i = 0; i < inventoryUI.amountOfSlots; i++)
        {
            inventory.Add(null);
            itemStack.Add(null);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If collided with something with the Item component
        if (other.gameObject.TryGetComponent(out Item item))
        {
            // Adds the item to the player's inventory.
            AddItemToInventory(other);

            // Destroys the object that was hit.
            Destroy(other.gameObject);
            // Marks that the inventory has to be updated.
            inventoryUI.needToUpdateInventoryUI = true;
        }
    }

    private void AddItemToInventory(Collider2D other)
    {
        // Checks to see if the item is in the inventory.
        int? slotToPlaceItem = CheckInventory(GameVariables.prefabs[other.gameObject.name]);
        // If the item isn't in the inventory, return.
        if (slotToPlaceItem == null)
            return;

        // If the slot isn't empty, increase that slots stack count.
        if (inventory[(int)slotToPlaceItem] != null)
        {
            itemStack[(int)slotToPlaceItem]++;
            return;
        }
        // Adds the item to the player's inventory.
        inventory[(int)slotToPlaceItem] = (GameVariables.prefabs[other.gameObject.name]);
        // Sets that items stack to 1.
        itemStack[(int)slotToPlaceItem] = 1;
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
    public List<int?> GetItemStacks()
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
}
