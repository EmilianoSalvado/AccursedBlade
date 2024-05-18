using System.Collections;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] Transform[] _points;
    [SerializeField] Transform _root;
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _speed;
    [SerializeField] Collider _triggerCollider;
    PlayerModel _pm;

    Vector3 _auxVector;
    bool _platformOn = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!_platformOn) return;

        if (other.TryGetComponent<PlayerModel>(out var pm))
        {
            pm.transform.SetParent(transform);
            pm.GetPlayerMovement.IsAttachedToParent(true);
            StopAllCoroutines();
            StartCoroutine(Move());
            _pm = pm;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!_platformOn) return;

        if (other.TryGetComponent<PlayerModel>(out var model))
        {
            model.transform.SetParent(_root);
        }
    }

    private void Update()
    {
        if (_auxVector.sqrMagnitude <= 0f) return;

        transform.position += _auxVector * (_speed * Time.deltaTime);
    }

    public void PlatformOn(bool onOff)
    {
        _platformOn = onOff;
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(.6f);

        var path = _points.SkipWhile((x) => (x.transform.position - transform.position).sqrMagnitude < 1f).ToArray();

        foreach (var point in path)
        {
            while (Vector3.Distance(transform.position, point.position) > .2f)
            {
                yield return new WaitUntil(() => enabled);
                _auxVector = (point.position - transform.position).normalized;
                yield return null;
            }
        }

        _pm.GetPlayerMovement.IsAttachedToParent(false);
        _pm.transform.SetParent(_root);
        _points = _points.Reverse().ToArray();
        _auxVector = Vector3.zero;
    }
}
