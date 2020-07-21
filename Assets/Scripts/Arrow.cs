using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    //public GameObject hitEffect; // can be used to add a on hit effect
    public GameObject cam;

    void Start()
    {
        cam = GameObject.Find("Main Camera");
    }

    void Update()
    {
        Vector2 arrowPosition = transform.position;
        Vector2 cameraPosition = cam.transform.position;
        Vector2 distance = cameraPosition - arrowPosition;

        if(distance.magnitude > 50) //Destroy the arrow if it is too far offscreen
        {
            Destroy(gameObject);
        }
    }
        
        private void OnCollisionEnter2D(Collision2D collision)
    {
        //Instantiate(hitEffect, transform.position, Quaternion.identity);
        //Destroy(effect, 5f);
        Destroy(gameObject);
    }
}
