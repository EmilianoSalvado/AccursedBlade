using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] float _minNormalizedTime;
    bool _sheathed = true;
    float _animNormalizedTime { get { return _animator.GetCurrentAnimatorStateInfo(2).normalizedTime; } }
    public bool Sheathed { get { return _sheathed; } }
    int _currentStateHash;
    public void Attack()
    {
        if (_animNormalizedTime < _minNormalizedTime)
        {
            StopAllCoroutines();
            _animator.SetBool("Attack", false);
            return;
        }

        _animator.SetBool("Attack", true);
        StopAllCoroutines();
        StartCoroutine(WaitUntilAnimationHasPassed());
    }

    public void Sheath()
    {
        _sheathed = !_sheathed;
        _animator.SetBool("Sheathed", _sheathed);
    }

    IEnumerator WaitUntilAnimationHasPassed()
    {
        yield return new WaitUntil(() => _currentStateHash != _animator.GetCurrentAnimatorStateInfo(2).shortNameHash);
        yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(2).normalizedTime > _minNormalizedTime);
        _animator.SetBool("Attack", false);
        _currentStateHash = _animator.GetCurrentAnimatorStateInfo(2).shortNameHash;
    }
}