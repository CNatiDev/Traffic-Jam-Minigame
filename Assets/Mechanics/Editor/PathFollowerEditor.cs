using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PathFollower))]
public class PathFollowerEditor : Editor
{
    private SerializedProperty pathPointsProperty;
    private PathFollower pathFollower;

    private void OnEnable()
    {
        pathFollower = (PathFollower)target;
        pathPointsProperty = serializedObject.FindProperty("pathPoints");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(pathPointsProperty, true);
        serializedObject.ApplyModifiedProperties();
    }

    private void OnSceneGUI()
    {
            serializedObject.Update();

            EditorGUI.BeginChangeCheck();

            // Draw the handles for each path point and its ramification points
            for (int i = 0; i < pathFollower.pathPoints.Count; i++)
            {
                // Access the PathPoint struct correctly
                PathFollower.PathPoint pathPoint = pathFollower.pathPoints[i];

                // Draw the handle for the main path point
                Vector3 newPosition = Handles.PositionHandle(pathPoint.pointPosition, Quaternion.identity);

                // If the position has changed, update the path point
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(pathFollower, "Move Path Point");
                    pathPoint.pointPosition = newPosition;
                    pathFollower.pathPoints[i] = pathPoint;
                }

                // Draw the handles for ramification points
                if (pathPoint.hasRamification)
                {
                    for (int j = 0; j < pathPoint.ramificationPoints.Count; j++)
                    {
                        Vector3 newRamificationPosition = Handles.PositionHandle(pathPoint.ramificationPoints[j], Quaternion.identity);

                        // If the position has changed, update the ramification point
                        if (EditorGUI.EndChangeCheck())
                        {
                            Undo.RecordObject(pathFollower, "Move Ramification Point");
                            pathPoint.ramificationPoints[j] = newRamificationPosition;
                            pathFollower.pathPoints[i] = pathPoint;
                        }
                    }
                }
            }

            serializedObject.ApplyModifiedProperties();
        
    }

}
