using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBehavior : MonoBehaviour
{
    private GameObject inventoryArea;
    private InventoryItems inventory;
    public List<GameObject> inventorySlots;

    // Start is called before the first frame update
    void Start()
    {
        inventoryArea = GameObject.Find("Canvas");
        inventorySlots = new List<GameObject>();
        inventory = GameObject.Find("Player").GetComponent<InventoryItems>();

        foreach(Transform child in inventoryArea.transform)
        {
            inventorySlots.Add(child.gameObject);
        }
    }

    private void Update()
    {
        for(int i = 0; i < inventory.Count; i++)
        {
            Image buttonImage = inventorySlots[i].GetComponent<Image>();
            Sprite buttonSprite = inventory[i].GetComponent<SpriteRenderer>().sprite;
            buttonImage.sprite = buttonSprite;
        }
    }

    public void RemoveItem(int index)
    {
        Image buttonImage = inventorySlots[index].GetComponent<Image>();

        if (GameVariables.prefabs.ContainsKey(buttonImage.sprite.name))
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].name == buttonImage.sprite.name)
                {
                    inventory.RemoveItem(buttonImage.name);
                    break;
                }
            }
            buttonImage.sprite = GameVariables.prefabs["Yeet"].GetComponent<SpriteRenderer>().sprite;
        }
    }
}
