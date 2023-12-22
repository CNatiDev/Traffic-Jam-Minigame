using UnityEngine;

public interface IRotatable {
    void RotateTowards();
    Vector3 targetPoint { get; set; }
}
public interface IMoveable {
    void MoveForward();
    void MoveForwardToPoint();
    bool stopCar { get; set; }
    float carSpeed { get; set; }
}
