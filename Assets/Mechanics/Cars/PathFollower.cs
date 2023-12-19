using System;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour, IPathFollower
{
    [System.Serializable]
    public struct PathPoint
    {
        public Vector3 pointPosition;
        public bool hasRamification;
        public List<RamificationPoint> ramificationPoints;
        public string name;
    }

    [System.Serializable]
    public struct RamificationPoint
    {
        public Vector3 pointPosition;
        public string name; 
        public bool hasRamification1;
        public List<PathPoint> nestedPathPoints;
    }

    public List<PathPoint> pathPoints = new List<PathPoint>();
    private int currentPathIndex = 0;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        // Draw the original path using Gizmos
        for (int i = 0; i < pathPoints.Count; i++)
        {
            Gizmos.DrawSphere(pathPoints[i].pointPosition, 0.1f);

            if (i < pathPoints.Count - 1)
            {
                Gizmos.DrawLine(pathPoints[i].pointPosition, pathPoints[i + 1].pointPosition);
            }

        }


        // Draw the original path using Gizmos
        for (int i = 0; i < pathPoints.Count; i++)
        {
            DrawPathPointGizmos(pathPoints[i]);
        }
    }

    void DrawPathPointGizmos(PathPoint pathPoint)
    {
        Gizmos.DrawSphere(pathPoint.pointPosition, 0.1f);

        if (pathPoint.hasRamification)
        {
            Gizmos.color = Color.red;
            foreach (var ramificationPoint in pathPoint.ramificationPoints)
            {
                Gizmos.DrawLine(pathPoint.pointPosition, ramificationPoint.pointPosition);
                DrawRamificationGizmos(pathPoint, ramificationPoint);
            }
            Gizmos.color = Color.yellow;
        }
    }

    void DrawRamificationGizmos(PathPoint pathPoint, RamificationPoint ramificationPoint)
    {
        Gizmos.DrawLine(pathPoint.pointPosition, ramificationPoint.pointPosition);

        if (ramificationPoint.hasRamification1)
        {
            foreach (var point in ramificationPoint.nestedPathPoints)
            {
                Gizmos.DrawLine(ramificationPoint.pointPosition, point.pointPosition);
                DrawPathPointGizmos(point);
            }
        }
    }


    public Vector3 GetNextPathPoint()
    {
        if (currentPathIndex < pathPoints.Count)
        {
            return pathPoints[currentPathIndex].pointPosition;
        }
        return Vector3.zero;
    }

    public void MoveToNextPoint()
    {
        currentPathIndex++;

        if (currentPathIndex >= pathPoints.Count)
        {
            currentPathIndex = 0;
        }
    }

    public PathPoint FindClosestPoint(Vector3 screenPoint)
    {
        PathPoint closestPoint = new PathPoint();
        float minDistance = float.MaxValue;
        foreach (var point in pathPoints)
        {
            if (Vector3.Distance(point.pointPosition, screenPoint) < minDistance)
            {
                closestPoint = point;
                minDistance = Vector3.Distance(point.pointPosition, screenPoint);
            }
            if (closestPoint.hasRamification)
            {
                foreach (var ramPoint in closestPoint.ramificationPoints)
                {
                    if (Vector3.Distance(ramPoint.pointPosition, screenPoint) < minDistance)
                    {
                        // Handle the closest ramification point if needed
                    }
                }
            }
        }
        return closestPoint;
    }

    public List<Vector3> GeneratePathToSelectedPoint(PathPoint selectedPoint)
    {
        List<Vector3> pathToSelectedPoint = new List<Vector3>();
        // Implement path generation logic here
        return pathToSelectedPoint;
    }
}
