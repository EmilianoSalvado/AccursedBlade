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
            if (Tools.InRadius(transform.position, _player.position, _visionRadius) &&
            Tools.InSight(transform.position, _player.position, _obstaclesMask) &&
            Tools.InAngle(transform, _player.position, _visionAngle))
            {
                StopAllCoroutines();
                StartCoroutine(AttackRoutine());
                transform.LookAt(new Vector3(_player.position.x, transform.position.y, _player.position.z));
                return;
            }

            StopAllCoroutines();
            StartCoroutine(SeekRoutine());
        };

        _checkForPlayer();
    }

    public void SetPlayer(Transform playerTransform)
    {
        _player = playerTransform;
    }

    public void OnDead()
    {
        EnemyManager.Instance.GetKill(this);
        StopAllCoroutines();
        GetComponent<Collider>().enabled = false;
        this.enabled = false;
        Destroy(gameObject, 1f);
    }

    IEnumerator SeekRoutine()
    {
        while (true)
        {
            yield return new WaitUntil(() => enabled);
            if (enabled)
                _naveMeshAgent.SetDestination(_player.position);
            yield return new WaitForSeconds(.2f);
            _checkForPlayer();
        }
    }

    IEnumerator AttackRoutine()
    {
        yield return new WaitUntil(() => enabled);
        if (enabled)
            _naveMeshAgent.SetDestination(transform.position);

        while (true)
        {
            yield return new WaitUntil(() => enabled);
            _weapon.Attack();
            yield return _attackCD;
            _checkForPlayer();
        }
    }
}