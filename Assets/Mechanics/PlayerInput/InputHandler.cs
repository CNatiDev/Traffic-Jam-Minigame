using System;
using UnityEngine;

/// <summary>
/// The InputHandler class handles user input
/// </summary>
public class InputHandler : MonoBehaviour
{
    public event Action OnMove = delegate { };
    public event Action OnRotate = delegate { };

    private IMoveable moveable;
    private IRotatable rotatable;
    private void Awake()
    {
        moveable = GetComponent<IMoveable>();
        rotatable = GetComponent<IRotatable>();

        if (moveable != null)
            OnMove += moveable.MoveForwardToPoint;
        if (rotatable != null)
            OnRotate += rotatable.RotateTowards;
    }
    public bool move {  get; private set; }
    private void FixedUpdate()
    {
        move = Input.GetMouseButton(0);
        if (move && RaycastUtility.GetMouseRaycastPoint() != Vector3.zero)
        {
            OnMove();
            OnRotate();
        }
    }

}
