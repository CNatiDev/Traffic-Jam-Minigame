using System.Collections.Generic;
using UnityEngine;
using static PathFollower;

public interface IPathFollower
{
    Vector3 GetNextPathPoint();
    void MoveToNextPoint();
    PathPoint FindClosestPoint(Vector3 point);
    List<Vector3> GeneratePathToSelectedPoint(PathPoint selectedPoint);
}