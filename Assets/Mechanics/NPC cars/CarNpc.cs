using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarNpc : MonoBehaviour
{
    public event Action OnMove = delegate { };
    private Moveable carMove;

    private void Awake()
    {   
        carMove = GetComponent<Moveable>();
        OnMove += carMove.MoveForward;        
    }
    void Update()
    {
        OnMove();
    }
}
