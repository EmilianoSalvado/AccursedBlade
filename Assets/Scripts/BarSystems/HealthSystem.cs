using System.Collections.Generic;
using UnityEngine;

public abstract class HealthSystem : MonoBehaviour, IObservable
{
    [SerializeField] protected float _maxHP, _currentHP;
    [SerializeField] protected GameObject[] _observerGameObjects;
    protected List<IObserver> _observers = new List<IObserver>();

    private void Start()
    {
        _currentHP = _maxHP;

        _observers.Clear();
        foreach (var observer in _observerGameObjects)
        { _observers.Add(observer.GetComponent<IObserver>()); }
        NotifyToSubscribers();
    }

    public virtual void GetDamage(float dmg)
    {
        if (_currentHP < 0f) return;

        _currentHP -= dmg;

        NotifyToSubscribers();

        if (_currentHP <= 0)
        { OnDead(); }
    }

    public virtual void OnDead()
    {
        Debug.Log("DEAD");
    }

    public void Subscribe(IObserver obs)
    {
        if (!_observers.Contains(obs))
            _observers.Add(obs);
    }

    public void NotifyToSubscribers()
    {
        foreach (var obs in _observers)
        {
            obs.Notify(UIBars.LifeBar, _currentHP / _maxHP);
        }
    }

    public void Unsubscribe(IObserver obs)
    {
        if (_observers.Contains(obs))
            _observers.Remove(obs);
    }
}
