using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using MovementTypes;


namespace MovementEditor
{
    [CustomEditor(typeof(SinusoidalMovement))]
    public class SinusoidalMovementEditor : Editor
    {
        private SinusoidalMovement _sinusoidal;
        private StringBuilder _helpboxText;

        private void OnEnable()
        {
            _sinusoidal = (SinusoidalMovement)target;
            SetHelpboxText();
        }

        private void SetHelpboxText()
        {
            _helpboxText = new StringBuilder();
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox(_helpboxText.ToString(), MessageType.Info, true);
            DrawDefaultInspector();

            if (!Application.isPlaying)
            {
                PrepareFiels();
            }
        }

        private void PrepareFiels()
        {
            PrepareDirections();
            PrepareCurveAngle();
        }

        private void PrepareDirections()
        {
            while (Direction.IsParallel(_sinusoidal.StraightPart.Direction, _sinusoidal.CurvePart.Direction))
            {
                _sinusoidal.CurvePart.SetNextDirection();
            }
        }

        private void PrepareCurveAngle()
        {
            _sinusoidal.CurvePart.PrepareAngle();
        }
    }
}
