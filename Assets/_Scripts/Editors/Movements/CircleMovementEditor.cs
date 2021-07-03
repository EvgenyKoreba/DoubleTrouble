﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;

[CustomEditor(typeof(CircleMovement))]
[CanEditMultipleObjects]
public class CircleMovementEditor : Editor
{
    private StringBuilder _helpboxText;

    private void OnEnable()
    {
        SetHelpboxText();
    }

    private void SetHelpboxText()
    {
        _helpboxText = new StringBuilder();
        _helpboxText.AppendLine("  1. Bool field 'Is Change Direction' only works when 'Is Looping' = true;");
        _helpboxText.AppendLine("  2. All angles are in degrees;");
        _helpboxText.AppendLine("  3. The position of the center of the circle is local to the object of rotation;");
        _helpboxText.AppendLine("  4. End angle can't be less then start angle. Otherwise 180 degrees are added to the end angle;");
        _helpboxText.AppendLine("  5. Don't set the center of the circle too close to the point. Otherwise, Vector2.one will be added to the center position.");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox(_helpboxText.ToString(), MessageType.Info, true);
        DrawDefaultInspector();
    }
}
