using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBehaviour : MonoBehaviour
{
    #region Fields
    [Header("Set in Inspector: MovingBehaviour")]
    [SerializeField] protected bool _isLooping = false;
    [SerializeField] protected bool changeDirection = false;
    [SerializeField] protected float changeDirectionDelay;

    protected float timeStart = -1;
    #endregion

    #region properties
    public bool isLooping
    {
        get { return _isLooping; }
        set
        {
            _isLooping = value;
            if (isLooping)
            {
                StopAllCoroutines();
                StartCoroutine(LoopMoving());
            }
        }
    }
    #endregion


    public void Move()
    {
        if (isLooping)
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
