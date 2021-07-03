using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom;
using System.Linq;

public class StraightMovement : MovementBehaviour
{
    #region Fields
    [Header("Set in Inspector: StraightMovement")]
    [SerializeField] private List<Point> _points = new List<Point>();
    #endregion

    public List<Point> Points => _points;


    private void Awake()
    {
        PreparePoints();
        LocalPointsToWorld();
    }

    public void PreparePoints()
    {
        if (_points.Count < 2)
        {
            Debug.LogError("The number of points cannot be less than 2");
        }
    }

    public void LocalPointsToWorld()
    {
        foreach (Point point in _points)
        {
            point.LocalPositionToWorld(transform.position);
        }
    }

    protected override IEnumerator LoopedMovement()
    {
        while (IsLooping)
        {
            for (int endPointIndex = 1; endPointIndex < _points.Count; endPointIndex++)
            {
                amountOfInterpolation = 0;
                timeStart = Time.time;

                while (amountOfInterpolation < 1)
                {
                    TransformTo(endPointIndex);
                    yield return null;
                }

                yield return new WaitForSeconds(_points[endPointIndex].Delay);
            }
            TryChangeDirection();
        }
    }

    protected override IEnumerator Movement()
    {
        for (int endPointIndex = 1; endPointIndex < _points.Count; endPointIndex++)
        {
            amountOfInterpolation = 0;
            timeStart = Time.time;

            while (amountOfInterpolation < 1)
            {
                TransformTo(endPointIndex);
                yield return null;
            }

            yield return new WaitForSeconds(_points[endPointIndex].Delay);
        }
    }

    protected override void ChangeDirection() => _points.Reverse();

    private void TransformTo(int endPointIndex)
    {
        Point startPoint = _points[endPointIndex - 1];
        Point endPoint = _points[endPointIndex];

        InterpolateTo(endPoint);
        float smoothedValue = Easing.Ease(amountOfInterpolation, endPoint.EasingCurve);
        Vector2 newPosition = Interpolation.Lerp(startPoint.LocalPosition, endPoint.LocalPosition, smoothedValue);
        SetPosition(newPosition);
    }

    private void InterpolateTo(Point endPoint)
    {
        amountOfInterpolation = (Time.time - timeStart) / endPoint.DurationOfPassage;
        amountOfInterpolation = Mathf.Clamp01(amountOfInterpolation);
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
        {
            foreach (Point point in _points)
            {
                Vector3 worldPoint = new Vector3(point.LocalPosition.x, point.LocalPosition.y, 0);
                Gizmos.DrawWireSphere(worldPoint + transform.position, 0.5f);
            }
        }
    }
}
