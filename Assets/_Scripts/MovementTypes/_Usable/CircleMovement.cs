using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom;

namespace MovementTypes
{
    public class CircleMovement : MovementBehaviour
    {
        #region Fields
        [Header("Set in Inspector: CircleMovingBehaviour")]
        [SerializeField] private bool _isClockwise = false;
        [SerializeField] private Vector2 _circleCenter = Vector2.one;
        [SerializeField] private float _endAngle = 180f;

        [Range(0, 5)]
        [SerializeField] private float _delay = 0;
        [SerializeField] private MovementInterpolation _interpolation;


        private float _angle;
        private float _radius;
        private float _startAngle;
        #endregion

        public Vector2 CircleCenter 
        { 
            get => _circleCenter; 
            set => _circleCenter = value; 
        }


        private void Awake()
        {
            PrepareFields();
        }

        private void PrepareFields()
        {
            _circleCenter = LocalCenterToWorld();
            CalculateRaduis();
            CalculateStartAngle();
            IncreaseEndAngle();
        }

        private Vector2 LocalCenterToWorld()
        {
            Vector2 worldCenter = _circleCenter;
            worldCenter.x += transform.position.x;
            worldCenter.y += transform.position.y;
            return worldCenter;
        }

        private void CalculateRaduis()
        {
            _radius = Vector2.Distance(_circleCenter, transform.position);
        }

        private void CalculateStartAngle()
        {
            Vector2 direction = GetVector2_Position() - _circleCenter;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            _startAngle = _isClockwise
                ? direction.y > 0 ? 360 - Mathf.Abs(angle) : Mathf.Abs(angle)
                : direction.y > 0 ? Mathf.Abs(angle) : 360 - Mathf.Abs(angle);
        }

        private void IncreaseEndAngle()
        {
            while (_startAngle > _endAngle || _startAngle == _endAngle)
            {
                _endAngle += 180;
            }
        }

        protected override IEnumerator LoopedMovement()
        {
            while (IsLooping)
            {
                _interpolation.Reset();

                while (_interpolation.IsNotEnded())
                {
                    TransformOnCircle();
                    yield return null;
                }
                yield return new WaitForSeconds(_delay);
                TryChangeDirection();
            }
        }

        private void TransformOnCircle()
        {
            Vector2 newPosition = CalculatePositionOnCircle();
            SetPosition(newPosition + _circleCenter);
        }

        private Vector2 CalculatePositionOnCircle()
        {
            float smoothedAmount = _interpolation.CalculateSmoothedAmount();
            float deltaAngle = smoothedAmount * (_endAngle - _startAngle);
            _angle = _startAngle + deltaAngle;
            float x = Mathf.Cos(Mathf.Deg2Rad * _angle) * _radius;
            float y = Mathf.Sin(Mathf.Deg2Rad * _angle) * _radius * GetClockwiseFactor();
            return new Vector2(x, y);
        }

        private int GetClockwiseFactor()
        {
            return _isClockwise ? -1 : 1;
        }

        protected override void ChangeDirection()
        {
            _isClockwise = !_isClockwise;
            float angleOfRotation = _endAngle - _startAngle;
            CalculateStartAngle();
            _endAngle = _startAngle + angleOfRotation;
            IncreaseEndAngle();
        }

        protected override IEnumerator Movement()
        {
            _interpolation.Reset();

            while (_interpolation.IsNotEnded())
            {
                TransformOnCircle();
                yield return null;
            }
        }


        private void OnDrawGizmosSelected()
        {
            if (!Application.isPlaying)
            {
                Gizmos.DrawWireSphere(LocalCenterToWorld(), Vector3.Distance(transform.position, LocalCenterToWorld()));
            }
        }
    }
}
