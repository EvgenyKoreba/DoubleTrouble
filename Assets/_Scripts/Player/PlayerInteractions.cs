using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInteractions : MonoBehaviour
{
    [Header("       Set in Inspector")]
    [SerializeField] private KeyCode _grabButton = KeyCode.E;
    [SerializeField] private KeyCode _dropButton = KeyCode.E;
    [SerializeField] private KeyCode _throwButton = KeyCode.R;
    //[SerializeField] private float minThrowForce = 2f;
    [SerializeField] private float _maxThrowForce = 10f;
    [SerializeField] private float _maxThrowBtnHoldingTime = 2f;
    [SerializeField] private GameObject _throwChargeBarPrefab;

    [Header("       Set Dynamically"), Space(30)]
    [SerializeField] private List<Item> _itemsInZone;
    [SerializeField] private Item _equippedItem;
    [SerializeField] private float _throwButtonHoldingTime = 0f;


    private GameObject _throwChargeBar;


    private void Awake()
    {
        _itemsInZone = new List<Item>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item foundedItem = collision.gameObject.GetComponent<Item>();
        if (foundedItem != null)
        {
            int itemIndex = _itemsInZone.IndexOf(foundedItem);
            if (itemIndex == -1)
            {
                _itemsInZone.Add(foundedItem);

                if (_itemsInZone.Count == 1)
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
            int itemIndex = _itemsInZone.IndexOf(foundedItem);
            if (itemIndex >= 0)
            {
                _itemsInZone.Remove(foundedItem);
                if (_itemsInZone.Count == 0 && _equippedItem == null)
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
            if (Input.GetKeyUp(_dropButton))
            {
                if (_equippedItem != null)
                {
                    DropEquipedItem();
                }
            }

            if (Input.GetKeyUp(_grabButton))
            {
                if (_equippedItem == null && _itemsInZone.Count > 0)
                {
                    GrabNearestItem();
                }
            }


            if (Input.GetKeyDown(_throwButton) && _equippedItem != null)
            {
                _throwChargeBar = Instantiate(_throwChargeBarPrefab, transform);
                _throwChargeBar.transform.localPosition = new Vector2(0, 2);
            }

            if (Input.GetKey(_throwButton) && _equippedItem != null)
            {
                _throwButtonHoldingTime += Time.deltaTime;
                _throwButtonHoldingTime = Mathf.Min(_throwButtonHoldingTime, _maxThrowBtnHoldingTime);
                _throwChargeBar.transform.localScale = new Vector3(1, _throwButtonHoldingTime / _maxThrowBtnHoldingTime, 1);
            }

            if (Input.GetKeyUp(_throwButton) && _equippedItem != null)
            {
                ThrowEquipedItem();
                Destroy(_throwChargeBar);
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

        _equippedItem = nearestItem;
    }


    private void DropEquipedItem()
    {
        transform.DetachChildren();
        Rigidbody2D rb = _equippedItem.GetComponent<Rigidbody2D>();
        rb.simulated = true;
        transform.parent.gameObject.GetComponent<Rigidbody2D>().mass -= rb.mass;
        _equippedItem = null;
    }


    private void ThrowEquipedItem()
    {
        transform.DetachChildren();
        Rigidbody2D rb = _equippedItem.GetComponent<Rigidbody2D>();
        rb.simulated = true;


        transform.parent.gameObject.GetComponent<Rigidbody2D>().mass -= rb.mass;


        //player mover check for facing
        if (transform.parent.gameObject.GetComponent<PlayerMover>().FacingRight == true)
            rb.AddForce(new Vector2(1 * _maxThrowForce * _throwButtonHoldingTime / _maxThrowBtnHoldingTime, 1 * _maxThrowForce * _throwButtonHoldingTime / _maxThrowBtnHoldingTime));
        else
            rb.AddForce(new Vector2(-1 * _maxThrowForce * _throwButtonHoldingTime / _maxThrowBtnHoldingTime, 1 * _maxThrowForce * _throwButtonHoldingTime / _maxThrowBtnHoldingTime));

        _throwButtonHoldingTime = 0;
        _equippedItem = null;
    }


    private Item GetNearestItem()
    {
        Item nearestItem = _itemsInZone[0];
        float shortestRange = Vector3.Distance(nearestItem.transform.position, transform.position);
        for (int i = 1; i < _itemsInZone.Count; i++)
        {
            float range = Vector3.Distance(_itemsInZone[i].transform.position, transform.position);
            if (range < shortestRange)
            {
                shortestRange = range;
                nearestItem = _itemsInZone[i];
            }
        }
        return nearestItem;
    }
}
