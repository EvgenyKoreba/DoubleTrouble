using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// внедрить delayDurations из SimpleMovingBehaviour

public class BezierMovingBehaviour : PointsMovingBehaviour
{
    [Header("Set in Inspector: MovingBehaviour")]
    [SerializeField] private string easingCurve = Easing.Linear;
    [SerializeField] private float easeMod = 2f;
    [SerializeField] private float duration;



    protected override void Awake()
    {
        PrepareLists();
        PrepareFields();
        base.Awake();
    }


    protected override IEnumerator Moving()
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
            if (changeDirection)
            {
                ChangeDirection();
                yield return new WaitForSeconds(changeDirectionDelay);
            }
        }
    }

    protected override IEnumerator LoopMoving()
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
                if (changeDirection)
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
        float u = (Time.time - timeStart) / duration;
        u = Mathf.Clamp01(u);
        float uC = Easing.Ease(u, easingCurve, easeMod);
        Vector3 newPosition = Utils.Bezier(uC, pts);
        transform.position = newPosition;
        return u;
    }


    private void PrepareFields()
    {
        if (easeMod == 0)
        {
            easeMod = 2;
        }
    }
}
