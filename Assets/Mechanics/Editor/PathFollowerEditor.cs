using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static PathFollower;

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

    private PathFollower.PathPoint DrawHandlesRecursively(PathFollower.PathPoint pathPoint)
    {
        EditorGUI.BeginChangeCheck();

        // Draw the handle for the current path point
        Vector3 newPosition = Handles.PositionHandle(pathPoint.pointPosition, Quaternion.identity);

        // If the position has changed, update the path point
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(pathFollower, "Move Path Point");
            pathPoint.pointPosition = newPosition;
        }

        // Draw handles for ramification points
        if (pathPoint.hasRamification)
        {
            for (int j = 0; j < pathPoint.ramificationPoints.Count; j++)
            {
                PathFollower.RamificationPoint ramificationPoint = pathPoint.ramificationPoints[j];

                // Draw the handle for the ramification point
                Vector3 newRamificationPosition = Handles.PositionHandle(ramificationPoint.pointPosition, Quaternion.identity);

                // If the position has changed, update the ramification point
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(pathFollower, "Move Ramification Point");
                    ramificationPoint.pointPosition = newRamificationPosition;
                    pathPoint.pointPosition = newPosition;
                    pathPoint.ramificationPoints[j] = ramificationPoint;
                }

                // Recursively draw handles for nestedPathPoints
                if (ramificationPoint.hasRamification1)
                {
                    for (int k = 0; k < ramificationPoint.nestedPathPoints.Count; k++)
                    {
                        ramificationPoint.nestedPathPoints[k] = DrawHandlesRecursively(ramificationPoint.nestedPathPoints[k]);
                    }
                }
            }
        }

        return pathPoint; // Return the updated path point
    }

    private void OnSceneGUI()
    {
        serializedObject.Update();

        for (int i = 0; i < pathFollower.pathPoints.Count; i++)
        {
            pathFollower.pathPoints[i] = DrawHandlesRecursively(pathFollower.pathPoints[i]);
        }

        serializedObject.ApplyModifiedProperties();
    }


}
