using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed = 5f;

    public Rigidbody2D rigidbody; //Player character's rigid body used to control where the character is
    public Animator animator;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePosition;
    //Vector2 playerDirection;

    //float playerAngle;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Speed", movement.sqrMagnitude);

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);   //Gets mouse position and transfers it from pixel coordinates to world units
    }

    void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + movement * movementSpeed * Time.fixedDeltaTime);

        Vector2 playerDirection = mousePosition - rigidbody.position; //Finding the vector starting from the player to the mouse position
        float playerAngle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg  + 90f;//getting the angle that the vector creates to rotate the character towards the mouse
        rigidbody.rotation = playerAngle;
    }
}
