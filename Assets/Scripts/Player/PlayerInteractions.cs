using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private KeyCode grabButton = KeyCode.E;
    [SerializeField] private KeyCode throwButton = KeyCode.R;
    [SerializeField] private float throwForce = 10f;

    private bool isAnyItemsInZone = false;
    private bool isAnyItemInArms = false;
    private GameObject itemInZone;
    private GameObject itemInArms;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ItemsForGrab>() != null && itemInZone == null)
        {
            isAnyItemsInZone = true;
            itemInZone = collision.gameObject;
            print("zalez");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<ItemsForGrab>() != null && collision.gameObject == itemInZone)
        {
            isAnyItemsInZone = false;
            itemInZone = null;
            print("vilez");
        }
    }
    private void Update()
    {
        if (Input.GetKeyUp(grabButton))
        {
            if (isAnyItemInArms)
            {
                DropItem(itemInArms);
            }
            else if (isAnyItemsInZone)
            {
                GrabItem(itemInZone);
            }
        }
        if (Input.GetKeyUp(throwButton) && itemInArms != null)
        {
            ThrowItem(itemInArms);
        }
    }



    private void GrabItem (GameObject item)
    {
        Transform tr = item.GetComponent<Transform>();
        Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
        
        rb.simulated = false;
        tr.SetParent(transform);
        tr.position = transform.position;
        tr.rotation = new Quaternion(0, 0, 0, 0);
        isAnyItemInArms = true;
        itemInArms = itemInZone;
    }
    private void DropItem(GameObject item)
    {
        transform.DetachChildren();
        Rigidbody2D rb = item.GetComponent<Rigidbody2D>();

        rb.simulated = true;
        isAnyItemInArms = false;

        itemInArms = null;
    }
    private void ThrowItem (GameObject item)
    {
        transform.DetachChildren();
        Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
        rb.simulated = true;
        isAnyItemInArms = false;
        rb.AddForce(new Vector2(1 * throwForce, 1 * throwForce));

        itemInArms = null;
    }
}
