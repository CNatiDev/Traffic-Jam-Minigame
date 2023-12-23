using UnityEngine;

/// <summary>
/// Represents a rotatable non-player controlled character in the game.
/// </summary>
public class BlueCarRotable : MonoBehaviour, IRotatable
{
    [SerializeField] private float rotationSpeed = 200f;

    /// <summary>
    /// Gets or sets the target point towards which the object should rotate.
    /// </summary>
    public Vector3 targetPoint { get; set; }

    /// <summary>
    /// Rotate the object towards a specified position in world space.
    /// </summary>
    public void RotateTowards()
    {
        // Calculate the direction vector towards the target point
        Vector3 direction = targetPoint - transform.position;
        direction.y = 0f;

        // Calculate the target rotation based on the direction
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        // Smoothly rotate towards the target rotation using rotationSpeed and Time.deltaTime
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
