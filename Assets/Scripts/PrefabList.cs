using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabList : MonoBehaviour
{
    public static Dictionary<GameObject, int> items;

    private void Start()
    {
        Object[] items = Resources.LoadAll("F:/UnityProjects/Personal/NameAvailableLater/Assets/Prefabs/Weapons");
    }
}
