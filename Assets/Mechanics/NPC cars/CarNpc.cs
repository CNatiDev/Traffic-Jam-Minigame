using System;
using UnityEngine;

/// <summary>
/// Represents a non-player controlled car in the game.
/// </summary>
public class CarNpc : MonoBehaviour
{
    /// <summary>
    /// Event triggered when the car is supposed to move.
    /// </summary>
    public event Action OnMove = delegate { };

    /// <summary>
    /// Event triggered when the car is supposed to rotate.
    /// </summary>
    public event Action OnRotate = delegate { };

    // Interfaces for movement and rotation behaviors
    private IMoveable carMove;
    private IRotatable carRotate;

    // Proximity sensor to detect nearby obstacles
    private ProximitySensor proximitySensor;

    // Flag indicating whether the car is on the main street, for the sensor to know if need to ignore th car
    public bool mainStreetCar;

    // Target point for the car to navigate towards
    public Transform dezactivatorPosition;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        // Initialize interfaces and events
        carMove = GetComponent<IMoveable>();
        carRotate = GetComponent<IRotatable>();
        proximitySensor = GetComponent<ProximitySensor>();

        // Subscribe methods to events
        OnMove += carMove.MoveForward;
        OnRotate += carRotate.RotateTowards;
    }

    /// <summary>
    /// FixedUpdate is called every fixed framerate frame.
    /// </summary>
    private void FixedUpdate()
    {
        // Check proximity sensor to determine if the car should stop
        GetComponent<IMoveable>().stopCar = proximitySensor.ToClose();

        // Trigger move and rotate events
        OnMove();
        OnRotate();
    }
}
