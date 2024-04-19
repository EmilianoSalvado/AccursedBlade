using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] float _maxHP, _currentHP;

    private void Start()
    {
        _currentHP = _maxHP;
    }

    public void GetDamage(float dmg)
    {
        _currentHP -= dmg;

        if (_currentHP <= 0)
        { OnDead(); }
    }

    public void OnDead()
    {
        Debug.Log("DEAD");
    }
}
