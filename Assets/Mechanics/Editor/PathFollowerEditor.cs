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
    private void DrawButtons(Vector3 position, PathFollower.PathPoint pathPoint)
    {
        Handles.BeginGUI();

        // Create a Rect for the buttons next to the handle
        Rect buttonRect = new Rect(HandleUtility.WorldToGUIPoint(position) - new Vector2(30, 50), new Vector2(60, 20));

        GUILayout.BeginArea(new Rect(buttonRect.position, new Vector2(60, 20)));
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("+", GUILayout.Width(20), GUILayout.Height(20)))
        {
            Undo.RecordObject(pathFollower, "Add Ramification Point");
            if (pathPoint.hasRamification)
                pathPoint.ramificationPoints.Add(new PathFollower.RamificationPoint());
            else
                Debug.LogWarning("hasRamification is false");


        }
        if (GUILayout.Button("-", GUILayout.Width(20), GUILayout.Height(20)) && pathPoint.ramificationPoints.Count > 0)
        {
            Undo.RecordObject(pathFollower, "Remove Ramification Point");
            pathPoint.ramificationPoints.RemoveAt(pathPoint.ramificationPoints.Count - 1);
        }

        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        Handles.EndGUI();
    }
    private void DrawButtonsRamifications(Vector3 position, PathFollower.RamificationPoint pathPoint, bool hasRam)
    {
        Handles.BeginGUI();

        // Create a Rect for the buttons next to the handle
        Rect buttonRect = new Rect(HandleUtility.WorldToGUIPoint(position) - new Vector2(30, 50), new Vector2(60, 20));

        GUILayout.BeginArea(new Rect(buttonRect.position, new Vector2(60, 20)));
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("+", GUILayout.Width(20), GUILayout.Height(20)))
        {
            Undo.RecordObject(pathFollower, "Add Ramification Point");
            if (pathPoint.hasNestedPoint)
                pathPoint.nestedPathPoints.Add(new PathFollower.PathPoint());
            else
                Debug.LogWarning("hasNestedPoint is false");
        }
        if (GUILayout.Button("-", GUILayout.Width(20), GUILayout.Height(20)) && pathPoint.nestedPathPoints.Count > 0)
        {
            Undo.RecordObject(pathFollower, "Remove Ramification Point");
            pathPoint.nestedPathPoints.RemoveAt(pathPoint.nestedPathPoints.Count - 1);

        }

        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        Handles.EndGUI();
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
                if (ramificationPoint.hasNestedPoint)
                {
                    for (int k = 0; k < ramificationPoint.nestedPathPoints.Count; k++)
                    {
                        ramificationPoint.nestedPathPoints[k] = DrawHandlesRecursively(ramificationPoint.nestedPathPoints[k]);
                        // Move DrawButtons outside the EditorGUI.EndChangeCheck block
                    }
                }

                // Move DrawButtonsRamifications outside the EditorGUI.EndChangeCheck block
                DrawButtonsRamifications(newRamificationPosition, pathPoint.ramificationPoints[j], pathPoint.ramificationPoints[j].hasNestedPoint);

            }
        }

        // Move DrawButtons outside the EditorGUI.EndChangeCheck block
        DrawButtons(pathPoint.pointPosition, pathPoint);

        return pathPoint; // Return the updated path point
    }

    private void OnSceneGUI()
    {
        serializedObject.Update();

        for (int i = 0; i < pathFollower.pathPoints.Count; i++)
        {
            pathFollower.pathPoints[i] = DrawHandlesRecursively(pathFollower.pathPoints[i]);
            DrawButtons(pathFollower.pathPoints[i].pointPosition, pathFollower.pathPoints[i]);

            // Check if the main path point has any ramification 
            bool hasRamificationsOrNested = HasRamifications(pathFollower.pathPoints[i]);
            if (hasRamificationsOrNested && !pathFollower.pathPoints[i].hasRamification)
            {
                PathFollower.PathPoint point = pathFollower.pathPoints[i];
                point.hasRamification = true;
                pathFollower.pathPoints[i] = point;
            }
        }
        serializedObject.ApplyModifiedProperties();
    }

    private bool HasRamifications(PathFollower.PathPoint pathPoint)
    {
        if (pathPoint.ramificationPoints.Count > 0)
        {
            return true;
        }

        return false;
    }
}