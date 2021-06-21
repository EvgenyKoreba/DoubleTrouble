//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;
//using System.Linq;

////[CustomEditor(typeof(SimpleMovingBehaviour1))]
//public class SimpleMovingBehaviourEditor : Editor
//{
//    private bool _isPointsListShowed = true;
//    private SimpleMovingBehaviour _smb;
//    private int _currentSize;

//    private int CurrentSize
//    {
//        get => _currentSize;
//        set
//        {
//            _currentSize = value;

//            while (_currentSize > _smb.PointsList.Count)
//            {
//                CreateDefaultPoint();
//            }
//            while (_currentSize < _smb.PointsList.Count)
//            {
//                RemoveLastPoint();
//            }
//        }
//    }

//    public override void OnInspectorGUI()
//    {
//        _smb = (SimpleMovingBehaviour)target;

//        EditorGUILayout.HelpBox("Hello", MessageType.Info, true);

//        EditorGUILayout.BeginHorizontal();
//        if (GUILayout.Button("Create Point"))
//        {
//            if (_smb.PointsList.Count > 0)
//            {
//                DuplicateLastPoint();
//            }
//            else
//            {
//                CreateDefaultPoint();
//            }
//        }

//        if (GUILayout.Button("Remove Last Point"))
//        {
//            RemoveLastPoint();
//        }
//        EditorGUILayout.EndHorizontal();

//        _isPointsListShowed = EditorGUILayout.BeginFoldoutHeaderGroup(_isPointsListShowed, new GUIContent("Points"));
//        if (_isPointsListShowed)
//        {

//            //CurrentSize = EditorGUILayout.IntField("size" ,_smb.PointsList.Count);

//            for (int i = 0; i < _smb.PointsList.Count; i++)
//            {
//                GUIStyle v2style = new GUIStyle();
//                v2style.stretchWidth = false;

//                Point point = _smb.PointsList[i];
//                Rect rect = EditorGUILayout.BeginHorizontal(v2style);
//                //EditorGUILayout.LabelField(new GUIContent("Point " + i));
//                point.LocalPosition = EditorGUILayout.Vector2Field("Position", point.LocalPosition, MyOptions());
//                point.DurationOfPassage = EditorGUILayout.FloatField("Duration", point.DurationOfPassage, MyOptions());
//                EditorGUILayout.EndHorizontal();

//            }

//        }

//        EditorGUILayout.EndFoldoutHeaderGroup();
//    }

    


//    private void OnSceneGUI()
//    {
        
//    }

//    private void DuplicateLastPoint()
//    {
//        Point lastPoint = _smb.PointsList.Last();
//        Point newPoint = (Point)lastPoint.Clone();
//        _smb.PointsList.Add(newPoint);
//    }

//    private void CreateDefaultPoint()
//    {
//        Point point = new Point();
//        _smb.PointsList.Add(point);
//    }

//    private void RemoveLastPoint()
//    {
//        _smb.PointsList.Remove(_smb.PointsList.Last());
//    }

//    private GUILayoutOption[] MyOptions()
//    {
//        GUILayoutOption[] glo = new GUILayoutOption[]
//        {
//            GUILayout.Width(200f),
//            GUILayout.MinWidth(100f),
//            GUILayout.ExpandWidth(true),
//            //GUILayout.
//        };
//        return glo;
//    }
//}
