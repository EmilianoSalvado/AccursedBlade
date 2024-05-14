using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _movementSpeed;
    Vector3 _direction;
    public Vector3 Direction { get { return _direction; } }

    public void Movement(float hAxis, float vAxis, PlayerCamera playerCamera)
    {
        if (hAxis <= 0f && vAxis <= 0f)
        { return; }

        _direction = playerCamera.CameraRelativeRight * hAxis + playerCamera.CameraRelativeForward * vAxis;
        _direction.y = 0f;

        Debug.Log($"_dir: {_direction}");

        if (_direction.sqrMagnitude > 1)
            _direction.Normalize();

        _rb.MovePosition(transform.position + _direction * (_movementSpeed * Time.deltaTime));

        if (_direction.sqrMagnitude > 0f)
            transform.forward = _direction;
    }
}