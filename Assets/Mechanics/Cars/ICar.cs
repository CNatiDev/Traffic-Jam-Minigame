public interface IRotatable {
    void RotateTowards();
}
public interface IMoveable {
    void MoveForward();
    void MoveForwardToPoint();
    bool stopCar { get; set; }
}
