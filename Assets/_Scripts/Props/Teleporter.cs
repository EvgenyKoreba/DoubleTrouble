using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Teleporter _destination;
    public Facing ExitFacing = Facing.RIGHT;

    private bool _isTeleporting = false;


    public bool IsTeleporting
    {
        get => _isTeleporting;
        set => _isTeleporting = value;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isTeleporting)
        {
            return;
        }

        if (collision.GetComponent<PlayerMover>() != null)
        {
            Teleport(collision.transform);
        }

    }

    private void Teleport(Transform player)
    {
        _isTeleporting = true;
        _destination.IsTeleporting = true;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        Vector2 enterVelocity = rb.velocity;

        rb.velocity = GetExitVelocity(enterVelocity);

        player.position = _destination.transform.position;
    }


    private Vector2 GetExitVelocity(Vector2 enterVelocity)
    {
        Vector2 exitVelocity = enterVelocity;
        exitVelocity.x = Mathf.Abs(exitVelocity.x);
        exitVelocity.y = Mathf.Abs(exitVelocity.y);

        float max = Mathf.Max(exitVelocity.x, exitVelocity.y);
        float min = Mathf.Min(exitVelocity.x, exitVelocity.y);

        if (Direction.IsHorizontal(_destination.ExitFacing))
        {
            exitVelocity.x = max;
            exitVelocity.y = min;
        }
        else
        {
            exitVelocity.x = min;
            exitVelocity.y = max;
        }

        exitVelocity *= Direction.GetVector2(_destination.ExitFacing);

        return exitVelocity;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        IsTeleporting = false;
    }
}
