using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

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
    private GameObject itemOnMouse;
    private int stackOnMouse;
    [SerializeField]
    private Sprite defaultSprite;
    private GameObject weaponSlot, armorSlot;
    private bool justMovedItem = false;


    void Start()
    {
        // Gets the inventory's canvas.
        inventoryArea = GameObject.Find("Slots");
        // Initializes inventory slots.
        inventorySlots = new List<GameObject>();

        // Sets the inventory slots to the slots on the canvas.
        foreach(Transform child in inventoryArea.transform)
        {
            inventorySlots.Add(child.gameObject);
        }

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

        weaponSlot.GetComponent<Image>().sprite = defaultSprite;
        armorSlot.GetComponent<Image>().sprite = defaultSprite;

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
                    continue;
                }
                // Updates the image of the slots of the UI.
                UpdateSlotImage(inventorySlots[i], playerInventoryItems[i].GetComponent<SpriteRenderer>().sprite);
                // Updates the stack number for the UI.
                UpdateSlotNumber(inventorySlots[i], (int)playerItemStack[i]);
            }
        }

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

    public void AttachItemToMouse(string slot)
    {
        if (itemOnMouse != null || 
            justMovedItem)
        {
            justMovedItem = false;
            return;
        }
        Texture2D textureOfItemOnMouse;

        if(Int32.TryParse(slot, out int index))
        {
            itemOnMouse = playerInventory.GetInventoryItems()[index];

            textureOfItemOnMouse =
            GameVariables.prefabs[itemOnMouse.name].GetComponent<SpriteRenderer>().sprite.texture;

            Cursor.SetCursor(textureOfItemOnMouse, Vector2.zero, CursorMode.Auto);
            playerInventory.GetInventoryItems()[index] = null;
            stackOnMouse = (int)playerInventory.GetItemStacks()[index];
            playerInventory.GetItemStacks()[index] = 0;
            needToUpdateInventoryUI = true;
            return;
        }

        if (slot == "Weapon")
        {
            itemOnMouse = playerInventory.EquippedWeapon;

            textureOfItemOnMouse =
            GameVariables.prefabs[itemOnMouse.name].GetComponent<SpriteRenderer>().sprite.texture;

            Cursor.SetCursor(textureOfItemOnMouse, Vector2.zero, CursorMode.Auto);
            playerInventory.EquippedWeapon = null;
        }

        if (slot == "Armor")
        {
            itemOnMouse = playerInventory.EquippedArmor;

            textureOfItemOnMouse =
            GameVariables.prefabs[itemOnMouse.name].GetComponent<SpriteRenderer>().sprite.texture;

            Cursor.SetCursor(textureOfItemOnMouse, Vector2.zero, CursorMode.Auto);
            playerInventory.EquippedArmor = null;
        }
        needToUpdateInventoryUI = true;
    }

    public void MoveItemToSlot(string slot)
    {
        if (itemOnMouse == null)
            return;

        if(Int32.TryParse(slot, out int slotIndex))
        {
            playerInventory.GetInventoryItems()[slotIndex] = itemOnMouse;
            playerInventory.GetItemStacks()[slotIndex] = stackOnMouse;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            inventorySlots[slotIndex].GetComponent<ItemInSlot>().itemInSlot = itemOnMouse;
            itemOnMouse = null;
            justMovedItem = true;
            needToUpdateInventoryUI = true;
            return;
        }

        bool itemPlacedInSlot = false;

        itemOnMouse.TryGetComponent(out Item itemGrabbed);
        if (itemGrabbed is Weapon && slot == "Weapon")
        {
            weaponSlot.GetComponent<Image>().sprite =
                itemOnMouse.GetComponent<SpriteRenderer>().sprite;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            weaponSlot.GetComponent<ItemInSlot>().itemInSlot = itemOnMouse;
            playerInventory.EquippedWeapon = itemOnMouse;
            itemPlacedInSlot = true;
        }
        if (itemGrabbed is Armor && slot == "Armor")
        {
            armorSlot.GetComponent<Image>().sprite =
                itemOnMouse.GetComponent<SpriteRenderer>().sprite;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            armorSlot.GetComponent<ItemInSlot>().itemInSlot = itemOnMouse;
            playerInventory.EquippedArmor = itemOnMouse;
            itemPlacedInSlot = true;
        }
        if(itemPlacedInSlot)
        {
            itemOnMouse = null;
            justMovedItem = true;
            itemPlacedInSlot = false;
        }
        needToUpdateInventoryUI = true;
    }
}
