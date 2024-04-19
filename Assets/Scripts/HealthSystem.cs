using UnityEngine;

public abstract class HealthSystem : MonoBehaviour
{
    [SerializeField] protected float _maxHP, _currentHP;

    private void Start()
    {
        _currentHP = _maxHP;
    }

    public virtual void GetDamage(float dmg)
    {
        _currentHP -= dmg;

        if (_currentHP <= 0)
        { OnDead(); }
    }

    public virtual void OnDead()
    {
        Debug.Log("DEAD");
    }
}
