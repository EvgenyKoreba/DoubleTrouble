using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Text;
using UnityEngine;
using MovementTypes;


namespace MovementEditor
{
    [CustomEditor(typeof(BezierMovement))]
    [CanEditMultipleObjects]
    public class BezierMovementEditor : PointsMovementEditor
    {
        private BezierMovement _bezierMovement;

        private void OnEnable()
        {
            _bezierMovement = (BezierMovement)target;
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
            while (_bezierMovement.Points.Count < 2)
            {
                AddInitialPointOnPointsList();
            }

            PrepareFirstPoint();
            PrepareSecondPoint();
        }


        protected override void AddInitialPointOnPointsList()
        {
            _bezierMovement.Points.Insert(0, Vector2.zero);
        }

        protected override void PrepareFirstPoint()
        {
            if (_bezierMovement.Points[0] != Vector2.zero)
            {
                _bezierMovement.Points[0] = Vector2.zero;
            }
        }

        protected override void PrepareSecondPoint()
        {
            if (_bezierMovement.Points[1] == Vector2.zero)
            {
                _bezierMovement.Points[1] = Vector2.one;
            }
        }
    }
}
