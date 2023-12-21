using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Moveable : MonoBehaviour, IMoveable
{
    private Rigidbody rb;
    public float moveSpeed = 5;
    public bool stopCar { get; set; }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Freeze rotation to prevent unwanted physics behavior
        stopCar = false;
    }

    /// <summary>
    /// Move the object forward using physics.
    /// </summary>
    public void MoveForward()
    {
        if (!stopCar)
        {
            Vector3 movement = transform.forward * moveSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + movement);
        }

    }

    public void MoveForwardToPoint()
    {
        if (Vector3.Distance(transform.position, RaycastUtility.GetMouseRaycastPoint()) > 1.5f)
        {
            Debug.Log(Vector3.Distance(transform.position, RaycastUtility.GetMouseRaycastPoint()));
            MoveForward();
        }
    }
}
