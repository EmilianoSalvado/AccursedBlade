using System.Collections;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] Transform[] _points;
    [SerializeField] Transform _root;
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _speed;
    Vector3 _auxVector;
    bool _platformOn = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!_platformOn) return;

        if (other.TryGetComponent<Model>(out var model))
        {
            model.transform.SetParent(transform);
            StopAllCoroutines();
            StartCoroutine(Move(model));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!_platformOn) return;

        if (other.TryGetComponent<Model>(out var model))
        {
            model.transform.SetParent(_root);
        }
    }

    private void FixedUpdate()
    {
        if (_auxVector.sqrMagnitude <= 0f) return;

        _rb.MovePosition(transform.position + _auxVector * (_speed * Time.fixedDeltaTime));
    }

    public void PlatformOn(bool onOff)
    {
        _platformOn = onOff;
    }

    IEnumerator Move(Model model)
    {
        yield return new WaitForSeconds(.6f);

        foreach (var p in _points)
        {
            while (Vector3.Distance(transform.position, p.position) > .2f)
            {
                _auxVector = (p.position - transform.position).normalized;
                yield return null;
            }

            _auxVector = Vector3.zero;
            _rb.MovePosition(p.position);
        }
        _points = _points.Reverse().ToArray();
    }
}
