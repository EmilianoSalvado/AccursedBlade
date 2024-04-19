using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] float _dmg;

    public void SetDamage(float dgm)
    {
        _dmg = dgm;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<HealthSystem>(out var healthSystem))
        {
            healthSystem.GetDamage(_dmg);
        }
    }
}
