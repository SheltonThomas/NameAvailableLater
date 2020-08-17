using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged_Enemy_Behavior : MonoBehaviour
{

    public Transform firePoint;
    public GameObject arrowPrefab;
    public float fireDelta = 1.5f;
    public float arrowForce = 10f;
    private float myTime;

    public Rigidbody2D enemyRigidBody;

    public float movementSpeed = 5f;

    public Rigidbody2D playerRigidBody;

    public int maxHealth = 100; //Health modifiers?
    int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        myTime += Time.deltaTime;

        Vector2 bowPosition = firePoint.position;

        Vector2 vectorFromEnemyToPlayer = playerRigidBody.position - bowPosition;

        float distanceFromPlayer = vectorFromEnemyToPlayer.magnitude;

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
    }

    void Shoot()
    {
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * arrowForce, ForceMode2D.Impulse);

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; //armor?
        //play hurt animation

        if(currentHealth <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        Debug.Log("Enemy Died");
        //Death animation
        //Disable enemy
    }
}
