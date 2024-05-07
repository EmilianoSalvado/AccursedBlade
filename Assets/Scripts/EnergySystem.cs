using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    [SerializeField] float _maxEnergy, _currentEnergy;
    [SerializeField] int _maxBatteries, _currentBateries;

    private void Start()
    {
        _currentEnergy = _maxEnergy;
        _currentBateries = _maxBatteries;
    }

    public void SpendEnergy(float amount)
    {
        _currentEnergy -= amount;
        if (_currentEnergy < 0)
        {
            _currentBateries--;
            _currentEnergy = _maxEnergy;
        }
    }

    public void AddBatterie()
    {
        if (_currentBateries + 1 <= _maxBatteries)
            _currentEnergy++;
    }
}