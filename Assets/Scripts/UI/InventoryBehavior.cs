using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBehavior : MonoBehaviour
{
    private InventoryItems Inventory;

    // Start is called before the first frame update
    void Start()
    {
        Inventory = GameObject.Find("Player").GetComponent<InventoryItems>();
    }
}
