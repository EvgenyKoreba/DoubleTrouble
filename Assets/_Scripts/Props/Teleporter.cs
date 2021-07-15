using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Teleporter _destination;
    [SerializeField] public Facing ExitFacing = Facing.RIGHT;

    public bool IsTeleporting = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsTeleporting == false)
        {
            Teleport(collision.transform);
        }
    }

    private void Teleport(Transform player)
    {
        IsTeleporting = true;
        _destination.IsTeleporting = true;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        Vector2 velocity = rb.velocity;
        velocity.x = Mathf.Abs(velocity.x);
        velocity.y = Mathf.Abs(velocity.y);

        float max = Mathf.Max(velocity.x, velocity.y);
        float min = Mathf.Min(velocity.x, velocity.y);

        if (Direction.IsHorizontal(_destination.ExitFacing))
        {
            velocity.x = max;
            velocity.y = min;
        }
        else
        {
            velocity.x = min;
            velocity.y = max;
        }

        velocity *= Direction.GetVector2(_destination.ExitFacing);
        rb.velocity = velocity;

        player.position = _destination.transform.position;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        IsTeleporting = false;
    }
}
