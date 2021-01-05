using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class MovingBehaviour : MonoBehaviour
{
    [Header("Set in Inspector: MovingBehaviour")]
    [SerializeField] private List<Vector3>      pts;
    [SerializeField] private string             easingCurve = Easing.Linear;
    [SerializeField] private float              duration;


    [Header("Set Dynamically: MovingBehaviour"), Space(10)]
    [SerializeField] private bool isLooping = true;


    private float                   timeStart = -1;
    private List<Vector3>           points;


    public Vector3 position
    {
        get
        {
            return transform.position;
        }
        set
        {
            transform.position = value;
        }
    }


    public void Move()
    {
        if (isLooping)
        {
            StartCoroutine(LoopMoving());
        }
        else
        {
            StartCoroutine(Moving());
        }
    }


    protected virtual IEnumerator Moving()
    {
        points = new List<Vector3>() { position };
        points.AddRange(pts);
        float u = 0;
        timeStart = Time.time;

        while (u < 1)
        {
            u = Interpolate();
            yield return null;
        }
    }

    protected virtual IEnumerator LoopMoving()
    {
        points = new List<Vector3>() { position };
        points.AddRange(pts);
        float u;

        while (isLooping)
        {
            u = 0;
            timeStart = Time.time;

            while (u < 1)
            {
                u = Interpolate();
                yield return null;
            }

            // Если интерполяция закончена, сменить направление
            if (u == 1)
            {
                ChangeDirection();
            }
        }
    }

    private float Interpolate()
    {
        // Стандартная линейная интерполяция
        float u = (Time.time - timeStart) / duration;
        u = Mathf.Clamp01(u);
        float uC = Easing.Ease(u, easingCurve);
        Vector3 newPosition = Utils.Bezier(uC, points);
        position = newPosition;
        return u;
    }


    private void ChangeDirection()
    {
        List<Vector3> revercePts = new List<Vector3>();

        for (int i = points.Count - 1; i >= 0; i--)
        {
            revercePts.Add(points[i]);
        }
        points = revercePts;
    }
}
