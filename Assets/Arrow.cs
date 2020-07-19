using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    //public GameObject hitEffect; // can be used to add a on hit effect
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Instantiate(hitEffect, transform.position, Quaternion.identity);
        //Destroy(effect, 5f);
        Destroy(gameObject);
    }
}
