using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom;

namespace MovementTypes
{
    [System.Serializable]
    public class MovementInterpolation
    {
        [SerializeField] private EasingCurve _easingCurve = EasingCurve.Linear;

        [Range(0.05f, 10)]
        [SerializeField] private float _easeMultiplier = 2f;

        [Range(0.01f, 10)]
        [SerializeField] private float _duration = 1f;


        private float _timeStart = 1;
        private float _amount;


        public float Duration { get => _duration; }

        public EasingCurve EasingCurve { get => _easingCurve; }

        public float EaseMultiplier { get => _easeMultiplier; }


        public Vector2 Lerp(Vector2 from, Vector2 to)
        {
            float smoothedAmount = CalculateSmoothedAmount();
            return Interpolation.Lerp(from, to, smoothedAmount);
        }

        public Vector2 Bezier(List<Vector2> points)
        {
            float smoothedAmount = CalculateSmoothedAmount();
            return Interpolation.Bezier(smoothedAmount, points);
        }

        public float CalculateSmoothedAmount()
        {
            _amount = (Time.time - _timeStart) / _duration;
            _amount = Mathf.Clamp01(_amount);
            return Easing.Ease(_amount, _easingCurve, EaseMultiplier);
        }

        public void Reset()
        {
            _amount = 0;
            _timeStart = Time.time;
        }

        public bool IsNotEnded()
        {
            return _amount < 1;
        }
    }
}
