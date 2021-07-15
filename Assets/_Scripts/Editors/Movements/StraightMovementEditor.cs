using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using MovementTypes;


namespace MovementEditor
{
    [CustomEditor(typeof(StraightMovement))]
    [CanEditMultipleObjects]
    public class StraightMovementEditor : PointsMovementEditor
    {
        private StraightMovement _straightMovement;

        private void OnEnable()
        {
            _straightMovement = (StraightMovement)target;
            SetHelpboxText();
        }

        protected override void SetHelpboxText()
        {
            helpboxText = new StringBuilder();
            helpboxText.AppendLine("  1. All point positions in the point list are local to the original position.");
            helpboxText.AppendLine("  2. The first point in the list of points is always the original position (0,0).");
            helpboxText.AppendLine("  3. Bool field 'Is Change Direction' only works when 'Is Looping' = true");
        }

        protected override void PreparePoints()
        {
            while (_straightMovement.Points.Count < 2)
            {
                AddInitialPointOnPointsList();
            }

            PrepareFirstPoint();
            PrepareSecondPoint();
        }

        protected override void AddInitialPointOnPointsList()
        {
             _straightMovement.Points.Insert(0, new Point());
        }

        protected override void PrepareFirstPoint()
        {
            Point firstPoint = _straightMovement.Points[0];
            if (firstPoint.LocalPosition != Vector2.zero)
            {
                firstPoint.LocalPosition = Vector2.zero;
            }
        }

        protected override void PrepareSecondPoint()
        {
            Point secondPoint = _straightMovement.Points[1];
            if (secondPoint.LocalPosition == Vector2.zero)
            {
                secondPoint.LocalPosition = Vector2.right;
            }
        }
    }
}
