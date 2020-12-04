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

    public float playerAngle; //angle the player is facing, used by the animator to determine which sprite to use so that the player is facing the correct direction

    public bool dashUnlocked = true;

    Vector2 movement; //current direction the player is going
    Vector2 mousePosition; //position of the mouse
    Vector2 playerToMouseDirection; //direction from the player to the mouse
    Vector2 dashDirection;

    // old implementation
    //Vector2 dash;
    //Vector2 move;

    public Vector2 GetPlayerToMouseDirection()
    {
        return playerToMouseDirection;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        animator.SetFloat("Speed", movement.sqrMagnitude);
        animator.SetFloat("Rotation", playerAngle);

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition); //Gets mouse position and transfers it from pixel coordinates to world units
        playerToMouseDirection = mousePosition - rigidBody.position; //Finding the vector starting from the player to the mouse position

        if (dashUnlocked) //Checking if the dash has been unlocked
        {
            dashPressed = Input.GetButtonDown("Dash"); //Getting whether or not the dash button has been pressed on this frame

            timer += Time.deltaTime;

            if (dashPressed && timer > dashCooldown) //Checking if the dash is not on cooldown and if the button has been pressed then initiatializes the dash
            {
                dashVelocityReset = false; //Turning this to false so that it has to be turned back to true after the dash ends
                timer = 0; //Reseting the cooldown and dash timer (same timer used for both)
                           //dashing towards mouse:
                           //playerToMouseDirection.Normalize(); 
                           //dashDirection = playerToMouseDirection; //Setting a variable for the direction of the dash that cannot not be changed until the next dash

                if (movement.magnitude != 0)
                {
                    dashDirection = movement; //if the player is moving dash in that direction
                }
                else //if the player is not moving dash in the direction they are facing
                {
                    dashDirection.x = Mathf.Round(Mathf.Sin(Mathf.Round(playerAngle / 45) * 0.79f - 1.57f));//Since the dash only works in 8 directions angles 22.5-67.5 need to all give the same output of (-1,-1) as well and similarly for the other angles
                    dashDirection.y = Mathf.Round(Mathf.Sin(Mathf.Round(playerAngle / 45) * 0.79f + 3.14f));//Dividing by 45 and rounding makes it so that each set of angles is now a digit any angle 22.5-67.5 would become 1 this groups the angles so that they can easily be used as input points on a sine fucntion to get the necessary outputs (the multipication and addition are there to match the sine function to the points needed) the rounding at the end is so that the output will be either 1 or 0
                }
                //Debug.Log(dashDirection);
                dashDirection.Normalize();
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
            print("player direction " + playerToMouseDirection);
            print("PLayer position " + rigidBody.position);

            playerToMouseDirection.Normalize();

            print("Normalized" + playerToMouseDirection);
            Vector2 a = rigidBody.position + dashDistance * playerToMouseDirection;
            print("Where it dashes to" + a);

            rigidBody.MovePosition(rigidBody.position + dashDistance * playerToMouseDirection); //Dashing a set distance in the direction the player is looking in
            print("PLayer position " + rigidBody.position);
        }else if (movement.magnitude != 0)
        {
            rigidBody.MovePosition(rigidBody.position + movement * movementSpeed * Time.deltaTime);
        }
        */

        /*
        if (Input.GetButtonDown("Dash"))
        {
            playerToMouseDirection.Normalize();

            dash = dashDistance * playerToMouseDirection;  
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
        //2nd implementation//playerAngle = Mathf.Atan2(playerToMouseDirection.y, playerToMouseDirection.x) * Mathf.Rad2Deg + 180f; //Getting the angle that the vector creates to rotate the character towards the mouse

        //1st implementation//rigidBody.rotation = playerAngle; //rotates player model to face the mouse

        if (movement.magnitude != 0 && timer > dashTime) //If the player is not dashing and pushed a direction, move the player in that direction
        {
            playerAngle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg + 180f;
            rigidBody.MovePosition(rigidBody.position + movement * movementSpeed * Time.deltaTime);
        }

        /*// old implementation
        if (Input.GetButtonDown("Dash"))
        {
            print(c);
            c++;
            print("player direction " + playerToMouseDirection);
            print("PLayer position " + rigidBody.position);

            playerToMouseDirection.Normalize();

            print("Normalized" + playerToMouseDirection);
            Vector2 a = rigidBody.position + dashDistance * playerToMouseDirection;
            print("Where it dashes to" + a);

            rigidBody.MovePosition(rigidBody.position + dashDistance * playerToMouseDirection); //Dashing a set distance in the direction the player is looking in

        }
        else
        {
            rigidBody.MovePosition(rigidBody.position + movement * movementSpeed * Time.fixedDeltaTime);
        }
        */
    }
}
