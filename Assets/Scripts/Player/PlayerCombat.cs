using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] float _minTimeBetweenCombos, _minTimeBetweenTriggered;
    float _lastTimeTriggered, _lastComboEnd;
    bool _sheathed = true;
    public bool Sheathed { get { return _sheathed; } }

    public void Attack()
    {
        if (Time.time - _lastComboEnd < _minTimeBetweenCombos)
        { return; }

        StopAllCoroutines();

        if (Time.time - _lastTimeTriggered > _minTimeBetweenTriggered)
        {
            StartCoroutine(WaitUntilAnimationHasPassed(.8f));
            _lastTimeTriggered = Time.time;
            _animator.SetBool("Attack", true);
            return;
        }

        _lastTimeTriggered = Time.time;
        _animator.SetBool("Attack", false);
    }

    public void Sheath()
    {
        _sheathed = !_sheathed;
        _animator.SetBool("Sheathed", _sheathed);
    }

    IEnumerator WaitUntilAnimationHasPassed(float minNormilizedTime)
    {
        yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(2).normalizedTime > minNormilizedTime);
        _animator.SetBool("Attack", false);
    }
}