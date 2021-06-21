using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom;
using System.Linq;

public class SimpleMovingBehaviour : MovingBehaviour
{
    #region Fields
    [Header("Set in Inspector: SimpleMovingBehaviour")]
    public List<Point> PointsList = new List<Point>();

    private float _amountOfInterpolation;
    #endregion


    private void Awake()
    {
        PrepareLists();
        LocalPointsToWorld();
    }

    protected virtual void PrepareLists()
    {
        AddInitialPositionOnPointsList();
        if (PointsList.Count < 2)
        {
            Debug.LogError("The number of points cannot be less than 2");
        }
    }

    private void AddInitialPositionOnPointsList()
    {
        PointsList.Insert(0, new Point());
    }

    private void LocalPointsToWorld()
    {
        foreach (Point point in PointsList)
        {
            point.LocalPositionToWorld(transform.position);
        }
    }

    protected override void ChangeDirection() => PointsList.Reverse();

    protected override IEnumerator Movement()
    {
        for (int endPointIndex = 1; endPointIndex < PointsList.Count; endPointIndex++)
        {
            _amountOfInterpolation = 0;
            timeStart = Time.time;

            while (_amountOfInterpolation < 1)
            {
                TransformTo(endPointIndex);
                yield return null;
            }

            yield return new WaitForSeconds(PointsList[endPointIndex].Delay);

            // Если интерполяция закончена, сменить направление
            if (isChangeDirection)
            {
                if (endPointIndex == PointsList.Count - 1 && _amountOfInterpolation == 1)
                {
                    ChangeDirection();
                    yield return new WaitForSeconds(changeDirectionDelay);
                }
            }
        }
    }

    protected override IEnumerator LoopedMovement()
    {
        while (IsLooping)
        {
            for (int endPointIndex = 1; endPointIndex < PointsList.Count; endPointIndex++)
            {
                _amountOfInterpolation = 0;
                timeStart = Time.time;

                while (_amountOfInterpolation < 1)
                {
                    TransformTo(endPointIndex);
                    yield return null;
                }

                yield return new WaitForSeconds(PointsList[endPointIndex].Delay);

                // Если интерполяция закончена, сменить направление
                if (isChangeDirection)
                {
                    if (endPointIndex == PointsList.Count - 1 && _amountOfInterpolation == 1)
                    {
                        ChangeDirection();
                        yield return new WaitForSeconds(changeDirectionDelay);
                    }
                }
            }
        }
    }

    private void TransformTo(int endPointIndex)
    {
        Point startPoint = PointsList[endPointIndex - 1];
        Point endPoint = PointsList[endPointIndex];

        InterpolateTo(endPoint);
        float smoothedValue = Easing.Ease(_amountOfInterpolation, endPoint.EasingCurve);
        Vector2 newPosition = Interpolation.Lerp(startPoint.LocalPosition, endPoint.LocalPosition, smoothedValue);
        SetPosition(newPosition);
    }

    private void InterpolateTo(Point endPoint)
    {
        _amountOfInterpolation = (Time.time - timeStart) / endPoint.DurationOfPassage;
        _amountOfInterpolation = Mathf.Clamp01(_amountOfInterpolation);
    }
}
