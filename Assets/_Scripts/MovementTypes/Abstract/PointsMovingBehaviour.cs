using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class PointsMovingBehaviour : MovingBehaviour
{
    #region Fields
    [SerializeField] protected List<Vector3> points;
    #endregion

    protected virtual void Awake()
    {
        PrepareLists();
        LocalPointsToWorld();
    }

    protected virtual void PrepareLists()
    {
        AddInitialPositionOnPointsList();
        if (points.Count < 2)
        {
            Debug.LogError("The number of points cannot be less than 2");
        }
    }

    private void AddInitialPositionOnPointsList()
    {
        points.Insert(0, Vector3.zero);
    }

    private void LocalPointsToWorld()
    {   
        for (int i = 0; i < points.Count; i++)
        {
            points[i] += transform.position;
        }
    }


    protected override void ChangeDirection()
    {
        points.Reverse();
    }

    protected void PrepareList<T>(ref List<T> sectionsList, T defaultValue)
    {
        int sections = points.Count - 1;
        while (sectionsList.Count < sections)
        {
            sectionsList.Add(defaultValue);
        }

        while (sectionsList.Count > sections)
        {
            sectionsList.Remove(sectionsList.Last());
        }
    }
}
