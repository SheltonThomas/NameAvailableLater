using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    //Health modifiers?
    public int maxHealth = 100; //maximum health
    public int startingHealth = 100; //starting health
    int currentHealth; //current health

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; //armor?
        //Debug.Log("The enemy took " + damage + " damage");
        //play hurt animation
        //play hurt sound

        if (currentHealth <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        //Debug.Log("Enemy Died");
        //Death animation
        //Death sound
        //Drop loot
        Destroy(gameObject);
    }
}
