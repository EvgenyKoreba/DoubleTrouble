using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom;


namespace MovementTypes
{
    [System.Serializable]
    public class Sinusoidal_Interpolation
    {
        [Range(0.01f, 10)]
        [SerializeField] private float _duration = 1f;

        [SerializeField] private StraightSinusoidalMovementPartData _straightPart;
        [SerializeField] private CurveSinusoidalMovementPartData _curvePart;

        private float _timeStart = -1;
        private float _amount;
        private Vector3 _endPosition;

        public StraightSinusoidalMovementPartData StraightPart { get => _straightPart; }

        public CurveSinusoidalMovementPartData CurvePart { get =>_curvePart; }


        public void Reset(Vector3 startPosition)
        {
            _amount = 0;
            _timeStart = Time.time;
            CalculateEndPosition(startPosition);
        }

        private void CalculateEndPosition(Vector3 startPosition)
        {
            _endPosition = _duration * _straightPart.Speed * _straightPart.GetDirection();
            _endPosition += startPosition;
        }

        public void CalculateAmount()
        {
            _amount = (Time.time - _timeStart) / _duration;
            _amount = Mathf.Clamp01(_amount);
        }

        public Vector3 CalculateStraightPartPosition(Vector3 startPosition)
        {
            float smoothedValue = Easing.Ease(_amount, _straightPart.EasingCurve, _straightPart.EaseMultiplier);
            Vector3 newPosition = Interpolation.Lerp(startPosition, _endPosition, smoothedValue);
            return newPosition;
        }

        public Vector3 CalculateCurvePartPosition(Vector3 startPosition)
        {
            float smoothedValue = Easing.Ease(_amount, _curvePart.EasingCurve, _curvePart.EaseMultiplier);
            Vector3 newPosition = _curvePart.GetDirection() * Mathf.Sin(smoothedValue * _curvePart.Angle * Mathf.Deg2Rad) * _curvePart.Amplitude;
            newPosition += startPosition;
            return newPosition;
        }

        public bool IsNotEnded()
        {
            return _amount < 1;
        }

        public void Reverse()
        {
            _straightPart.Reverse();
            _curvePart.Reverse();
        }
    }
}
