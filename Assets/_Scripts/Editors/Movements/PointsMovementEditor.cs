using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;

namespace MovementEditor
{
    public abstract class PointsMovementEditor : Editor
    {
        protected StringBuilder helpboxText;

        protected virtual void SetHelpboxText() { }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox(helpboxText.ToString(), MessageType.Info, true);
            DrawDefaultInspector();

            if (!Application.isPlaying)
            {
                PreparePoints();
            }
        }

        protected virtual void PreparePoints() { }

        protected virtual void AddInitialPointOnPointsList() { }

        protected virtual void PrepareFirstPoint() { }

        protected virtual void PrepareSecondPoint() { }
    }
}
