using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] private PlayerMover player;
    [SerializeField] private GameObject poi;
    [SerializeField] private float easing;
    [SerializeField] private float cameraShift;


    private void FixedUpdate()
    {
        if (poi == null)
        {
            FollowPlayer();
        }
        else if (poi != null)
        {
            FollowPOI();
        }
    }
    public void FollowPOI()
    {
        Vector3 destination = poi.transform.position;
        destination.z = transform.position.z;
        destination = Vector3.Lerp(transform.position, destination, easing);
        transform.position = destination;
    }
    private void FollowPlayer()
    {
        if (player.TurnRight)
        {
            Vector3 destination = new Vector3(player.transform.position.x + cameraShift, player.transform.position.y, transform.position.z);
            destination = Vector3.Lerp(transform.position, destination, easing);
            transform.position = destination;
        }
        if (!player.TurnRight)
        {
            Vector3 destination = new Vector3(player.transform.position.x - cameraShift, player.transform.position.y, transform.position.z);
            destination = Vector3.Lerp(transform.position, destination, easing);
            transform.position = destination;
        }
    }
}
