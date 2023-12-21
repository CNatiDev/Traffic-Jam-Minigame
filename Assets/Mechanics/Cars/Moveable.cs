using UnityEngine;

/// <summary>
/// The Moveable class controls the movement of the object based on user input.
/// </summary>
public class Moveable : MonoBehaviour, IMoveable
{
    [SerializeField] private float moveSpeed = 5f;

    private InputHandler inputHandler;

    private void Awake()
    {

        inputHandler = GetComponent<InputHandler>();
        if (inputHandler != null )
        inputHandler.OnRotate += MoveForward;
    }


    /// <summary>
    /// Move the object forward in local space.
    /// </summary>
    public void MoveForward()
    {
        if (Vector3.Distance(transform.position, RaycastUtility.GetMouseRaycastPoint()) > 0.9f)
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
    }
}
