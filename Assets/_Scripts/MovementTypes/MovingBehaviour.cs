using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBehaviour : MonoBehaviour
{
    #region Fields
    [Header("Set in Inspector: MovingBehaviour")]
    [SerializeField] protected bool isLooping = false;
    [SerializeField] protected bool changeDirection = false;
    [SerializeField] protected float changeDirectionDelay = 0f;

    protected float timeStart = -1;
    #endregion

    #region properties
    public bool IsLooping
    {
        get { return isLooping; }
        set
        {
            isLooping = value;
            if (IsLooping)
            {
                StopAllCoroutines();
                StartCoroutine(LoopMoving());
            }
        }
    }
    #endregion


    public void Move()
    {
        if (IsLooping)
        {
            StartCoroutine(LoopMoving());
        }
        else
        {
            StartCoroutine(Moving());
        }
    }


    protected virtual IEnumerator Moving()
    {
        yield break;
    }


    protected virtual IEnumerator LoopMoving()
    {
        yield break;
    }


    protected virtual void ChangeDirection() { }
}
