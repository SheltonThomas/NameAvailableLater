using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NonPlayerCharacter : MonoBehaviour
{
    protected int currentHealth;
    protected int maximumHealth;
    protected float movementSpeed;
    protected int startingHealth;
    protected Vector2 movement; //current direction the character is going in
    //protected Animator animator;
    
    protected bool attackOnSight;
    protected bool attackIfHit;
    //protected float angle; //angle the character is facing, used by the animator to determine which sprite to use so that the character is facing the correct direction

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    virtual public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
    virtual protected void Die()
    {
        //Debug.Log("Enemy Died");
        //Death animation
        //Death sound
        //Drop loot
        Destroy(gameObject);
    }


}
