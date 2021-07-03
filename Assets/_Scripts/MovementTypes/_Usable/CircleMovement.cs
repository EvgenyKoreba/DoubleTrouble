using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom;

public class CircleMovement : MovementBehaviour
{
    [Header("Set in Inspector: CircleMovingBehaviour")]
    [SerializeField] private bool _isClockwise = false;

    [Range(0.01f, 10)]
    [SerializeField] private float _duration = 1f;
    [SerializeField] private Vector2 _circleCenter = Vector2.one;

    [SerializeField] private float _endAngle = 180f;
    [SerializeField] private EasingCurve _easingCurve = EasingCurve.Linear;

    [Range(0.01f, 10)]
    [SerializeField] private float _easeMod = 2f;


    private float _angle;
    private float _radius;
    private float _startAngle;


    private void Awake()
    {
        PrepareFields();
    }

    private void PrepareFields()
    {
        PrepareCenter();
        _radius = CalculateRaduis();
        CalculateStartAngle();
        CheckBoundsStartAngle();
    }

    private void PrepareCenter()
    {
        _circleCenter = LocalCenterToWorld();
        CheckRadiusSize();
    }

    private Vector2 LocalCenterToWorld()
    {
        Vector2 worldCenter = Vector2.zero;
        worldCenter.x = _circleCenter.x + transform.position.x;
        worldCenter.y = _circleCenter.y + transform.position.y;
        return worldCenter;
    }

    private void CheckRadiusSize()
    {
        if (CalculateRaduis() < 0.1f)
        {
            _circleCenter += Vector2.one;
        }
    }

    private float CalculateRaduis()
    {
        return Vector2.Distance(_circleCenter, transform.position);
    }

    private void CalculateStartAngle()
    {
        Vector2 direction = Position() - _circleCenter;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (_isClockwise)
        {
            if (direction.y > 0)
            {
                _startAngle = 360 - Mathf.Abs(angle);
            }
            else
            {
                _startAngle = Mathf.Abs(angle);
            }
        }
        else
        {
            if (direction.y > 0)
            {
                _startAngle = Mathf.Abs(angle);
            }
            else
            {
                _startAngle = 360 - Mathf.Abs(angle);
            }
        }
    }

    private Vector2 Position()
    {
        Vector2 position = Vector2.zero;
        position.x = transform.position.x;
        position.y = transform.position.y;
        return position;
    }

    private void CheckBoundsStartAngle()
    {
        while (_startAngle > _endAngle || _startAngle == _endAngle)
        {
            _endAngle += 180;
        }
    }

    protected override IEnumerator Movement()
    {
        timeStart = Time.time;
        _angle = _startAngle;

        while (amountOfInterpolation < 1)
        {
            TransformOnCircle();
            yield return null;
        }
    }

    private void TransformOnCircle()
    {
        amountOfInterpolation = (Time.time - timeStart) / _duration;
        amountOfInterpolation = Mathf.Clamp01(amountOfInterpolation);
        float uC = Easing.Ease(amountOfInterpolation, _easingCurve, _easeMod) * (_endAngle - _startAngle);
        _angle = _startAngle + uC;
        float x = Mathf.Cos(Mathf.Deg2Rad * _angle) * _radius;
        float y = Mathf.Sin(Mathf.Deg2Rad * _angle) * _radius * GetClockwiseFactor();
        Vector2 newPosition = new Vector2(x, y);
        transform.position = newPosition + _circleCenter;
    }

    private int GetClockwiseFactor()
    {
        return _isClockwise ? -1 : 1;
    }

    protected override IEnumerator LoopedMovement()
    {
        while (IsLooping)
        {
            amountOfInterpolation = 0;
            timeStart = Time.time;
            _angle = _startAngle;

            while (amountOfInterpolation < 1)
            {
                TransformOnCircle();
                yield return null;
            }
            TryChangeDirection();
        }
    }

    protected override void ChangeDirection()
    {
        _isClockwise = !_isClockwise;
        float angleOfRotation = _endAngle - _startAngle;
        CalculateStartAngle();
        _endAngle = _startAngle + angleOfRotation;
        CheckBoundsStartAngle();
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
        {
            Gizmos.DrawWireSphere(LocalCenterToWorld(), Vector3.Distance(transform.position, LocalCenterToWorld()));
        }
    }
}
