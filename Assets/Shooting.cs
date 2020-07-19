using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject arrowPrefab;
    public float fireDelta = 1.0f;

    public float arrowForce = 10f;
    private float myTime;
 
    // Update is called once per frame
    void Update()
    {
        myTime += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && myTime > fireDelta)
        {
            Shoot();
            myTime = 0.0f;
        }
    }

    void Shoot()
    {
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * arrowForce, ForceMode2D.Impulse);

    }
}
