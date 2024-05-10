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
        _direction = playerCamera.CameraRelativeRight * hAxis + playerCamera.CameraRelativeForward * vAxis;
        _direction.y = 0f;

        if (_direction.sqrMagnitude > 1)
            _direction.Normalize();

        _rb.MovePosition(transform.position + _direction * (_movementSpeed * Time.deltaTime));

        transform.forward =  _direction;
    }
}