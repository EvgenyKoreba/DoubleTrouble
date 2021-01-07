using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
        [Header("Set in Inspector")]
    [SerializeField] private GameObject         poi;
    [SerializeField] private float              easing; 


    private void FixedUpdate()
    {
        Vector3 destination = poi.transform.position;
        destination.z = transform.position.z;
        destination = Vector3.Lerp(transform.position, destination, easing);
        transform.position = destination;     
    }
}
