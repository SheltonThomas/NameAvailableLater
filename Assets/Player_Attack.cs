using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{

    public Animator animator;

    /*
    public int attackDamage = 20;

    public Transform attackPoint;
    public float attackRange = 0.5;
    public LayerMask enemyLayers;
    */

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            Attack();
        }
    }

    void Attack()
    {
        //Play attack animation
        animator.SetTrigger("Attack");

        //Detect enemies in range
        //Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage enemies
        /*foreach(Collider2D enemy in enemiesHit)
        {
            Debug.Log(enemy.name + " hit")
            enemy.GetComponent<enemy>().TakeDamage(attackDamage;) //multiply attack damage by any modifiers
        }
        */
        
    }

    /*
    void OnDrawGizmosSelected()
    {

        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    */
}
