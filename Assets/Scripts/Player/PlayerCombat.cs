using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] List<SOAttack> _combo;
    [SerializeField] float _minTimeBetweenCombos;
    int _comboCount = 0;
    float _lastTimeTriggered, _lastComboEnd;

    public void Attack()
    {
        if (Time.time - _lastComboEnd < .5f)
        { return; }

        CancelInvoke("EndCombo");

        if (Time.time - _lastTimeTriggered < _combo[_comboCount].TimeToGetNextHit)
        {
            _animator.runtimeAnimatorController = _combo[_comboCount].OverrideController;
            _animator.Play("AN_Player_Attack", 2, 0);
            _lastTimeTriggered = Time.time;
            _comboCount = _combo.Count + 1 >= _combo.Count ? 0 : _comboCount + 1;
        }
    }

    public void ExitAttack()
    {
        if (_animator.GetCurrentAnimatorStateInfo(2).normalizedTime > .9f)
        {
            Invoke("EndCombo", .5f);
        }
    }

    public void EndCombo()
    {
        _comboCount = 0;
        _lastComboEnd = Time.time;
    }
}
