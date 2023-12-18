using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour, IPathFollower
{
    [System.Serializable]
    public struct PathPoint
    {
        public Vector3 pointPosition;
        public bool hasRamification;
        public List<Vector3> ramificationPoints;
        public string name;
    }

    public List<PathPoint> pathPoints = new List<PathPoint>();
    private int currentPathIndex = 0;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        // Draw the path using Gizmos
        for (int i = 0; i < pathPoints.Count; i++)
        {
            Gizmos.DrawSphere(pathPoints[i].pointPosition, 0.1f);

            if (i < pathPoints.Count - 1)
            {
                Gizmos.DrawLine(pathPoints[i].pointPosition, pathPoints[i + 1].pointPosition);
            }

            if (pathPoints[i].hasRamification)
            {
                Gizmos.color = Color.red;
                foreach (var ramificationPoint in pathPoints[i].ramificationPoints)
                {
                    Gizmos.DrawLine(pathPoints[i].pointPosition, ramificationPoint);
                }
                Gizmos.color = Color.yellow;
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
        Ray ray = Camera.main.ScreenPointToRay(screenPoint);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 clickPoint = hit.point;

            PathPoint closestPoint = pathPoints[0];
            float minDistance = Vector3.Distance(clickPoint, closestPoint.pointPosition);

            foreach (var pathPoint in pathPoints)
            {
                float distance = Vector3.Distance(clickPoint, pathPoint.pointPosition);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestPoint = pathPoint;
                }

                if (pathPoint.hasRamification)
                {
                    foreach (var ramificationPoint in pathPoint.ramificationPoints)
                    {
                        distance = Vector3.Distance(clickPoint, ramificationPoint);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            closestPoint = pathPoint;
                        }
                    }
                }
            }
            return closestPoint;
        }

        return new PathPoint(); // Return an empty PathPoint if no hit is detected
    }

    public List<Vector3> GeneratePathToSelectedPoint(PathPoint selectedPoint)
    {
        List<Vector3> pathToSelectedPoint = new List<Vector3>();

        // Add the points from the current position to the selected point
        pathToSelectedPoint.Add(transform.position);

        // If the selected point is a ramification point, add its ramification points to the path
        if (selectedPoint.hasRamification)
        {
            pathToSelectedPoint.AddRange(selectedPoint.ramificationPoints);
        }

        // Add the selected point to the path
        pathToSelectedPoint.Add(selectedPoint.pointPosition);

        return pathToSelectedPoint;
    }
}
