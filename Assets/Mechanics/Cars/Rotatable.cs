using UnityEngine;

/// <summary>
/// The Rotable class controls the rotation of the object based on a specified position.
/// </summary>
public class Rotatable : MonoBehaviour, IRotatable
{
    [SerializeField] private float rotationSpeed = 200f;

    public Vector3 targetPoint { get; set;}

    /// <summary>
    /// Rotate the object towards a specified position in world space.
    /// </summary>
    /// <param name="targetPosition">The position to rotate the object towards.</param>
    public void RotateTowards()
    {
        if (Vector3.Distance(transform.position, RaycastUtility.GetMouseRaycastPoint()) > 0.9f)
        {
            Vector3 direction = targetPoint - transform.position;
            direction.y = 0f;
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
