using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBehavior : MonoBehaviour
{
    public Stats playerStats;

    public InventoryItems playerInventory;

    private void Start()
    {
        playerInventory.inventoryUI = GameObject.Find("Inventory").GetComponent<InventoryBehavior>();
        playerInventory.Clear();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.TryGetComponent(out Item test))
        {
            playerInventory.AddItemToInventory(other.gameObject);
            Destroy(other.gameObject);
        }
    }
}
