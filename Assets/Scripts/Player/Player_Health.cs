using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
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
        
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; //armor?
        //Debug.Log("The player took " + damage + " damage");
        //play hurt animation
        //play hurt sound

        if (currentHealth <= 0)
        {
            Die();
        }

    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }

    void Die()
    {
        //Debug.Log("Player has died");
        //revive if needed
        //play death animation
        //play death sound
        //reset things that need to be reset
        //go to death screen
    }
}
