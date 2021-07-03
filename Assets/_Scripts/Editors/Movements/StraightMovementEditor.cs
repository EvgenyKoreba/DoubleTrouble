using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;

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

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox(helpboxText.ToString(), MessageType.Info, true);
        DrawDefaultInspector();

        if (!Application.isPlaying)
        {
            AddInitialPositionOnEmptyPointsList();
            ZeroFirstPoint();
        }
    }

    protected override void AddInitialPositionOnEmptyPointsList()
    {
        if (_straightMovement.Points.Count == 0)
        {
            AddInitialPositionOnPointsList();
        }
    }

    protected override void AddInitialPositionOnPointsList()
    {
        _straightMovement.Points.Insert(0, GetSimplePoint());
    }

    private Point GetSimplePoint()
    {
        return _straightMovement.Points.Count > 0 ? (Point)_straightMovement.Points[0].Clone() : new Point();
    }

    protected override void ZeroFirstPoint()
    {
        Point firstPoint = _straightMovement.Points[0];
        if (firstPoint.LocalPosition != Vector2.zero)
        {
            firstPoint.LocalPosition = Vector2.zero;
        }
    }
}
