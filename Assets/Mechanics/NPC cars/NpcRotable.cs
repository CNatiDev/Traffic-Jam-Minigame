using UnityEngine;
/// <summary>
/// Represents a rotatable non-player controlled character in the game.
/// </summary>
public class NpcRotable : MonoBehaviour, IRotatable
{
    [SerializeField] private float rotationSpeed = 200f;

    public Vector3 targetPoint { get; set; }


    /// <summary>
    /// Rotate the object towards a specified position in world space.
    /// </summary>
    public void RotateTowards()
    {
            Vector3 direction = targetPoint - transform.position;
            direction.y = 0f;

            // Calculate the target rotation based on the direction
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

            // Smoothly rotate towards the target rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}