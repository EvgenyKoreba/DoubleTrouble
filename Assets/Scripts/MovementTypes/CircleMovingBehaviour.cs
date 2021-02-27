using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovingBehaviour : MovingBehaviour
{
    #region Fields
    [Header("Set in Inspector: CircleMovingBehaviour")]
    [SerializeField] private bool _clockwise = false;
    [SerializeField] private float duration;
    [SerializeField] private Vector3 circleCenter;
    [SerializeField] private float _endAngle;
    [SerializeField] private string easingCurve = "In";
    [SerializeField] private float easeMod = 2f;


    [Header("Set Dynamically: CircleMovingBehaviour")]
    [SerializeField] private float _angle;
    [SerializeField] private float radius;
    [SerializeField] private float _startAngle;
    
     
    private int clockwiseDirectionFactor = 1;
    #endregion
    #region Properties
    public float angle
    {
        get { return _angle; }
        set
        {
            _angle = value;
        }
    }

    public float endAngle
    {
        get { return _endAngle; }
        set
        {
            _endAngle = value;
        }
    }


    public float startAngle
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


    public bool clockwise
    {
        get { return _clockwise; }
        set
        {
            if (_clockwise != value)
            {
                clockwiseDirectionFactor *= -1;
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



    protected override IEnumerator Moving()
    {
        timeStart = Time.time;
        angle = startAngle;
        while (angle < 360)
        {
            angle = TransformOnCircle();

            if (changeDirection)
            {
                if (angle >= endAngle)
                {
                    yield return new WaitForSeconds(changeDirectionDelay);
                    ChangeDirection();
                }
            }

            yield return null;
        }
    }



    protected override IEnumerator LoopMoving()
    {
        timeStart = Time.time;
        angle = startAngle;
        while (isLooping)
        {
            angle = TransformOnCircle();

            if (changeDirection)
            {
                if (angle >= endAngle)
                {
                    yield return new WaitForSeconds(changeDirectionDelay);
                    ChangeDirection();
                }
            }

            yield return null;
        }
    }



    private float TransformOnCircle()
    {
        // Стандартная линейная интерполяция
        float u = (Time.time - timeStart) / duration;
        float uC = Easing.Ease(u, easingCurve, easeMod) * 360;
        uC += startAngle;
        float x = Mathf.Cos(Mathf.Deg2Rad * uC) * radius * clockwiseDirectionFactor;
        float y = Mathf.Sin(Mathf.Deg2Rad * uC) * radius;
        Vector3 newPosition = new Vector3(x, y, 0);
        newPosition += circleCenter;
        transform.position = newPosition;
        return uC;
    }



    protected override void ChangeDirection()
    {
        clockwise = !clockwise;
        timeStart = Time.time;
        FindStartAngle();
    }



    private void FindStartAngle()
    {
        Vector3 posOnCircle = transform.position - circleCenter;
        float angleU = Mathf.Atan2(posOnCircle.y, posOnCircle.x) * Mathf.Rad2Deg;

        if (clockwise)
        {
            if (posOnCircle.y >= 0)
            {
                startAngle = 180 - Mathf.Abs(angleU);
            }
            else
            {
                startAngle = Mathf.Abs(angleU) + 180;
            }
        }
        else
        {
            startAngle = angleU;
        }
    }



    private void PrepareFields()
    {
        if (easeMod == 0)
        {
            easeMod = 2;
        }

        CheckBound();
        circleCenter += transform.position;
        radius = Vector3.Distance(transform.position, circleCenter);
    }



    private void CheckBound()
    {
        if (endAngle >= angle)
        {
            endAngle += 180;
        }
    }


    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
        {
            Gizmos.DrawWireSphere(circleCenter + transform.position, circleCenter.magnitude);
        }
    }
}
