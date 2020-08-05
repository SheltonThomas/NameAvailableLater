using System;
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
    [HideInInspector]
    public InventoryItems playerInventory;
    // Used to check if the inventory is updated.
    [HideInInspector]
    public bool needToUpdateInventoryUI;
    // Used to keep track of what and how many is on the mouse.
    private GameObject itemOnMouse;
    private int stackOnMouse;
    // Used for setting the default sprite of the inventory slots.
    [SerializeField]
    private Sprite defaultSprite;
    // The weapon and armor inventory slots.
    private GameObject weaponSlot, armorSlot;
    // Used for keeping track of if an item was just moved.
    private bool justMovedItem = false;
    // Used to set the amount of slots the inventory has.
    public int amountOfSlots = 0;


    void Start()
    {
        // Gets the inventory's canvas.
        inventoryArea = GameObject.Find("Slots");
        // Initializes inventory slots.
        inventorySlots = new List<GameObject>();

        // Sets the inventory slots to the slots on the canvas.
        foreach(Transform child in inventoryArea.transform)
        {
            amountOfSlots++;
            inventorySlots.Add(child.gameObject);
        }

        // Gets the weapon and the armor slot for later use.
        int iterator = 0;
        foreach(Transform child in GameObject.Find("Equipped").transform)
        {
            if(iterator == 0)
            {
                weaponSlot = child.gameObject;
            }
            if(iterator == 1)
            {
                armorSlot = child.gameObject;
            }
            iterator++;
        }

        // Sets the weapon and the armor slot to the default sprites.
        weaponSlot.GetComponent<Image>().sprite = defaultSprite;
        armorSlot.GetComponent<Image>().sprite = defaultSprite;

        // Sets that the inventory needs to be updated.
        needToUpdateInventoryUI = true;
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
        List<int?> playerItemStack = playerInventory.GetItemStacks();
        // If the player has items in their inventory then update all items.
        if (playerInventoryItems.Count > 0)
        {
            // Updates everything in the UI.
            for (int i = 0; i < playerInventoryItems.Count; i++)
            {
                if (playerInventoryItems[i] == null)
                {
                    UpdateSlotImage(inventorySlots[i],
                        defaultSprite);
                    UpdateSlotNumber(inventorySlots[i], 0);
                    inventorySlots[i].GetComponent<ItemInSlot>().itemInSlot = null;
                    continue;
                }
                // Updates the image of the slots of the UI.
                UpdateSlotImage(inventorySlots[i], playerInventoryItems[i].GetComponent<SpriteRenderer>().sprite);
                inventorySlots[i].GetComponent<ItemInSlot>().itemInSlot = playerInventory[i];
                // Updates the stack number for the UI.
                UpdateSlotNumber(inventorySlots[i], (int)playerItemStack[i]);
                inventorySlots[i].GetComponent<ItemInSlot>().stackAmount = (int)playerItemStack[i];
            }
        }

        // If the armor or the weapon slot is null to set to the default sprite.
        if(playerInventory.EquippedWeapon == null)
        {
            weaponSlot.GetComponent<Image>().sprite = defaultSprite;
        }
        if(playerInventory.EquippedArmor == null)
        {
            armorSlot.GetComponent<Image>().sprite = defaultSprite;
        }

        // Sets that the UI doesn't need to update anymore.
        needToUpdateInventoryUI = false;
    }

    private void UpdateSlotImage(GameObject slot, Sprite inventoryItemSprite)
    {
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
        // Mark that the inventory UI to be updated.
        needToUpdateInventoryUI = true;
        // Removes the item at the index from the players inventory.
        if (playerInventory.GetItemStacks()[index] - 1 <= 0)
        {
            playerInventory.GetInventoryItems().Remove(playerInventory.GetInventoryItems()[index]);
            playerInventory.GetItemStacks().Remove(playerInventory.GetItemStacks()[index]);
        }
        // Decreases the stack amount for the item.
        playerInventory.GetItemStacks()[index]--;
    }

    // Attaches an item to the mouse.
    public void AttachItemToMouse(string slot)
    {
        // If there is an item on the mouse or and item was just moved, then dont run this function.
        if (itemOnMouse != null || 
            justMovedItem)
        {
            // Set the tag of moving an item to false.
            justMovedItem = false;
            return;
        }

        // Declare the texture of the mouse for later use.
        Texture2D textureOfItemOnMouse;

        // If the slot is an integer.
        if(Int32.TryParse(slot, out int index))
        {
            if(playerInventory.GetInventoryItems()[index] == null)
            {
                return;
            }

            // Set the item of the mouse to the item in the player's inventory of that index and the stack of the item onto the mouse.
            itemOnMouse = playerInventory.GetInventoryItems()[index];
            stackOnMouse = (int)playerInventory.GetItemStacks()[index];

            // Set the texture of the mouse.
            textureOfItemOnMouse = itemOnMouse.GetComponent<SpriteRenderer>().sprite.texture;
            // Set the cursor to the texture.
            Cursor.SetCursor(textureOfItemOnMouse, Vector2.zero, CursorMode.Auto);

            // Set that item in the inventory to null.
            playerInventory.GetInventoryItems()[index] = null;
            // Set the stack amount of that slot to 0.
            playerInventory.GetItemStacks()[index] = 0;
            // Mark that the inventory needs to be updated.
            needToUpdateInventoryUI = true;
            return;
        }

        // If the slot is the weapon slot.
        if (slot == "Weapon")
        {
            //Attach the item in the weapon slot to the mouse and set the stack to one.
            itemOnMouse = playerInventory.EquippedWeapon;
            stackOnMouse = 1;

            // Set the texture of the mouse.
            textureOfItemOnMouse = itemOnMouse.GetComponent<SpriteRenderer>().sprite.texture;
            // Set the cursor to the texture.
            Cursor.SetCursor(textureOfItemOnMouse, Vector2.zero, CursorMode.Auto);

            // Set the players equipped weapon to null.
            playerInventory.EquippedWeapon = null;
        }

        // If the slot is the armor slot.
        if (slot == "Armor")
        {
            //Attach the item in the armor slot to the mouse and set the stack to one.
            itemOnMouse = playerInventory.EquippedArmor;
            stackOnMouse = 1;

            // Set the texture of the mouse.
            textureOfItemOnMouse = itemOnMouse.GetComponent<SpriteRenderer>().sprite.texture;
            // Set the cursor to the texture.
            Cursor.SetCursor(textureOfItemOnMouse, Vector2.zero, CursorMode.Auto);

            // Set the players equipped weapon to null.
            playerInventory.EquippedArmor = null;
        }
        // Mark that the inventory needs to be updated.
        needToUpdateInventoryUI = true;
    }

    public void MoveItemToSlot(string slot)
    {
        // If there isn't an item on the mouse, don't run it.
        if (itemOnMouse == null)
            return;

        // If the slot is an integer.
        if (Int32.TryParse(slot, out int slotIndex))
        {
            // If there isn't an item in the slot of the inventory.
            if (playerInventory.GetInventoryItems()[slotIndex] == null)
            {
                // Set the item in that slot of the inventory to the item and the stack amount on the mouse.
                playerInventory.GetInventoryItems()[slotIndex] = itemOnMouse;
                playerInventory.GetItemStacks()[slotIndex] = stackOnMouse;

                // Set the cursor to the default cursor.
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

                // Set the item in the slot to the item and the stack amount on the mouse.
                inventorySlots[slotIndex].GetComponent<ItemInSlot>().itemInSlot = itemOnMouse;
                inventorySlots[slotIndex].GetComponent<ItemInSlot>().stackAmount = stackOnMouse;

                // Set the item on the mouse and the stack amount to default.
                itemOnMouse = null;
                stackOnMouse = 0;

                // Set that an item was just moved.
                justMovedItem = true;
                // Set that the UI needs to be updated.
                needToUpdateInventoryUI = true;
            }
            else
            {
                // Get the item in the slot.
                ItemInSlot itemsInSlot = inventorySlots[slotIndex].GetComponent<ItemInSlot>();

                // Set the temporary item and the temporary stack amount for later use.
                itemsInSlot.tempItemInSlot = itemsInSlot.itemInSlot;
                itemsInSlot.tempStackAmount = itemsInSlot.stackAmount;

                // Set the player inventory item and the stack amount to what is on the mouse.
                playerInventory.GetInventoryItems()[slotIndex] = itemOnMouse;
                playerInventory.GetItemStacks()[slotIndex] = stackOnMouse;

                // Get the new item to put on the mouse.
                GameObject itemToPutOnMouse = itemsInSlot.tempItemInSlot;
                // Get the sprite to set the mouse to.
                Texture2D mouseSprite = itemToPutOnMouse.GetComponent<SpriteRenderer>().sprite.texture;
                // Set the mouse to the sprite.
                Cursor.SetCursor(mouseSprite, Vector2.zero, CursorMode.Auto);

                // Set the slot item and stack amout to what is on the mouse.
                itemsInSlot.itemInSlot = itemOnMouse;
                itemsInSlot.stackAmount = stackOnMouse;

                // Attach the old item and the stack amount to the mouse.
                itemOnMouse = itemsInSlot.tempItemInSlot;
                stackOnMouse = itemsInSlot.tempStackAmount;

                // Reset the temporary slot information.
                itemsInSlot.tempItemInSlot = null;
                itemsInSlot.tempStackAmount = 0;

                // Flag that an item was just moved.
                justMovedItem = true;
                // Flag that the inventory needs to be updated.
                needToUpdateInventoryUI = true;
            }
            return;
        }

        // Checks to see if an item was placed in a slot.
        bool itemPlacedInSlot = false;

        // Gets the item component of the item.
        itemOnMouse.TryGetComponent(out Item itemGrabbed);
        // If the item is a weapon.
        if (itemGrabbed is Weapon && slot == "Weapon")
        {
            // Sets the sprite of the weapon slot to the item on the mouse.
            weaponSlot.GetComponent<Image>().sprite =
                itemOnMouse.GetComponent<SpriteRenderer>().sprite;
            // Resets the mouse sprite.
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

            // Sets the item and the stacks in the weapon slot to what is on the mouse.
            weaponSlot.GetComponent<ItemInSlot>().itemInSlot = itemOnMouse;
            weaponSlot.GetComponent<ItemInSlot>().stackAmount = stackOnMouse;

            // Sets the players equipped weapon to what is on the mouse.
            playerInventory.EquippedWeapon = itemOnMouse;

            // Resets mouse item and stack amount on the mouse to default values.
            itemOnMouse = null;
            stackOnMouse = 0;

            // Sets that an item was places in a slot.
            itemPlacedInSlot = true;
        }
        // If the item is armor.
        if (itemGrabbed is Armor && slot == "Armor")
        {
            // Sets the sprite of the armor slot to the item on the mouse.
            armorSlot.GetComponent<Image>().sprite =
                itemOnMouse.GetComponent<SpriteRenderer>().sprite;

            // Resets the mouse sprite.
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

            // Sets the item and the stacks in the armor slot to what is on the mouse.
            armorSlot.GetComponent<ItemInSlot>().itemInSlot = itemOnMouse;
            armorSlot.GetComponent<ItemInSlot>().stackAmount = stackOnMouse;

            // Sets the players equipped armor to what is on the mouse.
            playerInventory.EquippedArmor = itemOnMouse;

            // Resets mouse item and stack amount on the mouse to default values.
            itemOnMouse = null;
            stackOnMouse = 0;

            // Sets that an item was places in a slot.
            itemPlacedInSlot = true;
        }

        // If an item was place in a slot.
        if(itemPlacedInSlot)
        {
            // Mark that an item was just moved and that the inventory needs to be updated..
            justMovedItem = true;
            needToUpdateInventoryUI = true;
        }
    }
}