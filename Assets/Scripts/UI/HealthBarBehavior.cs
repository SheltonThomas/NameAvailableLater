using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehavior : MonoBehaviour
{
    [SerializeField]
    private Slider healthUI;
    private PlayerStats player;
    [SerializeField]
    private Button inventoryButton;
    [SerializeField]
    private GameObject playerInventory;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerStats>();
        healthUI.maxValue = player.GetPlayerHealth();
        healthUI.value = player.GetPlayerHealth();
    }

    // Update is called once per frame
    void Update()
    {
        healthUI.value = player.GetPlayerHealth();
    }

    public void TogglePlayerInventory()
    {
        playerInventory.SetActive(!playerInventory.activeInHierarchy);
    }
}
