using System.Collections.Generic;
using UnityEngine;
using static PathFollower;

public class Moveable : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 200f;

    private IPathFollower pathFollower;
    private Vector3 targetPosition;
    public GameObject target;
    public List<Vector3> pathToSelectedPoint;

    private void Start()
    {
        pathFollower = GetComponent<IPathFollower>();
    }

    void Update()
    {
        if (pathFollower == null)
        {
            Debug.LogWarning("pathFollower is null in Update.");
            return;
        }

        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButton(0))
        {
            target.transform.position = targetPosition;
            FollowClosestPoint();
        }
    }

    private void LogDebug(string message)
    {
#if UNITY_EDITOR
        Debug.Log(message);
#endif
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void FollowPath(Vector3 nextPathPoint)
    {
        //nextPathPoint = pathFollower.GetNextPathPoint();
        if(Vector3.Distance(transform.position, nextPathPoint)>0.1f)
        {
            if (Vector3.Distance(transform.position, nextPathPoint) < 0.9f)
            {
                pathFollower.MoveToNextPoint();
            }

            MoveForward();
            RotateTowards(nextPathPoint);
        }

    }

    private void FollowClosestPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            targetPosition = hit.point;

            // Find the closest path point (either main point or ramification point)
            PathFollower.PathPoint closestPoint = pathFollower.FindClosestPoint(targetPosition);
            Debug.Log(closestPoint.name);

            // Generate a path to the selected point
            pathToSelectedPoint = pathFollower.GeneratePathToSelectedPoint(closestPoint);
            
            // Log the generated path for debugging
            foreach (var point in pathToSelectedPoint)
            {
                FollowPath(point);
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        // Draw the original path using Gizmos
        for (int i = 0; i < pathToSelectedPoint.Count; i++)
        {
            Gizmos.DrawSphere(pathToSelectedPoint[i], 0.1f);

            if (i < pathToSelectedPoint.Count - 1)
            {
                Gizmos.DrawLine(pathToSelectedPoint[i], pathToSelectedPoint[i + 1]);
            }
        }
    }
}
