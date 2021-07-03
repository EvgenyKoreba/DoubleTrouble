using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom;

public class BezierMovement : MovementBehaviour
{
    #region Fields
    [Header("Set in Inspector: BezierMovement")]
    [SerializeField] private List<Vector2> _points = new List<Vector2>();

    [Range(0.01f, 10)]
    [SerializeField] private float _duration;
    [SerializeField] private EasingCurve _easingCurve = EasingCurve.Linear;

    [Range(1, 10)]
    [SerializeField] private float _easeMultiplier = 2f;

    [Range(0, 5)]
    [SerializeField] private float _delay = 0;
    #endregion

    public List<Vector2> Points => _points;

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
        for (int i = 0; i < _points.Count; i++)
        {
            Vector2 point = _points[i];
            point.x += transform.position.x;
            point.y += transform.position.y;
            _points[i] = point;
        }
    }

    protected override IEnumerator LoopedMovement()
    {
        while (IsLooping)
        {
            amountOfInterpolation = 0;
            timeStart = Time.time;

            while (amountOfInterpolation < 1)
            {
                TransformOnCurve();
                yield return null;
            }
            yield return new WaitForSeconds(_delay);
            TryChangeDirection();
        }
    }

    protected override IEnumerator Movement()
    {
        amountOfInterpolation = 0;
        timeStart = Time.time;

        while (amountOfInterpolation < 1)
        {
            TransformOnCurve();
            yield return null;
        }
        yield return new WaitForSeconds(_delay);
    }

    protected override void ChangeDirection() => _points.Reverse();

    private void TransformOnCurve()
    {
        amountOfInterpolation = (Time.time - timeStart) / _duration;
        amountOfInterpolation = Mathf.Clamp01(amountOfInterpolation);
        float smoothedValue = Easing.Ease(amountOfInterpolation, _easingCurve, _easeMultiplier);
        Vector2 newPosition = Interpolation.Bezier(smoothedValue, _points);
        SetPosition(newPosition);
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
        {
            foreach (Vector2 point in _points)
            {
                Vector3 worldPoint = new Vector3(point.x, point.y, 0);
                Gizmos.DrawWireSphere(worldPoint + transform.position, 0.5f);
            }
        }
    }
}
