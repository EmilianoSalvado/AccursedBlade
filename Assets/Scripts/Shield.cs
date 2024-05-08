using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour, IDamageTaker
{
    [SerializeField] Collider _collider;
    [SerializeField] EnergySystem _energySystem;
    [SerializeField] MeshRenderer _meshRenderer;

    public void GetDamage(float dmg)
    {
        _energySystem.SpendEnergy(dmg);
        if (!_energySystem.Available)
        { ShieldOn(false); return; }
    }

    public void ShieldOn(bool onOff)
    {
        if (onOff && !_energySystem.Available)
        {
            ShieldOn(false);
            return;
        }

        _collider.enabled = onOff;
        _meshRenderer.enabled = onOff;
    }
}