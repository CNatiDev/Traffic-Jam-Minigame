using UnityEngine;

/// <summary>
/// The Rotatable class controls the rotation of the object based on a specified position.
/// </summary>
public class Rotatable : MonoBehaviour, IRotatable
{
    [SerializeField] private float rotationSpeed = 200f;

    /// <summary>
    /// Gets or sets the target point towards which the object should rotate.
    /// </summary>
    public Vector3 targetPoint { get; set; }

    /// <summary>
    /// Rotate the object towards a specified position in world space.
    /// </summary>
    /// <remarks>
    /// The rotation is performed only if the distance between the current object position
    /// and the mouse raycast point is greater than 0.9f.
    /// </remarks>
    public void RotateTowards()
    {
        // Check if the distance to the mouse raycast point is greater than 0.9f
        if (Vector3.Distance(transform.position, RaycastUtility.GetMouseRaycastPoint()) > 0.9f)
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
}
