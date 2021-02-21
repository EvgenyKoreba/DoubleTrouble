using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Переопределить ChangeDirection - развернуть списки easingCurves и durations

public class SimpleMovingBehaviour : MovingBehaviour
{
    #region Fields
    [Header("Set in Inspector: SimpleMovingBehaviour")]
    [SerializeField] private List<string> easingCurves;
    [SerializeField] private List<float> durations;
    #endregion


    protected override void Start()
    {
        CheckLists();
        base.Start();
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
        Vector3 newPosition = Vector3.Lerp(pts[pointNumber - 1], pts[pointNumber], uC);
        transform.position = newPosition;
        return u;
    }


    protected void CheckLists()
    {
        if (points == null || points.Count < 2)
        {
            Debug.LogError("The number of points cannot be less than 2 ");
            return;
        }


        if (easingCurves == null)
        {
            easingCurves = new List<string>(points.Count + 1);
        }

        for (int i = 0; i < easingCurves.Count; i++)
        {
            if (easingCurves[i].Equals(null))
            {
                easingCurves[i] = "Linear";
            }
        }


        if (durations == null)
        {
            durations = new List<float>(points.Count + 1);
        }

        for (int i = 0; i < durations.Count; i++)
        {
            if (durations[i] == 0f)
            {
                durations[i] = 1f;
            }
        }
    }
}
