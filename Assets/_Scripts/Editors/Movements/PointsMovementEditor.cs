using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;

public abstract class PointsMovementEditor : Editor
{
    protected StringBuilder helpboxText;

    protected virtual void SetHelpboxText() => helpboxText = new StringBuilder();

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

    protected virtual void AddInitialPositionOnEmptyPointsList() { }

    protected virtual void AddInitialPositionOnPointsList() { }

    protected virtual void ZeroFirstPoint() { }
}
