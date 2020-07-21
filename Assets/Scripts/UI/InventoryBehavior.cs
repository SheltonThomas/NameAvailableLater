using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBehavior : MonoBehaviour
{
    // Gets the canvas for the inventory UI.
    private GameObject inventoryArea;
    // Used for the different slots of the inventory.
    private List<GameObject> inventorySlots;
    // An instance of the player's inventory.
    public InventoryItems playerInventory;
    // Used to check if the inventory is updated.
    [HideInInspector]
    public bool needToUpdateInventoryUI;

    void Start()
    {
        // Gets the inventory's canvas.
        inventoryArea = GameObject.Find("Canvas");
        // Initializes inventory slots.
        inventorySlots = new List<GameObject>();

        // Sets the inventory slots to the slots on the canvas.
        foreach(Transform child in inventoryArea.transform)
        {
            inventorySlots.Add(child.gameObject);
        }
    }

    private void Update()
    {
        // Updates the inventory UI if needed.
        if(needToUpdateInventoryUI)
        {
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        // Gets the player's items to update the UI.
        List<GameObject> playerInventoryItems = playerInventory.GetInventoryItems();
        // Gets the player's items stacks to update the UI.
        List<int> playerItemStack = playerInventory.GetItemStacks();
        // Updates everything in the UI.
        for(int i = 0; i < playerInventoryItems.Count; i++)
        {
            // Updates the image of the slots of the UI.
            UpdateSlotImage(inventorySlots[i], playerInventoryItems[i]);
            // Updates the stack number for the UI.
            UpdateSlotNumber(inventorySlots[i], playerItemStack[i]);
        }
        // Sets that the UI doesn't need to update anymore.
        needToUpdateInventoryUI = false;
    }

    private void UpdateSlotImage(GameObject slot, GameObject inventoryItem)
    {
        // Gets the sprite from the item in the player's inventory.
        Sprite inventoryItemSprite = inventoryItem.GetComponent<SpriteRenderer>().sprite;
        // Sets the sprite of the slot to the sprite of the item in the player's inventory.
        slot.GetComponent<Image>().sprite = inventoryItemSprite;
    }

    private void UpdateSlotNumber(GameObject slot, int itemStack)
    {
        foreach(Transform child in slot.transform)
        {
            // Updates the stack number of the item.
            child.gameObject.GetComponent<Text>().text = itemStack.ToString();
            // Breaks out of the function.
            return;
        }
    }

    public void RemoveItem(int index)
    {
        needToUpdateInventoryUI = true;
    }
}
