using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom;
using System;

[System.Serializable]
public class Point: ICloneable
{
    #region Fields
    [SerializeField] private Vector2 _localPosition = Vector2.zero;

    [Range(0.01f,10)]
    [SerializeField] private float _durationOfPassage = 1f;
    [SerializeField] private EasingCurve _easingCurve = EasingCurve.Linear;

    [Range(1,10)]
    [SerializeField] private float _easeMultiplier = 2f;

    [Range(0,5)]
    [SerializeField] private float _delay = 0;
    #endregion

    public Point()
    {
        _localPosition = Vector2.zero;
        _durationOfPassage = 1f;
        _easingCurve = EasingCurve.Linear;
        _easeMultiplier = 2f;
        _delay = 0f;
    }

    public Point(Point clone)
    {
        _localPosition = Vector2.zero;
        _durationOfPassage = clone.DurationOfPassage;
        _easingCurve = clone.EasingCurve;
        _easeMultiplier = clone.EaseMultiplier;
        _delay = clone.Delay;
    }

    public Vector2 LocalPosition {
        get => _localPosition;
        set => _localPosition = value;
    }

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

    public object Clone()
    {
        return (Point)MemberwiseClone();
    }
}
