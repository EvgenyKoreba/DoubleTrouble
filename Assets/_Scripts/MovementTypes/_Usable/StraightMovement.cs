using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MovementTypes
{
    public class StraightMovement : MovementBehaviour
    {
        #region Fields
        [Header("Set in Inspector: StraightMovement")]
        [SerializeField] private List<Point> _points = new List<Point>();
        #endregion

        public List<Point> Points 
        { 
            get => _points; 
        }


        private void Awake()
        {
            LocalPointsToWorld();
        }

        public void LocalPointsToWorld()
        {
            foreach (Point point in _points)
            {
                point.LocalPositionToWorld(transform.position);
            }
        }

        protected override IEnumerator LoopedMovement()
        {
            while (IsLooping)
            {
                for (int endPointIndex = 1; endPointIndex < _points.Count; endPointIndex++)
                {
                    Point startPoint = _points[endPointIndex - 1];
                    Point endPoint = _points[endPointIndex];

                    endPoint.ResetInterpolation();

                    while (endPoint.IsNotInterpolated())
                    {
                        TransformOnLine(startPoint, endPoint);
                        yield return null;
                    }

                    yield return new WaitForSeconds(endPoint.Delay);
                }
                TryChangeDirection();
            }
        }

        private void TransformOnLine(Point startPoint, Point endPoint)
        {
            Vector2 newPosition = endPoint.Interpolate(startPoint.LocalPosition);
            SetPosition(newPosition);
        }

        protected override void ChangeDirection() => _points.Reverse();

        protected override IEnumerator Movement()
        {
            for (int endPointIndex = 1; endPointIndex < _points.Count; endPointIndex++)
            {
                Point startPoint = _points[endPointIndex - 1];
                Point endPoint = _points[endPointIndex];

                endPoint.ResetInterpolation();

                while (endPoint.IsNotInterpolated())
                {
                    TransformOnLine(startPoint, endPoint);
                    yield return null;
                }

                yield return new WaitForSeconds(endPoint.Delay);
            }
        }


        private void OnDrawGizmosSelected()
        {
            if (!Application.isPlaying)
            {
                foreach (Point point in _points)
                {
                    Vector3 worldPoint = new Vector3(point.LocalPosition.x, point.LocalPosition.y, 0);
                    Gizmos.DrawWireSphere(worldPoint + transform.position, 0.5f);
                }
            }
        }
    }
}
