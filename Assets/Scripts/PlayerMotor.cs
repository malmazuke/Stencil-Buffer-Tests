using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{
    public Rigidbody _rb;

    public float _speed;

    private Vector2 _movementDirection;

    public InputActionReference move;

    private void Update()
    {
        _movementDirection = move.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = new Vector3(_movementDirection.x * _speed, 0.0f, _movementDirection.y * _speed);
    }
}
