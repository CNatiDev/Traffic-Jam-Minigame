using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Moveable : MonoBehaviour, IMoveable
{
    private Rigidbody rb;

    /// <summary>
    /// Gets or sets a value indicating whether the car movement should be stopped.
    /// </summary>
    public bool stopCar { get; set; }

    /// <summary>
    /// Gets or sets the speed of the car.
    /// </summary>
    public float carSpeed { get; set; }

    private void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Freeze rotation to prevent unwanted physics behavior
        rb.freezeRotation = true;

        // Initialize stopCar to false
        stopCar = false;
    }

    /// <summary>
    /// Move the object forward using physics.
    /// </summary>
    public void MoveForward()
    {
        // Check if the car should not be stopped
        if (!stopCar)
        {
            // Calculate the movement vector and move the car using physics
            Vector3 movement = transform.forward * carSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + movement);
        }
    }

    /// <summary>
    /// Move the object forward towards a specified point if the distance is greater than 1.5f.
    /// </summary>
    public void MoveForwardToPoint()
    {
        // Check if the distance to the target point is greater than 1.5f
        if (Vector3.Distance(transform.position, RaycastUtility.GetMouseRaycastPoint()) > 1.5f)
        {
            // Move the car forward
            MoveForward();
        }
    }
}
