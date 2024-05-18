using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement
{
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _movementSpeed;
    Vector3 _direction;

    Transform _transform;
    PlayerCamera _camera;
    public Vector3 Direction { get { return _direction; } }

    Action<Vector3> _move;

    public PlayerMovement(Rigidbody rb, float speed, Transform t, PlayerCamera pCam)
    {
        _rb = rb;
        _movementSpeed = speed;
        _transform = t;
        _camera = pCam;

        _move = (x) => _rb.MovePosition(_transform.position + x);
    }

    public void Movement(float hAxis, float vAxis)
    {
        _direction = _camera.CameraRelativeRight * hAxis + _camera.CameraRelativeForward * vAxis;
        _direction.y = 0f;

        if (_direction.sqrMagnitude > 1)
            _direction.Normalize();

        Move(_direction * (_movementSpeed * Time.fixedDeltaTime));

        if (_direction.sqrMagnitude > 0f)
            _transform.forward = _direction;
    }

    public void Move(Vector3 dir)
    {
        _move(dir);
    }

    public void IsAttachedToParent(bool attach)
    {
        if (attach)
        { _move = (x) => _transform.position += x; return; }
        _move = (x) => _rb.MovePosition(_transform.position + x);
    }
}