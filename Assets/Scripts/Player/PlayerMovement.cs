using System;
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

        if (_direction.sqrMagnitude > 1)
            _direction.Normalize();

        Move(_direction * (_movementSpeed * Time.fixedDeltaTime));

        if (_direction.sqrMagnitude > 0f)
            transform.forward = _direction;
    }

    public void Move(Vector3 dir)
    {
        _rb.MovePosition(transform.position + dir);
    }
}