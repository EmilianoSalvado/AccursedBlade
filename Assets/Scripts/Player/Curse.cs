using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curse : MonoBehaviour, IObserver
{
    [SerializeField] HealthSystem _healthSystem;
    [SerializeField] float _damage, _timeBetweenTake;
    bool _curseOn;

    public void Cursed(bool on)
    {
        if (_curseOn == on)
            return;
        _curseOn = on;
        StartCoroutine(CurseRoutine());
    }

    public void Notify(params object[] parameters)
    {
        _healthSystem.Heal((float)parameters[0]);
    }

    IEnumerator CurseRoutine()
    {
        var t = new WaitForSeconds(_timeBetweenTake);

        while (_curseOn)
        {
            _healthSystem.Damage(_damage);
            yield return t;
        }
    }
}