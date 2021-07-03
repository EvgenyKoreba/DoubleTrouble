using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;

[CustomEditor(typeof(Circle))]
[CanEditMultipleObjects]
public class CircleMovementEditor : Editor
{
    private Circle _circleMovement;
    private StringBuilder _helpboxText;

    private void OnEnable()
    {
        _circleMovement = (Circle)target;
        SetHelpboxText();
    }

    private void SetHelpboxText()
    {
        _helpboxText = new StringBuilder();
        _helpboxText.AppendLine("  1. All point positions in the point list are local to the original position.");
        _helpboxText.AppendLine("  2. The first point in the list of points is always the original position (0,0).");
        _helpboxText.AppendLine("  3. Bool field 'Is Change Direction' only works when 'Is Looping' = true");
        _helpboxText.AppendLine("  4. End angle can't be less then start angle.");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox(_helpboxText.ToString(), MessageType.Info, true);
        DrawDefaultInspector();
    }
}
