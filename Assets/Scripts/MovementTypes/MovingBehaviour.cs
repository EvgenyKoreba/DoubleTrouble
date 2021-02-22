using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBehaviour : MonoBehaviour
{
    #region Fields
    [Header("Set in Inspector: MovingBehaviour")]
    [SerializeField] protected List<Vector3> points;
    [SerializeField] protected bool _isLooping = false;
    [SerializeField] protected bool changeDirection = false;
    [SerializeField] protected float changeDirectionDelay;

    protected float timeStart = -1;
    protected List<Vector3> pts;
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

    protected virtual void Awake()
    {
        for (int i = 0; i < points.Count; i++)
        {
            points[i] += transform.position;
        }
        pts = new List<Vector3>() { transform.position };
        pts.AddRange(points);
    }


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


    protected virtual void ChangeDirection()
    {
        pts.Reverse();
    }


    protected virtual void PrepareLists()
    {
        if (points is null || points.Count < 2)
        {
            Debug.LogError("The number of points cannot be less than 2 ");
            return;
        }
    }


    protected void PrepareList<T>(ref List<T> list, T defaultElement)
    {
        if (list is null)
        {
            list = new List<T>(points.Count + 1);
        }

        else if (list.Count != points.Count)
        {
            List<T> temp = new List<T>(points.Count);
            for (int i = 0; i < list.Count; i++)
            {
                temp.Add(list[i]);
                if (temp.Count == points.Count)
                {
                    break;
                }
            }
            while (temp.Count < points.Count)
            {
                temp.Add(defaultElement);
            }
            list = temp;
        }

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == null)
            {
                list[i] = defaultElement;
            }
        }
    }
}
