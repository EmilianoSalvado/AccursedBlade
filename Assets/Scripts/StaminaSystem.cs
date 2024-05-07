using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaSystem : MonoBehaviour
{
    [SerializeField] float _maxStamina, _currentStamina, _recoverySpeed, _recoveryDelay;

    private void Start()
    {
        _currentStamina = _maxStamina;
    }

    public void SpendStamina(float amount)
    {
        StopAllCoroutines();
        _currentStamina = amount;
        StartCoroutine(Recover());
    }

    IEnumerator Recover()
    {
        yield return new WaitForSeconds(_recoveryDelay);

        while (_currentStamina < _maxStamina)
        {
            _currentStamina += _recoverySpeed * Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}