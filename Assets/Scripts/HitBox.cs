using System.Collections;
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
        if (other.TryGetComponent<IDamageTaker>(out var damageTaker))
        {
            damageTaker.GetDamage(_dmg);
            StopAllCoroutines();
            StartCoroutine(ResetHitBox());
        }
    }

    IEnumerator ResetHitBox()
    {
        var c = GetComponent<Collider>();
        c.enabled = false;
        yield return new WaitForSeconds(1f);
        c.enabled = true;
    }
}