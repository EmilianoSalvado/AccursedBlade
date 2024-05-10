using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;
    [SerializeField] Transform _dirPoint;
    [SerializeField] float _movementSpeed;
    Vector3 _direction;
    public Vector3 Direction { get { return _direction; } }

    public void Movement(float hAxis, float vAxis, PlayerCamera playerCamera)
    {
        _direction = playerCamera.CameraRelativeRight * hAxis + playerCamera.CameraRelativeForward * vAxis;

        if (_direction.sqrMagnitude > 1)
            _direction.Normalize();

        _rb.MovePosition(transform.position + _direction * (_movementSpeed * Time.deltaTime));
        _dirPoint.position = transform.position + new Vector3(_direction.normalized.x,0f, _direction.normalized.z);

        if (_direction.sqrMagnitude > 0f)
            transform.LookAt(_dirPoint.position);
    }
}