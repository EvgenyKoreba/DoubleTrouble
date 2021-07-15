using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom;


namespace MovementTypes
{
    public class BezierMovement : MovementBehaviour
    {
        #region Fields
        [Header("Set in Inspector: BezierMovement")]
        [SerializeField] private List<Vector2> _points = new List<Vector2>();

        [Range(0, 5)]
        [SerializeField] private float _changeDirectionDelay = 0;
        [SerializeField] private MovementInterpolation _interpolation;
        #endregion

        public List<Vector2> Points => _points;


        private void Awake()
        {
            LocalPointsToWorld();
        }

        public void LocalPointsToWorld()
        {
            for (int i = 0; i < _points.Count; i++)
            {
                Vector2 point = _points[i];
                point.x += transform.position.x;
                point.y += transform.position.y;
                _points[i] = point;
            }
        }

        protected override IEnumerator LoopedMovement()
        {
            while (IsLooping)
            {
                _interpolation.Reset();

                while (_interpolation.IsNotEnded())
                {
                    TransformOnCurve();
                    yield return null;
                }
                yield return new WaitForSeconds(_changeDirectionDelay);
                TryChangeDirection();
            }
        }

        private void TransformOnCurve()
        {
            Vector2 newPosition = _interpolation.Bezier(_points);
            SetPosition(newPosition);
        }

        protected override void ChangeDirection() => _points.Reverse();

        protected override IEnumerator Movement()
        {
            _interpolation.Reset();

            while (_interpolation.IsNotEnded())
            {
                TransformOnCurve();
                yield return null;
            }
        }


        private void OnDrawGizmosSelected()
        {
            if (!Application.isPlaying)
            {
                foreach (Vector2 point in _points)
                {
                    Vector3 worldPoint = new Vector3(point.x, point.y, 0);
                    Gizmos.DrawWireSphere(worldPoint + transform.position, 0.5f);
                }
            }
        }
    }
}
