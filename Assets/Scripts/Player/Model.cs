using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(View))]
public class Model : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _movementSpeed;
    Vector3 _direction;

    [SerializeField] PlayerCamera _playerCamera;

    [SerializeField] View _view;
    Controller _controller;

    [SerializeField] HitBox _bladeHitBox;
    [SerializeField] float _dmgA, _dmgB, _dmgC;

    event Action<float> OnMovement = delegate { };
    event Action<bool> OnAttackA = delegate { };
    event Action<bool> OnAttackB = delegate { };
    event Action<bool> OnAttackC = delegate { };

    private void Start()
    {
        _controller = new Controller(this, _view);
        _bladeHitBox.SetDamage(_dmgA);
    }

    public void AddToOnMovement(Action<float> method)
    {
        OnMovement += method;
    }

    public void AddToOnAttackA(Action<bool> method)
    {
        OnAttackA += method;
    }

    public void AddToOnAttackB(Action<bool> method)
    {
        OnAttackB += method;
    }

    public void AddToOnAttackC(Action<bool> method)
    {
        OnAttackC += method;
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

    public void AttackA()
    {
        _bladeHitBox.SetDamage(_dmgA);
        OnAttackA(true);
        OnAttackB(false);
    }

    public void AttackB()
    {
        _bladeHitBox.SetDamage(_dmgB);
        OnAttackA(false);
        OnAttackB(true);
    }

    public void AttackC()
    {
        _bladeHitBox.SetDamage(_dmgC);
        OnAttackA(true);
        OnAttackB(true);
    }

    public void SetAttacksFalse()
    {
        OnAttackA(false);
        OnAttackB(false);
    }
}