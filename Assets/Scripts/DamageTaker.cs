using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTaker : MonoBehaviour, IDamageTaker
{
    [SerializeField] HealthSystem _healthSystem;

    public void GetDamage(float dmg)
    {
        _healthSystem.GetDamage(dmg);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DamageDoer>())
        {
            StartCoroutine(ResetHitBox());
        }
    }
    IEnumerator ResetHitBox()
    {
        var c = GetComponent<Collider>();
        c.enabled = false;
        yield return new WaitForSeconds(1f);
        if (!enabled) yield return new WaitUntil(() => enabled);
        c.enabled = true;
    }
}
