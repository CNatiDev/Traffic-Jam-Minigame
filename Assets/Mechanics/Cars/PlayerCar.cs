using UnityEngine;

/// <summary>
/// Represents the player-controlled car in the game.
/// </summary>
public class PlayerCar : MonoBehaviour
{
    // Variables for car movement
    public float moveSpeed = 5f;
    public float rotationSpeed = 200f;

    // Path following
    public PathFollower pathFollower;

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
        HandleInput();
    }

    /// <summary>
    /// Handles player input to move and rotate the car.
    /// </summary>
    private void HandleInput()
    {
        if (Input.GetMouseButton(0))
        {
            FollowPath();

        }
    }

    /// <summary>
    /// Moves the car forward based on its speed.
    /// </summary>
    private void MoveForward()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
    /// <summary>
    /// Rotates the car towards a specified position, considering only the Y-axis.
    /// </summary>
    /// <param name="targetPosition">The target position to rotate towards.</param>
    private void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0f; // Ignore the y-axis for rotation
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }



    /// <summary>
    /// Follows the predefined path.
    /// </summary>
    private void FollowPath()
    {
        Vector3 targetPosition = pathFollower.GetNextPathPoint();
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Check if we are close to the target point
        if (Vector3.Distance(transform.position, targetPosition) < 0.9f)
        {
            pathFollower.MoveToNextPoint();
        }

        // Move forward
        MoveForward();

        // Rotate towards the next point in the path
        RotateTowards(pathFollower.GetNextPathPoint());
        Debug.Log(pathFollower.GetNextPathPoint());
    }
}
