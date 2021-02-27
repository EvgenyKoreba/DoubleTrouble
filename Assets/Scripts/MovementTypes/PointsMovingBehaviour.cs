using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsMovingBehaviour : MovingBehaviour
{
    #region Fields
    [Header("Set in Inspector: PointsMovingBehaviour")]
    [SerializeField] protected List<Vector3> points;


    protected List<Vector3> pts;
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


    protected override void ChangeDirection()
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


    protected void PrepareList<T>(ref List<T> list, T defaultValue)
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
                temp.Add(defaultValue);
            }
            list = temp;
        }

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == null)
            {
                list[i] = defaultValue;
            }
        }
    }
}
