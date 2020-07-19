using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed = 10f;
    public float dashDistance = 3f;

    public Rigidbody2D rigidBody; //Player character's rigid body used to control where the character is
    public Animator animator;
    public Camera cam;

    bool spacePressed;

    Vector2 movement;
    Vector2 mousePosition;
    Vector2 playerDirection;

    //float playerAngle;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        spacePressed = Input.GetKeyDown("space"); //True if the player started pressing space on that frame. Needs to be kept in Update because this state gets reset each frame

        if (spacePressed)
        {
            playerDirection.Normalize();
            rigidBody.MovePosition(rigidBody.position + dashDistance * playerDirection); //Dashing a set distance in the direction the player is looking in
        }
        else
        {
            rigidBody.MovePosition(rigidBody.position + movement * movementSpeed * Time.deltaTime);
        }

        animator.SetFloat("Speed", movement.sqrMagnitude);

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition); //Gets mouse position and transfers it from pixel coordinates to world units
    }

    void FixedUpdate()
    {
        playerDirection = mousePosition - rigidBody.position; //Finding the vector starting from the player to the mouse position
        float playerAngle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg + 90f; //Getting the angle that the vector creates to rotate the character towards the mouse
        rigidBody.rotation = playerAngle;

    }
}
