using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehavior : MonoBehaviour
{
    private Slider healthUI;
    private PlayerStats player;
    // Start is called before the first frame update
    void Start()
    {
        healthUI = this.gameObject.GetComponent<Slider>();
        player = GameObject.Find("Player").GetComponent<PlayerStats>();
        healthUI.maxValue = player.GetPlayerHealth();
        healthUI.value = player.GetPlayerHealth();
    }

    // Update is called once per frame
    void Update()
    {
        healthUI.value = player.GetPlayerHealth();
    }
}
