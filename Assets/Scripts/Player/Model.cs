using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(View))]
public class Model : MonoBehaviour
{
    [SerializeField] PlayerMovement _playerMovement;

    [SerializeField] PlayerCamera _playerCamera;
    [SerializeField] PlayerCombat _playerCombat;

    [SerializeField] View _view;
    Controller _controller;

    [SerializeField] DamageDoer _bladeHitBox;
    [SerializeField] float _dmgA, _dmgB, _dmgC;
    [SerializeField] StaminaSystem _staminaSystem;
    [SerializeField] Shield _shield;
    [SerializeField] EnergySystem _energySystem;

    [SerializeField] Collider _shieldCollider;

    event Action<float> OnMovement = delegate { };
    event Action<bool> OnAttackA = delegate { };
    event Action<bool> OnAttackB = delegate { };
    event Action<bool> OnBlock = delegate { };

    private void Start()
    {
        _controller = new Controller(this, _view);
        _bladeHitBox.SetDamage(_dmgA);
        Cursor.lockState = CursorLockMode.Locked;
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

    public void AddToOnBlock(Action<bool> method)
    {
        OnBlock += method;
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
        _playerMovement.Movement(hAxis, vAxis, _playerCamera);
        OnMovement(_playerMovement.Direction.sqrMagnitude);
    }

    public void CameraAim(float mouseX, float mouseY)
    {
        _playerCamera.CameraMovement(mouseX, mouseY);
    }

    public void Attack()
    {
        if (!_staminaSystem.Available) return;
        _bladeHitBox.SetDamage(_dmgA);
        _playerCombat.Attack();
    }

    public void SetAttacksFalse()
    {
        OnAttackA(false);
        OnAttackB(false);
    }

    public void ShieldOn(bool onOff)
    {
        _shield.ShieldOn(onOff);
        //OnBlock(onOff);
    }
}