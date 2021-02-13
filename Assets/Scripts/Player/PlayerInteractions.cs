using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Переделать ItemInZone в список, подбор осуществить ближайшего предмета, а не первого
// вошедшего в триггер

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private KeyCode grabButton = KeyCode.E;
    [SerializeField] private KeyCode throwButton = KeyCode.R;
    [SerializeField] private float maxThrowForce = 10f;
    [SerializeField] private float maxThrowBtnHoldingTime = 2f;
    [SerializeField] GameObject throwChargeBarPrefab;


    private Item itemInZone;
    private Item equippedItem;
    private float throwBtnHoldingTime = 0f;
    private GameObject throwChargeBarGO;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item foundedItem = collision.gameObject.GetComponent<Item>();
        if (foundedItem != null && itemInZone == null)
        {
            itemInZone = foundedItem;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Item foundedItem = collision.gameObject.GetComponent<Item>();
        if (foundedItem != null && foundedItem == itemInZone)
        {
            itemInZone = null;
        }
    }
    private void Update()
    {
        if (Input.GetKeyUp(grabButton))
        {
            if (equippedItem != null)
            {
                DropItem(equippedItem);
            }
            else if (itemInZone != null)
            {
                GrabItem(itemInZone);
            }
        }
        if (Input.GetKeyDown(throwButton) && equippedItem != null)
        {
            throwChargeBarGO = Instantiate(throwChargeBarPrefab,transform);
            throwChargeBarGO.transform.localPosition = new Vector2(0, 2);
        }
        if (Input.GetKey(throwButton) && equippedItem != null)
        {

            throwBtnHoldingTime += Time.deltaTime;
            throwBtnHoldingTime = Mathf.Min(throwBtnHoldingTime, maxThrowBtnHoldingTime);
            throwChargeBarGO.transform.localScale = new Vector3(1, throwBtnHoldingTime / maxThrowBtnHoldingTime, 1);
        }
        if (Input.GetKeyUp(throwButton) && equippedItem != null)
        {
            ThrowItem(equippedItem);
            throwBtnHoldingTime = 0;
            Destroy(throwChargeBarGO);
        }
    }



    private void GrabItem(Item item)
    {
        Transform tr = item.GetComponent<Transform>();
        Rigidbody2D rb = item.GetComponent<Rigidbody2D>();

        //player rigid body
        transform.parent.gameObject.GetComponent<Rigidbody2D>().mass += rb.mass;


        rb.simulated = false;
        tr.SetParent(transform);
        tr.position = transform.position;
        tr.rotation = new Quaternion(0, 0, 0, 0);


        equippedItem = itemInZone;

    }
    private void DropItem(Item item)
    {
        transform.DetachChildren();
        Rigidbody2D rb = item.GetComponent<Rigidbody2D>();

        rb.simulated = true;


        transform.parent.gameObject.GetComponent<Rigidbody2D>().mass -= rb.mass;

        equippedItem = null;
    }
    private void ThrowItem(Item item)
    {
        transform.DetachChildren();
        Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
        rb.simulated = true;


        transform.parent.gameObject.GetComponent<Rigidbody2D>().mass -= rb.mass;


        //player mover check for facing
        if (transform.parent.gameObject.GetComponent<PlayerMover>().facingRight == true)
            rb.AddForce(new Vector2(1 * maxThrowForce * throwBtnHoldingTime / maxThrowBtnHoldingTime, 1 * maxThrowForce * throwBtnHoldingTime / maxThrowBtnHoldingTime));
        else
            rb.AddForce(new Vector2(-1 * maxThrowForce * throwBtnHoldingTime / maxThrowBtnHoldingTime, 1 * maxThrowForce * throwBtnHoldingTime / maxThrowBtnHoldingTime));



        equippedItem = null;
    }
}
