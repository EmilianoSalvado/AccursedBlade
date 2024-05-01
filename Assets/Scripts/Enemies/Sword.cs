using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : EnemyWeapon
{
    [SerializeField] Animator _animator;
    [SerializeField] string _animParameter;
    public override void Attack()
    {
        _animator.SetTrigger(_animParameter);
    }
}
