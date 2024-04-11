using UnityEngine;
using System;

[RequireComponent(typeof (Rigidbody))]
[RequireComponent(typeof (Collider))]
[RequireComponent(typeof (View))]
public class Model : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _movementSpeed;
    Vector3 _direction;

    [SerializeField] PlayerCamera _playerCamera;

    [SerializeField] View _view;
    Controller _controller;

    event Action<float> OnMovement = delegate { };

    private void Start()
    {
        _controller = new Controller(this, _view);
    }

    public void AddToOnMovement(Action<float> method)
    {
        OnMovement += method;
    }

    private void Update()
    {
        _playerCamera.UpdatePivot();
        _controller.OnUpdate();
    }

    private void FixedUpdate()
    {
        _controller.OnFixedUpdate();
    }

    private void LateUpdate()
    {
        _controller.OnLateUpdate();
    }

    public void Movement(float hAxis, float vAxis)
    {
        _direction = _playerCamera.CameraRelativeRight * hAxis + _playerCamera.CameraRelativeForward * vAxis;

        if (_direction.sqrMagnitude > 1)
            _direction.Normalize();

        _rb.MovePosition(transform.position + _direction * (_movementSpeed * Time.deltaTime));

        if (_direction.sqrMagnitude > 0f)
            transform.forward = Vector3.Lerp(transform.forward, new Vector3(_direction.x, transform.forward.y, _direction.z), _direction.sqrMagnitude);

        OnMovement(_direction.sqrMagnitude);
    }

    public void CameraAim(float mouseX, float mouseY)
    {
        _playerCamera.CameraMovement(mouseX, mouseY);
    }
}