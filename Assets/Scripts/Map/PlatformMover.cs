using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : PointsMovingBehaviour
{
    protected override void Awake()
    {
        Move();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null || collision.gameObject.GetComponent<Item>() != null)
        {
            collision.gameObject.transform.parent = gameObject.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null || collision.gameObject.GetComponent<Item>() != null)
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
