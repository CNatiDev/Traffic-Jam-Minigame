using UnityEngine;

/// <summary>
/// The Rotable class controls the rotation of the object based on a specified position.
/// </summary>
public class Rotable : MonoBehaviour, IRotable
{
    [SerializeField] private float rotationSpeed = 200f;

    private InputHandler inputHandler;

    private void Awake()
    {
        inputHandler = GetComponent<InputHandler>();
        inputHandler.OnRotate += RotateTowards;
    }

    /// <summary>
    /// Rotate the object towards a specified position in world space.
    /// </summary>
    /// <param name="targetPosition">The position to rotate the object towards.</param>
    public void RotateTowards()
    {
        if (Vector3.Distance(transform.position, RaycastUtility.GetMouseRaycastPoint()) > 0.9f)
        {
            Vector3 targetPosition = RaycastUtility.GetMouseRaycastPoint();
            Vector3 direction = targetPosition - transform.position;
            direction.y = 0f;
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
