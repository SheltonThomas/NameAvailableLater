using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVariables : MonoBehaviour
{
    // Creates a list of prefabs for use later.
    public static Dictionary<string, GameObject> prefabs;

    private void Awake()
    {
        prefabs = new Dictionary<string, GameObject>();

        GameObject[] loadedObjects = Resources.LoadAll<GameObject>("Items");

        foreach (GameObject iterator in loadedObjects)
        {
            prefabs.Add(iterator.name, iterator);
        }
    }
}
