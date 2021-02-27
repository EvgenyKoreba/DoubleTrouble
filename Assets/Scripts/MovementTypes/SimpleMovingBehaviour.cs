using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Переопределить ChangeDirection - развернуть списки easingCurves и durations

public class SimpleMovingBehaviour : PointsMovingBehaviour
{
    #region Fields
    [Header("Set in Inspector: SimpleMovingBehaviour")]
    [SerializeField] private List<float> durations;
    [SerializeField] private List<string> easingCurves;
    [SerializeField] private List<float> easeMods;
    [SerializeField] private List<float> delayDurations;
    #endregion


    protected override void Awake()
    {
        // обязательно в таком порядке!!!
        PrepareLists();
        base.Awake();
    }


    protected override IEnumerator Moving()
    {
        for (int i = 1; i < pts.Count; i++)
        {
            float u = 0;
            timeStart = Time.time;

            while (u < 1)
            {
                u = Interpolate(i);
                yield return null;
            }

            if (u == 1)
            {
                yield return new WaitForSeconds(delayDurations[i - 1]);
            }

            // Если интерполяция закончена, сменить направление
            if (i == pts.Count - 1 && u == 1)
            {
                if (changeDirection)
                {
                    ChangeDirection();
                    yield return new WaitForSeconds(changeDirectionDelay);
                }
            }
        }
    }

    protected override IEnumerator LoopMoving()
    {
        while (isLooping)
        {
            for (int i = 1; i < pts.Count; i++)
            {
                float u = 0;
                timeStart = Time.time;

                while (u < 1)
                {
                    u = Interpolate(i);
                    yield return null;
                }

                // Если интерполяция закончена, сменить направление
                if (i == pts.Count - 1 && u == 1)
                {
                    if (changeDirection)
                    {
                        ChangeDirection();
                        yield return new WaitForSeconds(changeDirectionDelay);
                    }
                }
            }
        }
    }


    private float Interpolate(int pointNumber)
    {
        // Стандартная линейная интерполяция
        float u = (Time.time - timeStart) / durations[pointNumber - 1];
        u = Mathf.Clamp01(u);
        float uC = Easing.Ease(u, easingCurves[pointNumber - 1]);
        Vector3 newPosition = Utils.Lerp(pts[pointNumber - 1], pts[pointNumber], uC);
        transform.position = newPosition;
        return u;
    }


    protected override void PrepareLists()
    {
        base.PrepareLists();
        PrepareList(ref easingCurves, "Linear");
        PrepareList(ref durations, 1f);
        PrepareList(ref delayDurations, 0);
        PrepareList(ref easeMods, 2);
    }


    protected override void ChangeDirection()
    {
        base.ChangeDirection();
        durations.Reverse();
        easingCurves.Reverse();
        delayDurations.Reverse();
    }
}
