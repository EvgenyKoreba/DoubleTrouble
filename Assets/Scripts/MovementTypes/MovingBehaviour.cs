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
            if (value)
            {
                StopAllCoroutines();
                StartCoroutine(LoopMoving());
            }
        }
    }
    #endregion

    protected virtual void Start()
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


    protected void ChangeDirection()
    {
        List<Vector3> revercePts = new List<Vector3>();

        for (int i = pts.Count - 1; i >= 0; i--)
        {
            revercePts.Add(pts[i]);
        }
        pts = revercePts;
    }

}
