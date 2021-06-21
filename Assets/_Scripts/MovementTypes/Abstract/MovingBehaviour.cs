using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingBehaviour : MonoBehaviour
{
    #region Fields
    [Header("Set in Inspector: MovingBehaviour")]
    [SerializeField] protected bool isLooping = false;
    [SerializeField] protected bool isChangeDirection = false;
    [SerializeField] protected float changeDirectionDelay = 0f;

    protected float timeStart = -1;
    #endregion

    #region properties
    public bool IsLooping
    {
        get => isLooping;
        set
        {
            isLooping = value;
            StopAllCoroutines();
            Move();
        }
    }
    #endregion

    public void Move()
    {
        if (IsLooping)
        {
            LoopMove();
        }
        else
        {
            StraightMove();
        }
    }

    private void LoopMove()
    {
        StartCoroutine(LoopedMovement());
    }

    protected virtual IEnumerator LoopedMovement()
    {
        yield break;
    }

    private void StraightMove()
    {
        StartCoroutine(Movement());
    }

    protected virtual IEnumerator Movement()
    {
        yield break;
    }

    protected virtual void ChangeDirection() { }

    protected void SetPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    protected void SetPosition(Vector2 newPosition)
    {
        Vector3 temp = Vector3.zero;
        temp.x = newPosition.x;
        temp.y = newPosition.y;
        transform.position = temp;
    }
}
