using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Represents the follower for a predefined path.
/// </summary>
public class PathFollower : MonoBehaviour
{
    public List<Vector3> pathPoints = new List<Vector3>();

    private int currentPathIndex = 0;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        // Draw the path using Gizmos
        for (int i = 0; i < pathPoints.Count; i++)
        {
            Gizmos.DrawSphere(pathPoints[i], 0.1f);

            if (i < pathPoints.Count - 1)
            {
                Gizmos.DrawLine(pathPoints[i], pathPoints[i + 1]);
            }
        }
    }


    /// <summary>
    /// Gets the next point in the path.
    /// </summary>
    /// <returns>The next point in the path.</returns>
    public Vector3 GetNextPathPoint()
    {
        if (currentPathIndex < pathPoints.Count)
        {
            return pathPoints[currentPathIndex];
        }
        return Vector3.zero;
    }

    /// <summary>
    /// Move to the next point in the path.
    /// </summary>
    public void MoveToNextPoint()
    {
        currentPathIndex++;

        if (currentPathIndex >= pathPoints.Count)
        {
            currentPathIndex = 0;
        }
    }
}
