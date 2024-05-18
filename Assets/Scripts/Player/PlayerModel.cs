using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(View))]
public class PlayerModel : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _movementSpeed;
    [SerializeField] PlayerCamera _playerCamera;
    [SerializeField] PlayerCombat _playerCombat;
    [SerializeField] Curse _curse;

    [SerializeField] View _view;
    PlayerController _controller;
    PlayerMovement _playerMovement;
    public PlayerMovement GetPlayerMovement { get { return _playerMovement; } }

    [SerializeField] DamageDoer _bladeHitBox;
    [SerializeField] float _dmgA, _dmgB, _dmgC;
    [SerializeField] StaminaSystem _staminaSystem;
    [SerializeField] Shield _shield;
    [SerializeField] EnergySystem _energySystem;

    [SerializeField] Collider _shieldCollider;

    event Action<float> OnMovement = delegate { };
    event Action<bool> OnAttack = delegate { };
    event Action<bool> OnSheathOrUnsheath = delegate { };
    event Action<bool> OnCurse = delegate { };
    event Action<bool> OnBlock = delegate { };

    private void Start()
    {
        _controller = new PlayerController(this, _view);
        _playerMovement = new PlayerMovement(_rb, _movementSpeed, transform, _playerCamera);
        _bladeHitBox.SetDamage(_dmgA);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void AddToOnMovement(Action<float> method)
    { OnMovement += method; }

    public void AddToOnAttack(Action<bool> method)
    { OnAttack += method; }

    public void AddToOnSheathOrUnsheath(Action<bool> method)
    { OnSheathOrUnsheath += method; }

    public void AddToOnCurse(Action<bool> method)
    { OnCurse += method; }

    public void AddToOnBlock(Action<bool> method)
    { OnBlock += method; }

    private void Update()
    {
        _playerCamera.UpdatePivot();
        _controller.OnUpdate();
    }

    private void FixedUpdate()
    { _controller.OnFixedUpdate(); }

    private void LateUpdate()
    { _controller.OnLateUpdate(); }

    public void Movement(float hAxis, float vAxis)
    {
        _playerMovement.Movement(hAxis, vAxis);
        OnMovement(_playerMovement.Direction.sqrMagnitude);
    }

    public void CameraAim(float mouseX, float mouseY)
    { _playerCamera.CameraMovement(mouseX, mouseY); }

    public void Attack()
    {
        if (!_staminaSystem.Available || _playerCombat.Sheathed) return;
        _bladeHitBox.SetDamage(_dmgA);
        _playerCombat.Attack();
        _curse.Cursed(!_playerCombat.Sheathed);
        OnAttack(!_playerCombat.Sheathed);
        OnCurse(!_playerCombat.Sheathed);
        StartCoroutine(CheckForAttackBoolChange());
    }

    public void Sheath()
    {
        _playerCombat.Sheath();
        _curse.Cursed(!_playerCombat.Sheathed);
        OnCurse(!_playerCombat.Sheathed);
        OnSheathOrUnsheath(!_playerCombat.Sheathed);
        StartCoroutine(CheckForSheathBoolChange());
    }

    IEnumerator CheckForAttackBoolChange()
    {
        yield return new WaitForSeconds(.5f);

        if (!enabled)
            yield return new WaitUntil(() => enabled);

        OnAttack(false);
    }

    IEnumerator CheckForSheathBoolChange()
    {
        yield return new WaitUntil(() => _view.GetAnimator.GetBool("Sheathed") == true);

        if (!enabled)
            yield return new WaitUntil(() => enabled);

        OnCurse(false);
    }

    public void ShieldOn(bool onOff)
    {
        _shield.ShieldOn(onOff);
        //OnBlock(onOff);
    }
}