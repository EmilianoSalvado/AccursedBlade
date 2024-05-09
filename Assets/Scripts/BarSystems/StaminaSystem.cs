using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaSystem : MonoBehaviour, IObservable
{
    [SerializeField] float _maxStamina, _currentStamina, _recoverySpeed, _recoveryDelay;
    [SerializeField] protected GameObject[] _observerGameObjects;
    protected List<IObserver> _observers = new List<IObserver>();
    public bool Available { get { return _currentStamina > 0; } }

    private void Start()
    {
        _currentStamina = _maxStamina;

        _observers.Clear();
        foreach (var observer in _observerGameObjects)
        { _observers.Add(observer.GetComponent<IObserver>()); }
        NotifyToSubscribers();
    }

    public void SpendStamina(float amount)
    {
        StopAllCoroutines();
        _currentStamina -= amount;
        StartCoroutine(Recover());

        NotifyToSubscribers();
    }

    IEnumerator Recover()
    {
        yield return new WaitForSeconds(_recoveryDelay);

        while (_currentStamina < _maxStamina)
        {
            _currentStamina += _recoverySpeed * Time.deltaTime;
            NotifyToSubscribers();
            yield return new WaitForSeconds(Time.deltaTime);
        }
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
            obs.Notify(UIBars.StaminaBar, _currentStamina / _maxStamina);
        }
    }

    public void Unsubscribe(IObserver obs)
    {
        if (_observers.Contains(obs))
            _observers.Remove(obs);
    }
}