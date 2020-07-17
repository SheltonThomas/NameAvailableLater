using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItems : MonoBehaviour
{
    public List<GameObject> Inventory;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Added to inventory");
        if (other.gameObject.tag == "Item")
        {
            Inventory.Add(other.gameObject);
        }
    }
}
