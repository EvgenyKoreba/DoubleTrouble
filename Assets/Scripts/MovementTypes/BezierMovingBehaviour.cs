using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierMovingBehaviour : MovingBehaviour
{
    [Header("Set in Inspector: MovingBehaviour")]
    [SerializeField] private string easingCurve = Easing.Linear;
    [SerializeField] private float duration;



    protected override IEnumerator Moving()
    {
        float u = 0;
        timeStart = Time.time;

        while (u < 1)
        {
            u = Interpolate();
            yield return null;
        }
    }

    protected override IEnumerator LoopMoving()
    {

        while (true)
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
        float uC = Easing.Ease(u, easingCurve);
        Vector3 newPosition = Utils.Bezier(uC, pts);
        transform.position = newPosition;
        return u;
    }
    
}
