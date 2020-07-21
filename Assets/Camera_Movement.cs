using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    public GameObject player;

    Vector2 playerPosition;
    Vector3 cameraPosition;
    

    // Update is called once per frame
    void Update()
    {
        playerPosition = player.transform.position;
        cameraPosition = transform.position;

        cameraPosition.x = playerPosition.x;
        cameraPosition.y = playerPosition.y;

        transform.position = cameraPosition;
    }
}
