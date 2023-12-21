using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Moveable : MonoBehaviour, IMoveable
{
    public float moveSpeed = 5f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Freeze rotation to prevent unwanted physics behavior
    }

    /// <summary>
    /// Move the object forward using physics.
    /// </summary>
    public void MoveForward()
    {
        Vector3 movement = transform.forward * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    public void MoveForwardToPoint()
    {
        if (Vector3.Distance(transform.position, RaycastUtility.GetMouseRaycastPoint()) > 0.9f)
        {
            MoveForward();
        }
    }
}
