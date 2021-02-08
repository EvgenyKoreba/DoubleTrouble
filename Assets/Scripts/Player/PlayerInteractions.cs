using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private KeyCode grabButton = KeyCode.E;
    [SerializeField] private KeyCode throwButton = KeyCode.R;
    [SerializeField] private float maxThrowForce = 10f;
    [SerializeField] private float maxThrowBtnHoldingTime = 2f;
    [SerializeField] GameObject throwChargeBarPrefab;

    private bool isAnyItemsInZone = false;
    private bool isAnyItemInArms = false;
    private GameObject itemInZone;
    private GameObject itemInArms;
    private float throwBtnHoldingTime = 0f;
    private GameObject throwChargeBarGO;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ItemsForGrab>() != null && itemInZone == null)
        {
            isAnyItemsInZone = true;
            itemInZone = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<ItemsForGrab>() != null && collision.gameObject == itemInZone)
        {
            isAnyItemsInZone = false;
            itemInZone = null;
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
        if (Input.GetKeyDown(throwButton) && itemInArms != null)
        {
            throwChargeBarGO = Instantiate(throwChargeBarPrefab);
            throwChargeBarGO.transform.SetParent(transform);
            throwChargeBarGO.transform.localPosition = new Vector2(0, 2);
        }
        if (Input.GetKey(throwButton) && itemInArms != null)
        {

            throwBtnHoldingTime += Time.deltaTime;
            if (throwBtnHoldingTime > maxThrowBtnHoldingTime)
                throwBtnHoldingTime = maxThrowBtnHoldingTime;
            throwChargeBarGO.transform.localScale = new Vector3(1, throwBtnHoldingTime / maxThrowBtnHoldingTime, 1);
        }
        if (Input.GetKeyUp(throwButton) && itemInArms != null)
        {
            ThrowItem(itemInArms);
            throwBtnHoldingTime = 0;
            Destroy(throwChargeBarGO);
        }
    }



    private void GrabItem(GameObject item)
    {
        Transform tr = item.GetComponent<Transform>();
        Rigidbody2D rb = item.GetComponent<Rigidbody2D>();

        //player rigid body
        transform.parent.gameObject.GetComponent<Rigidbody2D>().mass += rb.mass;


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


        transform.parent.gameObject.GetComponent<Rigidbody2D>().mass -= rb.mass;

        itemInArms = null;
    }
    private void ThrowItem(GameObject item)
    {
        transform.DetachChildren();
        Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
        rb.simulated = true;
        isAnyItemInArms = false;


        transform.parent.gameObject.GetComponent<Rigidbody2D>().mass -= rb.mass;


        //player mover check for facing
        if (transform.parent.gameObject.GetComponent<PlayerMover>().facingRight == true)
            rb.AddForce(new Vector2(1 * maxThrowForce * throwBtnHoldingTime / maxThrowBtnHoldingTime, 1 * maxThrowForce * throwBtnHoldingTime / maxThrowBtnHoldingTime));
        else
            rb.AddForce(new Vector2(-1 * maxThrowForce * throwBtnHoldingTime / maxThrowBtnHoldingTime, 1 * maxThrowForce * throwBtnHoldingTime / maxThrowBtnHoldingTime));



        itemInArms = null;
    }
}
