using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerCollision : MonoBehaviour
{
    [SerializeField]
    private NewInventoryBehavior playerInventory;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Item tempitem))
        {
            playerInventory.AddToInventory(collision);

            Destroy(collision.gameObject);
        }
    }
}
