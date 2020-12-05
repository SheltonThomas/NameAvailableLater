using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    //public GameObject hitEffect; // can be used to add a on hit effect
    GameObject cam;
    //GameObject player;
    public int arrowDamage = 10;

    void Start()
    {
        cam = GameObject.Find("Main Camera");
        //player = GameObject.FindWithTag("Player");
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Collision");
        //play sound
        //Instantiate(hitEffect, transform.position, Quaternion.identity);
        //Destroy(effect, 5f);
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("player");
            collision.gameObject.GetComponent<Player_Health>().TakeDamage(arrowDamage);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("enemy");
            collision.gameObject.GetComponent<NonPlayerCharacter>().TakeDamage(arrowDamage);
        }
        Destroy(gameObject);
    }
   
}
