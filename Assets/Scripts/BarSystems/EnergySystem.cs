using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySystem : MonoBehaviour, IObservable
{
    [SerializeField] float _maxEnergy, _currentEnergy, _refillDelay;
    [SerializeField] int _maxBatteries, _currentBateries;
    [SerializeField] protected GameObject[] _observerGameObjects;
    protected List<IObserver> _observers = new List<IObserver>();
    public bool Available { get { return _currentEnergy > 0; } }

    private void Start()
    {
        _currentEnergy = _maxEnergy;
        _currentBateries = _maxBatteries;

        _observers.Clear();
        foreach (var observer in _observerGameObjects)
        { _observers.Add(observer.GetComponent<IObserver>()); }
        NotifyToSubscribers();
    }

    public void SpendEnergy(float amount)
    {
        _currentEnergy -= amount;

        if (_currentEnergy <= 0 && _currentBateries > 0)
        { StartCoroutine(Refill()); }

        NotifyToSubscribers();
    }

    public void AddBatterie()
    {
        if (_currentBateries + 1 <= _maxBatteries)
            _currentEnergy++;
    }

    IEnumerator Refill()
    {
        yield return new WaitForSeconds(_refillDelay);
        _currentBateries--;
        _currentEnergy = _maxEnergy;
        NotifyToSubscribers();
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
            obs.Notify(UIBars.EnergyBar, _currentEnergy / _maxEnergy, _currentBateries);
        }
    }

    public void Unsubscribe(IObserver obs)
    {
        if (_observers.Contains(obs))
            _observers.Remove(obs);
    }
}