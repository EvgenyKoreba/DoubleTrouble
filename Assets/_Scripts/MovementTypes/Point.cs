using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom;

[System.Serializable]
public class Point
{
    static private readonly EasingCurve _defaultEasingCurve = EasingCurve.Linear;

    [SerializeField] private Vector2 _localPosition = Vector2.zero;
    [SerializeField] private float _durationOfPassage = 1f;
    [SerializeField] private EasingCurve _easingCurve = _defaultEasingCurve;
    [SerializeField] private float _easeMultiplier = 2f;
    [SerializeField] private float _delay = 0;

    public Point()
    {
        _localPosition = Vector2.zero;
        _durationOfPassage = 1f;
        _easingCurve = _defaultEasingCurve;
        _easeMultiplier = 2f;
        _delay = 0f;
    }

    public Vector2 LocalPosition => _localPosition;

    public float DurationOfPassage { get => _durationOfPassage; }

    public EasingCurve EasingCurve { get => _easingCurve; }

    public float EaseMultiplier { get => _easeMultiplier; }

    public float Delay { get => _delay; }

    public void LocalPositionToWorld(Vector3 origin)
    {
        Vector2 worldPosition = Vector2.zero;
        worldPosition.x = origin.x + _localPosition.x;
        worldPosition.y = origin.y + _localPosition.y;
        _localPosition = worldPosition;
    }
}
