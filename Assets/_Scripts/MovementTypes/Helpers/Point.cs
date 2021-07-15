using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using MovementTypes;
using Custom;

[Serializable]
public class Point
{
    #region Fields
    [SerializeField] private Vector2 _localPosition = Vector2.zero;

    //[Range(0.01f,10)]
    //[SerializeField] private float _durationOfPassage = 1f;
    //[SerializeField] private EasingCurve _easingCurve = EasingCurve.Linear;

    //[Range(1,10)]
    //[SerializeField] private float _easeMultiplier = 2f;

    [Range(0,5)]
    [SerializeField] private float _delay = 0;

    [SerializeField] private MovementInterpolation _interpolation;
    #endregion

    public Point()
    {
        _interpolation = new MovementInterpolation();
        _localPosition = Vector2.zero;
        _delay = 0f;
    }

    public Vector2 LocalPosition {
        get => _localPosition;
        set => _localPosition = value;
    }

    public float Delay { get => _delay; }

    public MovementInterpolation Interpolation { get => _interpolation; }

    public void LocalPositionToWorld(Vector3 origin)
    {
        Vector2 worldPosition = Vector2.zero;
        worldPosition.x = origin.x + _localPosition.x;
        worldPosition.y = origin.y + _localPosition.y;
        _localPosition = worldPosition;
    }

    public Vector2 Interpolate(Vector2 from)
    {
        return _interpolation.Lerp(from, _localPosition);
    }

    public void ResetInterpolation()
    {
        _interpolation.Reset();
    }

    public bool IsNotInterpolated()
    {
        return _interpolation.IsNotEnded();
    }
}
