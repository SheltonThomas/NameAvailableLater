using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryItems : MonoBehaviour
{
    public List<GameObject> Inventory;
    public Dictionary<string, GameObject> prefabs;

    private void Awake()
    {
        prefabs = new Dictionary<string, GameObject>();
        GameObject[] loadedObjects = Resources.LoadAll<GameObject>("Items");

        foreach(GameObject iterator in loadedObjects)
        {
            prefabs.Add(iterator.name, iterator);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Item")
        {
            Inventory.Add(prefabs[other.gameObject.name]);
            Destroy(other.gameObject);
        }
    }

    public int Count {
        get
        {
            return Inventory.Count;
        }
    }

    public GameObject this[int index]
    {
        get
        {
            return Inventory[index];
        }
        set
        {
            Inventory[index] = value;
        }
    }
}
