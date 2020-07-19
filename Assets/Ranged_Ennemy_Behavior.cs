using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged_Ennemy_Movement : MonoBehaviour
{

    public Transform firePoint;
    public GameObject arrowPrefab;
    public float fireDelta = 1.5f;
    public float arrowForce = 10f;
    private float myTime;

    public Rigidbody2D ennemyRigidBody;

    public float movementSpeed = 5f;

    public Rigidbody2D playerRigidBody;


    // Update is called once per frame
    void Update()
    {
        myTime += Time.deltaTime;

        Vector2 bowPosition = firePoint.position;

        Vector2 vectorFromEnnemyToPlayer = playerRigidBody.position - bowPosition;

        float distanceFromPlayer = vectorFromEnnemyToPlayer.magnitude;

        if (distanceFromPlayer < 15)
        {
            ennemyRigidBody.rotation = Mathf.Atan2(vectorFromEnnemyToPlayer.y, vectorFromEnnemyToPlayer.x) * Mathf.Rad2Deg + 90f;
            if (myTime > fireDelta)
            {
                Shoot();
                myTime = 0.0f;
            }
            if (distanceFromPlayer > 10)
            {
                vectorFromEnnemyToPlayer.Normalize();
                ennemyRigidBody.MovePosition(ennemyRigidBody.position + vectorFromEnnemyToPlayer * movementSpeed * Time.deltaTime);
            }
            if (distanceFromPlayer < 5)
            {
                vectorFromEnnemyToPlayer.Normalize();
                ennemyRigidBody.MovePosition(ennemyRigidBody.position + vectorFromEnnemyToPlayer * -movementSpeed * Time.deltaTime);
            }


        }
    }

    void Shoot()
    {
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * arrowForce, ForceMode2D.Impulse);

    }
}
