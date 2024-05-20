using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDoer : MonoBehaviour, IObservable
{
    [SerializeField] float _dmg;
    [SerializeField] GameObject[] _observerGameObjects;
    protected List<IObserver> _observers = new List<IObserver>();

    private void Start()
    {
        _observers.Clear();
        foreach (var observer in _observerGameObjects)
        { _observers.Add(observer.GetComponent<IObserver>()); }
    }

    public void NotifyToSubscribers()
    {
        foreach (var obs in _observers)
        {
            obs.Notify(_dmg);
        }
    }

    public void SetDamage(float dgm)
    {
        _dmg = dgm;
    }

    public void Subscribe(IObserver obs)
    {
        if (!_observers.Contains(obs))
            _observers.Add(obs);
    }

    public void Unsubscribe(IObserver obs)
    {
        if (_observers.Contains(obs))
            _observers.Remove(obs);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageTaker>(out var damageTaker))
        {
            damageTaker.GetDamage(_dmg);
            NotifyToSubscribers();
        }
    }
}