using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInteractions : MonoBehaviour
{
    [Header("       Set in Inspector")]
    [SerializeField] private KeyCode grabButton = KeyCode.E;
    [SerializeField] private KeyCode dropButton = KeyCode.E;
    [SerializeField] private KeyCode throwButton = KeyCode.R;
    //[SerializeField] private float minThrowForce = 2f;
    [SerializeField] private float maxThrowForce = 10f;
    [SerializeField] private float maxThrowBtnHoldingTime = 2f;
    [SerializeField] GameObject throwChargeBarPrefab;

    [Header("       Set Dynamically"), Space(30)]
    [SerializeField] private List<Item> itemsInZone;
    [SerializeField] private Item equippedItem;
    [SerializeField] private float throwButtonHoldingTime = 0f;


    private GameObject throwChargeBar;


    private void Awake()
    {
        itemsInZone = new List<Item>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item foundedItem = collision.gameObject.GetComponent<Item>();
        if (foundedItem != null)
        {
            int itemIndex = itemsInZone.IndexOf(foundedItem);
            if (itemIndex == -1)
            {
                itemsInZone.Add(foundedItem);

                if (itemsInZone.Count == 1)
                {
                    StartCoroutine(ButtonsClickCheck());
                }
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        Item foundedItem = collision.gameObject.GetComponent<Item>();
        if (foundedItem != null)
        {
            int itemIndex = itemsInZone.IndexOf(foundedItem);
            if (itemIndex >= 0)
            {
                itemsInZone.Remove(foundedItem);
                if (itemsInZone.Count == 0 && equippedItem == null)
                {
                    StopAllCoroutines();
                }
            }
        }
    }


    private IEnumerator ButtonsClickCheck()
    {
        while (true)
        {
            if (Input.GetKeyUp(dropButton))
            {
                if (equippedItem != null)
                {
                    DropEquipedItem();
                }
            }

            if (Input.GetKeyUp(grabButton))
            {
                if (equippedItem == null && itemsInZone.Count > 0)
                {
                    GrabNearestItem();
                }
            }


            if (Input.GetKeyDown(throwButton) && equippedItem != null)
            {
                throwChargeBar = Instantiate(throwChargeBarPrefab, transform);
                throwChargeBar.transform.localPosition = new Vector2(0, 2);
            }

            if (Input.GetKey(throwButton) && equippedItem != null)
            {
                throwButtonHoldingTime += Time.deltaTime;
                throwButtonHoldingTime = Mathf.Min(throwButtonHoldingTime, maxThrowBtnHoldingTime);
                throwChargeBar.transform.localScale = new Vector3(1, throwButtonHoldingTime / maxThrowBtnHoldingTime, 1);
            }

            if (Input.GetKeyUp(throwButton) && equippedItem != null)
            {
                ThrowEquipedItem();
                Destroy(throwChargeBar);
            }
            yield return null;
        }
    }


    private void GrabNearestItem()
    {
        Item nearestItem = GetNearestItem();

        Transform tr = nearestItem.GetComponent<Transform>();
        Rigidbody2D rb = nearestItem.GetComponent<Rigidbody2D>();

        //player rigid body
        transform.parent.gameObject.GetComponent<Rigidbody2D>().mass += rb.mass;


        rb.simulated = false;
        tr.SetParent(transform);
        tr.position = transform.position;
        tr.rotation = new Quaternion(0, 0, 0, 0);

        equippedItem = nearestItem;
    }


    private void DropEquipedItem()
    {
        transform.DetachChildren();
        Rigidbody2D rb = equippedItem.GetComponent<Rigidbody2D>();
        rb.simulated = true;
        transform.parent.gameObject.GetComponent<Rigidbody2D>().mass -= rb.mass;
        equippedItem = null;
    }


    private void ThrowEquipedItem()
    {
        transform.DetachChildren();
        Rigidbody2D rb = equippedItem.GetComponent<Rigidbody2D>();
        rb.simulated = true;


        transform.parent.gameObject.GetComponent<Rigidbody2D>().mass -= rb.mass;


        //player mover check for facing
        if (transform.parent.gameObject.GetComponent<PlayerMover>().facingRight == true)
            rb.AddForce(new Vector2(1 * maxThrowForce * throwButtonHoldingTime / maxThrowBtnHoldingTime, 1 * maxThrowForce * throwButtonHoldingTime / maxThrowBtnHoldingTime));
        else
            rb.AddForce(new Vector2(-1 * maxThrowForce * throwButtonHoldingTime / maxThrowBtnHoldingTime, 1 * maxThrowForce * throwButtonHoldingTime / maxThrowBtnHoldingTime));

        throwButtonHoldingTime = 0;
        equippedItem = null;
    }


    private Item GetNearestItem()
    {
        Item nearestItem = itemsInZone[0];
        float shortestRange = Vector3.Distance(nearestItem.transform.position, transform.position);
        for (int i = 1; i < itemsInZone.Count; i++)
        {
            float range = Vector3.Distance(itemsInZone[i].transform.position, transform.position);
            if (range < shortestRange)
            {
                shortestRange = range;
                nearestItem = itemsInZone[i];
            }
        }
        return nearestItem;
    }
}
