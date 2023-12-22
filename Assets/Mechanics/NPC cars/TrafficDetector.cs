using UnityEngine;

/// <summary>
/// Represents a proximity sensor for detecting obstacles in the environment.
/// </summary>
public class TrafficDetector : MonoBehaviour
{
    [Header("Sensor Settings")]
    [Tooltip("Minimum distance to consider an obstacle.")]
    public float minDistance;

    [Tooltip("Tag for the detectedObjectTag.")]
    public string detectedObjectTag;

    [Tooltip("Transform representing the front line sensor.")]
    public Transform frontLineSensor;

    [Tooltip("Transform representing the front sphere sensor.")]
    public Transform frontSphereSensor;

    [Tooltip("Transform representing the left sphere sensor.")]
    public Transform LSensorPosition;

    [Tooltip("Transform representing the right sphere sensor.")]
    public Transform RSensorPosition;

    [Tooltip("Range of the sphere sensor.")]
    public float sphereSensorRange;

    [Tooltip("Range of the front-facing sphere sensor.")]
    public float sphereFrontSensorRange;

    [Tooltip("Flag indicating whether this sensor car is on the main street.")]
    public bool isFacingOnMainStreet = true;

    private const int MaxColliders = 10; // Adjust as needed

    /// <summary>
    /// Draws gizmos for visualization in the editor.   
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = ToClose() ? Color.red : Color.green;

        Gizmos.DrawLine(frontLineSensor.position, frontLineSensor.position + frontLineSensor.forward * minDistance);
        Gizmos.DrawWireSphere(LSensorPosition.position, sphereSensorRange);
        Gizmos.DrawWireSphere(RSensorPosition.position, sphereSensorRange);
        Gizmos.DrawWireSphere(frontSphereSensor.position, sphereFrontSensorRange);
    }

    /// <summary>
    /// Checks if an obstacle is too close based on various sensor positions.
    /// </summary>
    /// <returns>True if an obstacle is too close; otherwise, false.</returns>
    public bool ToClose()
    {
        if (CheckSensor(frontLineSensor.position, frontLineSensor.forward * minDistance))
            return true;

        if (CheckSphereSensors(LSensorPosition.position, isFacingOnMainStreet, sphereSensorRange))
            return true;

        if (CheckSphereSensors(RSensorPosition.position, isFacingOnMainStreet, sphereSensorRange))
            return true;

        if (CheckSphereSensors(frontSphereSensor.position, isFacingOnMainStreet, sphereFrontSensorRange))
            return true;

        return false;
    }

    /// <summary>
    /// Checks a sensor using a raycast to detect obstacles.
    /// </summary>
    /// <param name="position">Starting position of the sensor.</param>
    /// <param name="direction">Direction of the sensor.</param>
    /// <returns>True if an obstacle is detected; otherwise, false.</returns>
    private bool CheckSensor(Vector3 position, Vector3 direction)
    {
        Ray ray = new Ray(position, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, minDistance) &&
            hit.collider.CompareTag(detectedObjectTag))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Checks a sphere sensor to detect obstacles.
    /// </summary>
    /// <param name="position">Position of the sphere sensor.</param>
    /// <param name="isMain">Flag indicating whether the sensor is on the main street.</param>
    /// <param name="range">Range of the sphere sensor.</param>
    /// <returns>True if an obstacle is detected; otherwise, false.</returns>
    private bool CheckSphereSensors(Vector3 position, bool isMain, float range)
    {
        Collider[] colliders = new Collider[MaxColliders];
        int numColliders = Physics.OverlapSphereNonAlloc(position, range, colliders);

        for (int i = 0; i < numColliders; i++)
        {
            if (colliders[i].CompareTag(detectedObjectTag) &&
                colliders[i].transform != transform &&
                colliders[i].TryGetComponent<CarNpc>(out var carNpc) &&
                carNpc.mainStreetCar != isMain)
            {
                return true;
            }
        }

        return false;
    }
}
