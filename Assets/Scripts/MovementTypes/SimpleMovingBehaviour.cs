using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Переопределить ChangeDirection - развернуть списки easingCurves и durations

public class SimpleMovingBehaviour : PointsMovingBehaviour
{
    #region Fields
    [Header("Set in Inspector: SimpleMovingBehaviour")]
    [SerializeField] private List<float> _durations;
    [SerializeField] private List<string> _easingCurves;
    [SerializeField] private List<float> _easeMods;
    [SerializeField] private List<float> _delayDurations;
    #endregion


    protected override void Awake()
    {
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
                yield return new WaitForSeconds(_delayDurations[i - 1]);
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
        while (IsLooping)
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
        float u = (Time.time - timeStart) / _durations[pointNumber - 1];
        u = Mathf.Clamp01(u);
        float uC = Easing.Ease(u, _easingCurves[pointNumber - 1]);
        Vector3 newPosition = Utils.Lerp(pts[pointNumber - 1], pts[pointNumber], uC);
        transform.position = newPosition;
        return u;
    }


    protected override void PrepareLists()
    {
        base.PrepareLists();
        PrepareList(ref _easingCurves, "Linear");
        PrepareList(ref _durations, 1f);
        PrepareList(ref _delayDurations, 0);
        PrepareList(ref _easeMods, 2);
    }


    protected override void ChangeDirection()
    {
        base.ChangeDirection();
        _durations.Reverse();
        _easingCurves.Reverse();
        _delayDurations.Reverse();
    }
}
