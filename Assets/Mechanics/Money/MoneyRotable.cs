using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The MoneyRotable class rotates the money object around its up axis.
/// </summary>
public class MoneyRotable : MonoBehaviour
{
    /// <summary>
    /// The rotation speed of the money object.
    /// </summary>
    public float rotationSpeed = 50f;

    /// <summary>
    /// Update is called once per frame. It rotates the money object.
    /// </summary>
    void Update()
    {
        RotateMoney();
    }

    /// <summary>
    /// Rotates the money object around its up axis.
    /// </summary>
    void RotateMoney()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
