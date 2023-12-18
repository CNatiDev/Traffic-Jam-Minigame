using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 200f;

    private IPathFollower pathFollower;
    private Vector3 targetPosition;

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
        if (Input.GetMouseButtonDown(0))
        {
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
        nextPathPoint = pathFollower.GetNextPathPoint();

        if (Vector3.Distance(transform.position, nextPathPoint) < 0.9f)
        {
            pathFollower.MoveToNextPoint();
        }

        MoveForward();
        RotateTowards(nextPathPoint);
        LogDebug(nextPathPoint.ToString());
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

            // Generate a path to the selected point
            List<Vector3> pathToSelectedPoint = pathFollower.GeneratePathToSelectedPoint(closestPoint);
            // Log the generated path for debugging
            foreach (var point in pathToSelectedPoint)
            {
                FollowPath(point);
            }
        }
    }
}
