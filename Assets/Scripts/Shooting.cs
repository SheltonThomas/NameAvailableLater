using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    //old implementation //public Transform firePoint;
    public GameObject arrowPrefab;
    public float fireDelta = 1.0f;
    Vector2 playerPosition;
    Vector3 rotation;
    public float arrowForce = 10f;
    private float timeSinceLastShot;
    Vector2 playerToMouseDirection; //direction from the player to the mouse
    Player_Attack playerAttack;
    PlayerMovement playerMovement;


    private void Start()
    {
        playerAttack = gameObject.GetComponent<Player_Attack>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (Input.GetButtonDown("Fire2") && timeSinceLastShot > fireDelta && playerAttack.GetTimeSinceLastAttack() > playerAttack.actionDelta)
        {
            playerToMouseDirection = playerMovement.GetPlayerToMouseDirection();
            playerToMouseDirection.Normalize();
            rotation.z = Mathf.Atan2(playerToMouseDirection.y, playerToMouseDirection.x) * Mathf.Rad2Deg;
            playerMovement.playerAngle = rotation.z + 180f;
            Shoot();
            timeSinceLastShot = 0.0f;
        }
    }

    void Shoot()
    {
        //play animation
        //play sound
        playerPosition = gameObject.transform.position;
        Vector2 instantiationPosition = playerPosition + playerToMouseDirection;
        GameObject arrow = Instantiate(arrowPrefab, instantiationPosition, Quaternion.Euler(rotation)); //instantiate the arrow at a distance of 1 from the player in the direction of the mouse, rotation is converted to quaternions for the function
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.AddForce(playerToMouseDirection * arrowForce, ForceMode2D.Impulse); //adding force to the arrow in the direction it was shot (the direction of the mouse at the time it was shot)

    }

    public float GetTimeSinceLastShot()
    {
        return timeSinceLastShot;
    }
}
