using UnityEngine;

/// <summary>
/// Represents a rotatable non-player controlled character in the game.
/// </summary>
public class NpcRotable : MonoBehaviour, IRotatable
{
    [SerializeField] private float rotationSpeed = 200f;

    /// <summary>
    /// The target point to rotate towards.
    /// </summary>
    public Transform point;

    /// <summary>
    /// Rotate the object towards a specified position in world space.
    /// </summary>
    public void RotateTowards()
    {
        // Check if a target point is assigned
        if (point != null)
        {
            Vector3 targetPosition = point.position;
            Vector3 direction = targetPosition - transform.position;
            direction.y = 0f;

            // Calculate the target rotation based on the direction
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

            // Smoothly rotate towards the target rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
