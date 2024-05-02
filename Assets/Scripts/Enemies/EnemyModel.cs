using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyModel : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;
    [SerializeField] Transform _player;
    [SerializeField] float _repelForce;

    [SerializeField] NavMeshAgent _naveMeshAgent;
    [SerializeField] Transform _target;
    [SerializeField] float _visionRadius, _visionAngle, _attackCooldown;
    [SerializeField] LayerMask _obstaclesMask;
    WaitForSeconds _attackCD;
    Action _checkForPlayer;

    [SerializeField] EnemyWeapon _weapon;

    private void Start()
    {
        _attackCD = new WaitForSeconds(_attackCooldown);

        _checkForPlayer = () =>
        {
            if (Tools.InRadius(transform.position, _target.position, _visionRadius) && 
            Tools.InSight(transform.position, _target.position, _obstaclesMask) &&
            Tools.InAngle(transform, _target.position, _visionAngle))
            {
                StopAllCoroutines();
                StartCoroutine(AttackRoutine());
                return;
            }

            StopAllCoroutines();
            StartCoroutine(SeekRoutine());
        };

        _checkForPlayer();
    }

    public void GetRepeled()
    {
        _rb.AddForce(-transform.forward * _repelForce, ForceMode.Impulse);
    }

    IEnumerator SeekRoutine()
    {
        while (true)
        {
            _naveMeshAgent.SetDestination(_target.position);
            yield return new WaitForSeconds(.2f);
            _checkForPlayer();
        }
    }

    IEnumerator AttackRoutine()
    {
        _naveMeshAgent.SetDestination(transform.position);

        while (true)
        {
            _weapon.Attack();
            yield return _attackCD;
            _checkForPlayer();
        }
    }
}
