using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] float _minNormalizedTime;
    bool _sheathed = true, _available = true;
    float _animNormalizedTime { get { return _animator.GetCurrentAnimatorStateInfo(2).normalizedTime; } }
    public bool Sheathed { get { return _sheathed; } }

    public void Attack(PlayerMovement pm)
    {
        if (_animNormalizedTime < _minNormalizedTime || !_available)
        {
            _animator.SetBool("Attack", false);
            return;
        }

        StopAllCoroutines();
        StartCoroutine(WaitUntilAnimationHasPassed(pm));
        _animator.SetBool("Attack", true);
    }

    public void Sheath()
    {
        _sheathed = !_sheathed;
        _animator.SetBool("Sheathed", _sheathed);
    }

    IEnumerator WaitUntilAnimationHasPassed(PlayerMovement pm)
    {
        _available = false;
        yield return new WaitUntil(() => _animNormalizedTime > _minNormalizedTime);
        yield return new WaitUntil(() => enabled);
        _available = true;
        yield return new WaitUntil(() => _animNormalizedTime > .9f);
        yield return new WaitUntil(() => enabled);
        _animator.SetBool("Attack", false);
    }
}