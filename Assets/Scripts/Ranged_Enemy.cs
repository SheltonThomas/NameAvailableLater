using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged_Enemy : NonPlayerCharacter
{

    public Transform firePoint;
    public GameObject arrowPrefab;
    public float fireDelta = 1.5f;
    public float arrowForce = 10f;
    private float myTime;

    public Rigidbody2D enemyRigidBody;
    public Rigidbody2D playerRigidBody;

    private void Start()
    {
        movementSpeed = 5f;
        startingHealth = 100;
        maximumHealth = 100;
        currentHealth = startingHealth;
    }
    // Update is called once per frame
    void Update()
    {
        myTime += Time.deltaTime;

        Vector2 bowPosition = firePoint.position;

        Vector2 vectorFromEnemyToPlayer = playerRigidBody.position - bowPosition;

        float distanceFromPlayer = vectorFromEnemyToPlayer.magnitude;
        //LIMIT HOW FAST HE CAN TURN
        if (distanceFromPlayer < 15)
        {
            enemyRigidBody.rotation = Mathf.Atan2(vectorFromEnemyToPlayer.y, vectorFromEnemyToPlayer.x) * Mathf.Rad2Deg + 90f;
            if (myTime > fireDelta)
            {
                Shoot();
                myTime = 0.0f;
            }
            if (distanceFromPlayer > 10)
            {
                vectorFromEnemyToPlayer.Normalize();
                enemyRigidBody.MovePosition(enemyRigidBody.position + vectorFromEnemyToPlayer * movementSpeed * Time.deltaTime);
            }
            if (distanceFromPlayer < 5)
            {
                vectorFromEnemyToPlayer.Normalize();
                enemyRigidBody.MovePosition(enemyRigidBody.position + vectorFromEnemyToPlayer * -movementSpeed * Time.deltaTime);
            }


        }
        if (currentHealth > maximumHealth)
        {
            currentHealth = maximumHealth;
        }
    }

    void Shoot()
    {
        //consider having enemies that shoot in the direction the player is heading 
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);// + Random.Range(-1.0f, 1.0f) for slightly random direction
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * arrowForce, ForceMode2D.Impulse);

    }
    
    override public void TakeDamage(int damage)
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

    override protected void Die()
    {
        //Debug.Log("Enemy Died");
        //Death animation
        //Death sound
        //Drop loot
        Destroy(gameObject);
    }

}
