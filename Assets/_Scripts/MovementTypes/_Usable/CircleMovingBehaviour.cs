using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom;

public class CircleMovingBehaviour : MovementBehaviour
{
    #region Fields
    [Header("Set in Inspector: CircleMovingBehaviour")]
    [SerializeField] private bool _clockwise = false;
    
    [Range(0.01f, 10)]
    [SerializeField] private float _duration = 1f;
    [SerializeField] private Vector2 _circleCenter = Vector2.one;
    [SerializeField] private float _endAngle;
    [SerializeField] private EasingCurve _easingCurve = EasingCurve.Linear;

    [Range(1, 10)]
    [SerializeField] private float _easeMod = 2f;


    [Header("Set Dynamically: CircleMovingBehaviour")]
    [SerializeField] private float _angle;
    [SerializeField] private float _radius;
    [SerializeField] private float _startAngle;
    
     
    private int _clockwiseDirectionFactor = 1;
    #endregion
    #region Properties
    public float Angle
    {
        get { return _angle; }
        set
        {
            _angle = value;
        }
    }

    public float EndEngle
    {
        get { return _endAngle; }
        set
        {
            _endAngle = value;
        }
    }

    public float StartAngle
    {
        get { return _startAngle; }
        set
        {
            _startAngle = value;
            while (_startAngle < 0)
            {
                _startAngle += 360;
            }
        }
    }

    public bool Clockwise
    {
        get { return _clockwise; }
        set
        {
            if (_clockwise != value)
            {
                _clockwiseDirectionFactor *= -1;
            }

            _clockwise = value;
        }
    }
    #endregion


    private void Awake()
    {
        PrepareFields();
        FindStartAngle();
    }

    private void PrepareFields()
    {
        LocalCenterToWorld();
        CheckCenter();
        _radius = Vector3.Distance(transform.position, _circleCenter);
        CheckBound();
    }

    private void LocalCenterToWorld()
    {
        _circleCenter.x += transform.position.x;
        _circleCenter.y += transform.position.y;
    }

    private void CheckCenter()
    {
        if (Vector3.Distance(_circleCenter, transform.position) < 0.1f)
        {
            _circleCenter += Vector2.one;
        }
    }

    private void CheckBound()
    {
        if (EndEngle >= Angle)
        {
            EndEngle += 180;
        }
    }



    protected override IEnumerator Movement()
    {
        timeStart = Time.time;
        Angle = StartAngle;
        while (Angle < 360)
        {
            Angle = TransformOnCircle();
            yield return null;
        }
    }



    protected override IEnumerator LoopedMovement()
    {
        timeStart = Time.time;
        Angle = StartAngle;
        while (IsLooping)
        {
            Angle = TransformOnCircle();

            if (isChangeDirection)
            {
                if (Angle >= EndEngle)
                {
                    ChangeDirection();
                }
            }

            yield return null;
        }
    }



    private float TransformOnCircle()
    {
        // Стандартная линейная интерполяция
        float u = (Time.time - timeStart) / _duration;
        float uC = Easing.Ease(u, _easingCurve, _easeMod) * 360;
        uC += StartAngle;
        float x = Mathf.Cos(Mathf.Deg2Rad * uC) * _radius * _clockwiseDirectionFactor;
        float y = Mathf.Sin(Mathf.Deg2Rad * uC) * _radius;
        Vector2 newPosition = new Vector3(x, y);
        newPosition += _circleCenter;
        transform.position = newPosition;
        return uC;
    }

    protected override void ChangeDirection()
    {
        Clockwise = !Clockwise;
        timeStart = Time.time;
        FindStartAngle();
    }

    private void FindStartAngle()
    {
        Vector2 direction = Position() - _circleCenter;
        float angleU = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (Clockwise)
        {
            if (direction.y >= 0)
            {
                StartAngle = 180 - Mathf.Abs(angleU);
            }
            else
            {
                StartAngle = Mathf.Abs(angleU) + 180;
            }
        }
        else
        {
            StartAngle = angleU;
        }
    }


    private Vector3 GetWorldCircleCenter()
    {
        Vector3 worldCenter = Vector3.zero;
        worldCenter.x = _circleCenter.x;
        worldCenter.y = _circleCenter.y;
        return worldCenter;
    }

    private Vector2 Position()
    {
        Vector2 position = Vector2.zero;
        position.x = transform.position.x;
        position.y = transform.position.y;
        return position;
    }


    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
        {
            Gizmos.DrawWireSphere(GetWorldCircleCenter() + transform.position, _circleCenter.magnitude);
        }
    }
}
