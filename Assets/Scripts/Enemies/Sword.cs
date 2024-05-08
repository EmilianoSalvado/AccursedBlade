using UnityEngine;
using System.Collections;

public class Sword : EnemyWeapon
{
    [SerializeField] Animator _animator;
    [SerializeField] string _animParameter;
    [SerializeField] Collider _collider;
    [SerializeField] float _colliderTime;
    public override void Attack()
    {
        _animator.SetTrigger(_animParameter);
        StartCoroutine(ColliderEnabling());
    }

    IEnumerator ColliderEnabling()
    {
        _collider.enabled = true;
        yield return new WaitForSeconds(_colliderTime);
        _collider.enabled = false;
    }
}