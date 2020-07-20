using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed = 5f;
    public float dashSpeed = 3f;
    float dashTime = 1.0f;
    float timer = 0.0f;
    float dashCooldown = 3.0f;

    public Rigidbody2D rigidBody; //Player character's rigid body used to control where the character is
    public Animator animator;
    public Camera cam;

    bool dashPressed;

    Vector2 movement;
    Vector2 mousePosition;
    Vector2 playerDirection;
    Vector2 dashDirection;
    //Vector2 dash;
    //Vector2 move;

    //float playerAngle;
    //int c=0;


    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        animator.SetFloat("Speed", movement.sqrMagnitude);

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition); //Gets mouse position and transfers it from pixel coordinates to world units

        dashPressed = Input.GetButtonDown("Dash");
        
        timer += Time.deltaTime;

        if (dashPressed && timer > dashCooldown)
        {
            timer = 0;
            playerDirection.Normalize();
            dashDirection = playerDirection;
        }
        

        if(timer < dashTime)
        {
            rigidBody.MovePosition(rigidBody.position + dashSpeed * dashDirection * Time.deltaTime);
        }

        /*
        if (Input.GetButtonDown("Dash"))
        {
            print(c);
            c++;
            print("player direction " + playerDirection);
            print("PLayer position " + rigidBody.position);

            playerDirection.Normalize();

            print("Normalized" + playerDirection);
            Vector2 a = rigidBody.position + dashDistance * playerDirection;
            print("Where it dashes to" + a);

            rigidBody.MovePosition(rigidBody.position + dashDistance * playerDirection); //Dashing a set distance in the direction the player is looking in
            print("PLayer position " + rigidBody.position);
        }else if (movement.magnitude != 0)
        {
            rigidBody.MovePosition(rigidBody.position + movement * movementSpeed * Time.deltaTime);
        }
        */

        /*
        if (Input.GetButtonDown("Dash"))
        {
            playerDirection.Normalize();

            dash = dashDistance * playerDirection;  
        }
        move = rigidBody.position + (movement * movementSpeed * Time.deltaTime) + dash;
        rigidBody.MovePosition(move);
        if (rigidBody.position.Equals(move))
        {
            dash.x = 0;
            dash.y = 0;
        }
        */

    }

    void FixedUpdate()
    {
        
        playerDirection = mousePosition - rigidBody.position; //Finding the vector starting from the player to the mouse position
        float playerAngle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg + 90f; //Getting the angle that the vector creates to rotate the character towards the mouse
        rigidBody.rotation = playerAngle;

        
        if (movement.magnitude != 0 && !dashPressed)
        {
            rigidBody.MovePosition(rigidBody.position + movement * movementSpeed * Time.deltaTime);
        }

        /*
        if (Input.GetButtonDown("Dash"))
        {
            print(c);
            c++;
            print("player direction " + playerDirection);
            print("PLayer position " + rigidBody.position);

            playerDirection.Normalize();

            print("Normalized" + playerDirection);
            Vector2 a = rigidBody.position + dashDistance * playerDirection;
            print("Where it dashes to" + a);

            rigidBody.MovePosition(rigidBody.position + dashDistance * playerDirection); //Dashing a set distance in the direction the player is looking in

        }
        else
        {
            rigidBody.MovePosition(rigidBody.position + movement * movementSpeed * Time.fixedDeltaTime);
        }
        */
    }
}
