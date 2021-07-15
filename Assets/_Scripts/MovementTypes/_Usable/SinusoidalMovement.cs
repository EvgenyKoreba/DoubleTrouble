using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MovementTypes
{
    public class SinusoidalMovement : MovementBehaviour
    {
        #region Fields
        [Header("Set in Inspector: SinusoidalMovement")]

        [Range(0.01f, 10)]
        [SerializeField] private float _duration = 1f;

        [Range(0, 5)]
        [SerializeField] private float _changeDirectionDelay = 0;

        [SerializeField] private Sinusoidal_Interpolation _interpolation;


        private Vector3 _startPosition;
        #endregion

        public StraightSinusoidalMovementPartData StraightPart { get => _interpolation.StraightPart; }

        public CurveSinusoidalMovementPartData CurvePart { get => _interpolation.CurvePart; }




        protected override IEnumerator LoopedMovement()
        {
            while (IsLooping)
            {
                ResetFieldsBeforeInterpolation();

                while (_interpolation.IsNotEnded())
                {
                    TransformOnCurve();
                    yield return null;
                }
                yield return new WaitForSeconds(_changeDirectionDelay);
                TryChangeDirection();
            }
        }

        private void ResetFieldsBeforeInterpolation()
        {
            _startPosition = transform.position;
            _interpolation.Reset(_startPosition);
        }

        protected override IEnumerator Movement()
        {
            ResetFieldsBeforeInterpolation();

            while (_interpolation.IsNotEnded())
            {
                TransformOnCurve();
                yield return null;
            }
        }

        private void TransformOnCurve()
        {
            _interpolation.CalculateAmount();
            Vector3 straightPosition = _interpolation.CalculateStraightPartPosition(_startPosition);
            Vector3 curvePosition = _interpolation.CalculateCurvePartPosition(_startPosition);

            Vector2 newPosition = Vector2.zero;
            if (StraightPart.IsVertical())
            {
                newPosition.x = curvePosition.x;
                newPosition.y = straightPosition.y;
            }
            else
            {
                newPosition.x = straightPosition.x;
                newPosition.y = curvePosition.y;
            }

            SetPosition(newPosition);
        }

        protected override void ChangeDirection()
        {
            _interpolation.Reverse();
        }


    }
}
