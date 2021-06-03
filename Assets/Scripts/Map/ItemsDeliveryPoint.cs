using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDeliveryPoint : MonoBehaviour
{
    private List<Item> itemsCollected;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Item>() != null)
        {
            ItemTake(collision.gameObject.GetComponent<Item>());
        }
    }
    private void ItemTake(Item item)
    {
        itemsCollected.Add(item);


        Destroy(item);
    }
}
