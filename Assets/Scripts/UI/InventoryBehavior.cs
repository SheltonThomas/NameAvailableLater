using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBehavior : MonoBehaviour
{
    public int itemsInInventory = 0;
    private InventoryItems Inventory;
    // Start is called before the first frame update
    void Start()
    {
        Inventory = GameObject.Find("Player").GetComponent<InventoryItems>();
    }

    // Update is called once per frame
    void Update()
    {
        itemsInInventory = Inventory.Inventory.Count;
    }
}
