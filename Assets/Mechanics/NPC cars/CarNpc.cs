using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarNpc : MonoBehaviour
{
    public event Action OnMove = delegate { };

    private Moveable carMove;
    private ProximitySensor proximitySensor;
    public bool mainStreetCar;


    private void Awake()
    {   
        carMove = GetComponent<Moveable>();
        proximitySensor = GetComponent<ProximitySensor>();
        OnMove += carMove.MoveForward;        
    }
    void FixedUpdate()
    {
        OnMove();
        if (proximitySensor.ToClose())
        {
            carMove.moveSpeed = 0;
        }
        else
        {
            carMove.moveSpeed = 5.0f;
        }
    }
    public static bool StopCar()
    {
        return false;
    }
}
