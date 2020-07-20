using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBehavior : MonoBehaviour
{
    private GameObject inventoryArea;
    private List<GameObject> inventorySlots;
    public InventoryItems playerInventory;
    public bool needToUpdateInventoryUI;

    // Start is called before the first frame update
    void Start()
    {
        inventoryArea = GameObject.Find("Canvas");
        inventorySlots = new List<GameObject>();

        foreach(Transform child in inventoryArea.transform)
        {
            inventorySlots.Add(child.gameObject);
        }
    }

    private void Update()
    {
        if(needToUpdateInventoryUI)
        {
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        List<GameObject> playerInventoryItems = playerInventory.GetInventoryItems();
        List<int> playerItemStack = playerInventory.GetItemStacks();
        for(int i = 0; i < playerInventoryItems.Count; i++)
        {
            UpdateSlotImage(inventorySlots[i], playerInventoryItems[i]);
            UpdateSlotNumber(inventorySlots[i], playerItemStack[i]);
        }
        needToUpdateInventoryUI = false;
    }

    private void UpdateSlotImage(GameObject slot, GameObject inventoryItem)
    {
        Sprite inventoryItemSprite = inventoryItem.GetComponent<SpriteRenderer>().sprite;
        slot.GetComponent<Image>().sprite = inventoryItemSprite;
    }

    private void UpdateSlotNumber(GameObject slot, int itemStack)
    {
        foreach(Transform child in slot.transform)
        {
            child.gameObject.GetComponent<Text>().text = itemStack.ToString();
            return;
        }
    }
}
