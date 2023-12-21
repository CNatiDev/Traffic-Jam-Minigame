using System;
using UnityEngine;

/// <summary>
/// The InputHandler class handles user input
/// </summary>
public class InputHandler : MonoBehaviour
{
    public event Action OnMove = delegate { };
    public event Action OnRotate = delegate { };

    public bool move {  get; private set; }
    private void Update()
    {
        move = Input.GetMouseButton(0);
        if (move)
        {
            OnMove();
            OnRotate();
        }
    }

}
