using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed = 5f;
    public float dashSpeed = 20f;
    public float dashTime = 0.2f;
    public float dashCooldown = 1.0f;
    
    float timer = 2.0f; // incremented to know when the dash is not on cooldown and when the dash should end

    public Rigidbody2D rigidBody; //Player character's rigid body used to control where the character is
    public Animator animator;
    public Camera cam;

    bool dashPressed;
    bool dashVelocityReset = false;//used to know if the dash's velocity has been reset since the 
                                   //dash adds velocity to the character and it needs to be set back to 0

    public float playerAngle;

    public bool dashUnlocked = true;

    Vector2 movement;
    Vector2 mousePosition;
    Vector2 playerDirection;
    Vector2 dashDirection;

    // old implementation
    //Vector2 dash;
    //Vector2 move;
    



    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        animator.SetFloat("Speed", movement.sqrMagnitude);
        animator.SetFloat("Rotation", playerAngle);

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition); //Gets mouse position and transfers it from pixel coordinates to world units

        
        if (dashUnlocked) //Checking if the dash has been unlocked
        {
        dashPressed = Input.GetButtonDown("Dash"); //Getting whether or not the dash button has been pressed on this frame
        
        timer += Time.deltaTime; 

            if (dashPressed && timer > dashCooldown) //Checking if the dash is not on cooldown and if the button has been pressed then initiatializes the dash
                {
                    dashVelocityReset = false; //Turning this to false so that it has to be turned back to true after the dash ends
                    timer = 0; //Reseting the cooldown and dash timer (same timer used for both)
                    playerDirection.Normalize(); 
                    dashDirection = playerDirection; //Setting a variable for the direction of the dash that cannot not be changed until the next dash
                    rigidBody.velocity = dashSpeed * dashDirection;
                }
        
            if (timer > dashTime && !dashVelocityReset) //When the dash is over
                {
                    rigidBody.velocity = Vector2.zero;
                    dashVelocityReset = true;
                }
        }
        

        /* // old implementation
        if (Input.GetButtonDown("Dash"))
        {
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
        playerAngle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg + 180f; //Getting the angle that the vector creates to rotate the character towards the mouse
        //rigidBody.rotation = playerAngle; //rotates player model to face the mouse

        
        if (movement.magnitude != 0 && timer > dashTime) //If the player is not dashing and pushed a direction, move the player in that direction
        {
            rigidBody.MovePosition(rigidBody.position + movement * movementSpeed * Time.deltaTime);
        }

        /*// old implementation
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
