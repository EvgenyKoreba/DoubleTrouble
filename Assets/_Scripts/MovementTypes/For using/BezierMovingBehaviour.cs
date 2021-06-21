using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom;

// внедрить delayDurations из SimpleMovingBehaviour

public class BezierMovingBehaviour : PointsMovingBehaviour
{
    [Header("Set in Inspector: MovingBehaviour")]
    [SerializeField] private EasingCurve _easingCurve = EasingCurve.Linear;
    [SerializeField] private float _easeMod = 2f;
    [SerializeField] private float _duration;



    protected override void Awake()
    {
        PrepareLists();
        PrepareFields();
        base.Awake();
    }


    protected override IEnumerator Movement()
    {
        float u = 0;
        timeStart = Time.time;

        while (u < 1)
        {
            u = Interpolate();
            yield return null;
        }

        // Если интерполяция закончена, сменить направление
        if (u == 1)
        {
            if (isChangeDirection)
            {
                ChangeDirection();
                yield return new WaitForSeconds(changeDirectionDelay);
            }
        }
    }

    protected override IEnumerator LoopedMovement()
    {

        while (IsLooping)
        {
            float u = 0;
            timeStart = Time.time;

            while (u < 1)
            {
                u = Interpolate();
                yield return null;
            }

            // Если интерполяция закончена, сменить направление
            if (u == 1)
            {
                if (isChangeDirection)
                {
                    ChangeDirection();
                    yield return new WaitForSeconds(changeDirectionDelay);
                }
            }
        }
    }


    private float Interpolate()
    {
        // Стандартная линейная интерполяция
        float u = (Time.time - timeStart) / _duration;
        u = Mathf.Clamp01(u);
        float uC = Easing.Ease(u, _easingCurve, _easeMod);
        Vector3 newPosition = Interpolation.Bezier(uC, points);
        transform.position = newPosition;
        return u;
    }


    private void PrepareFields()
    {
        if (_easeMod == 0)
        {
            _easeMod = 2;
        }
    }
}
