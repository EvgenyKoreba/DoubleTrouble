using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] private GameObject poi;
    [SerializeField] private float easing;
    [SerializeField] private float cameraDisplacement;


    private PlayerMover player;


    private void Awake()
    {
        player = poi.GetComponent<PlayerMover>();
    }


    private void FixedUpdate()
    {
        if (player == null)
        {
            FollowPOI();
        }
        else
        {
            FollowPlayer();
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
        Vector3 destination;

        if (player.facingRight)
        {
            destination = new Vector3(player.transform.position.x + cameraDisplacement, player.transform.position.y, transform.position.z);
        }
        else
        {
            destination = new Vector3(player.transform.position.x - cameraDisplacement, player.transform.position.y, transform.position.z);
        }

        destination = Vector3.Lerp(transform.position, destination, easing);
        transform.position = destination;
    }
}
