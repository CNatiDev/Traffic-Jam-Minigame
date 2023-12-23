using System;
using UnityEngine;

/// <summary>
/// The InputHandler class handles user input for movement and rotation.
/// </summary>
public class InputHandler : MonoBehaviour
{
    /// <summary>
    /// Event triggered when the object is supposed to move.
    /// </summary>
    public event Action OnMove = delegate { };

    /// <summary>
    /// Event triggered when the object is supposed to rotate.
    /// </summary>
    public event Action OnRotate = delegate { };

    // Interfaces for movement and rotation behaviors
    private IMoveable moveable;
    private IRotatable rotatable;


    //Game Manager instance for getting player GameObject
    private GameManager gameManager;

    /// <summary>
    /// Gets a value indicating whether the object should move.
    /// </summary>
    public bool move { get; private set; }

    private void Start()
    {
        //Assign gameManager with Game Manager instance, using Singleton
        gameManager = GameManager.Instance;

        // Initialize interfaces and events
        moveable = gameManager.playerCar.GetComponent<IMoveable>();
        rotatable = gameManager.playerCar.GetComponent<IRotatable>();

        // Subscribe methods to events if the corresponding interfaces are implemented
        if (moveable != null)
            OnMove += moveable.MoveForwardToPoint;
        if (rotatable != null)
            OnRotate += rotatable.RotateTowards;
    }
    private void FixedUpdate()
    {
        // Check for user input and mouse position
        move = Input.GetMouseButton(0);
        if (move && RaycastUtility.GetMouseRaycastPoint() != Vector3.zero)
        {
            // Trigger move and rotate events
            OnMove();

            rotatable.targetPoint = RaycastUtility.GetMouseRaycastPoint();
            OnRotate();
        }
    }
}
