using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks")]
public class SOAttack : ScriptableObject
{
    [SerializeField] AnimatorOverrideController _overrideController;
    public AnimatorOverrideController OverrideController { get { return _overrideController; } }
    [SerializeField] float _timeToGetNextHit;
    public float TimeToGetNextHit { get { return _timeToGetNextHit; } }
}
