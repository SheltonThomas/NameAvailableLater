using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewInventoryBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    // Used to check to see if the inventory needs to be updated.
    private bool needToUpdateUI;

    // Used to set a default slot image.
    [SerializeField]
    private Sprite defaulSlotSprite;

    // Used to get the inventory slots.
    [SerializeField]
    private GameObject slotsArea;
    private List<GameObject> inventorySlots;

    // Used to get the equip weapon and armor slots.
    [SerializeField]
    private GameObject weaponSlot, armorSlot;

    // Gets the amount of slots in the player's inventory.
    private int slotCount;

    // Holds the players inventory items and stack information.
    private List<GameObject> inventoryItems;
    private List<int> inventoryStacks;

    // The players equipped weapon and armor
    public GameObject EquippedWeapon { get; set; }
    public GameObject EquippedArmor { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        inventoryItems = new List<GameObject>();
        inventoryStacks = new List<int>();
        inventorySlots = new List<GameObject>();

        slotCount = 0;
        foreach(Transform inventorySlot in slotsArea.transform)
        {
            slotCount++;
            inventorySlots.Add(inventorySlot.gameObject);
        }

        for(int i = 0; i < slotCount; i++)
        {
            inventoryItems.Add(null);
            inventoryStacks.Add(0);
        }

        needToUpdateUI = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (needToUpdateUI)
            UpdateUI();
    }

    private void UpdateUI()
    {
        for(int index = 0; index < slotCount; index++)
        {
            if(inventoryItems[index] == null)
            {
                UpdateSlotImage(inventorySlots[index], defaulSlotSprite);
                UpdateSlotNumber(inventorySlots[index], 0);
                continue;
            }
            Sprite slotSprite = inventoryItems[index].GetComponent<SpriteRenderer>().sprite;
            UpdateSlotImage(inventorySlots[index], slotSprite);
            UpdateSlotNumber(inventorySlots[index], inventoryStacks[index]);
        }

        if(EquippedWeapon == null)
        {
            weaponSlot.GetComponent<Image>().sprite = defaulSlotSprite;
        }
        else
        {
            Sprite weaponSlotSprite = EquippedWeapon.GetComponent<SpriteRenderer>().sprite;
            weaponSlot.GetComponent<Image>().sprite = weaponSlotSprite;
        }

        if (EquippedArmor == null)
        {
            armorSlot.GetComponent<Image>().sprite = defaulSlotSprite;
        }
        else
        {
            Sprite armorSlotSprite = EquippedArmor.GetComponent<SpriteRenderer>().sprite;
            weaponSlot.GetComponent<Image>().sprite = armorSlotSprite;
        }
        needToUpdateUI = false;
    }

    private void UpdateSlotImage(GameObject slot, Sprite slotSprite)
    {
        slot.GetComponent<Image>().sprite = slotSprite;
    }

    private void UpdateSlotNumber(GameObject slot, int stackAmount)
    {
        foreach(Transform slotText in slot.transform)
        {
            slotText.gameObject.GetComponent<Text>().text = stackAmount.ToString();
            return;
        }
    }

    public void AddToInventory(Collider2D objectCollider)
    {
        GameObject objectToAdd = GameVariables.prefabs[objectCollider.gameObject.name];
        int? index = CheckForItemInInventory(objectToAdd);
        if (index != null)
        {
            inventoryStacks[(int)index]++;
            needToUpdateUI = true;
            return;
        }

        for(int slotIndex = 0; slotIndex < inventoryItems.Count; slotIndex++)
        {
            if (inventoryItems[slotIndex] != null)
            {
                continue;
            }
            inventoryItems[slotIndex] = objectToAdd;
            inventoryStacks[slotIndex]++;
            break;
        }
        needToUpdateUI = true;
    }

    private int? CheckForItemInInventory(GameObject objectToLookFor)
    {
        for(int index = 0; index < slotCount; index++)
        {
            if(inventoryItems[index] == objectToLookFor)
            {
                return index;
            }
        }
        return null;
    }
}
