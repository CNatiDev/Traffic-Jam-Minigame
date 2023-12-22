using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyRotable : MonoBehaviour
{
    public float rotationSpeed = 50f;

    void Update()
    {
        RotateMoney();
    }

    void RotateMoney()
    {

        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
