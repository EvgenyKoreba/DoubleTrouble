using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDeliveryPoint : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Item>() != null)
        {
            ItemTake(collision.gameObject);
        }
    }
    private void ItemTake( GameObject item )
    {



        Destroy(item);
    }
}
