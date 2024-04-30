using System.Collections;
using System.Linq;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] Transform[] _points;
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _speed;
    Vector3 _auxVector;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Model>(out var model))
        {
            model.transform.SetParent(transform);
            StopAllCoroutines();
            StartCoroutine(Move(model));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Model>(out var model))
        {
            model.transform.SetParent(null);
        }
    }

    private void FixedUpdate()
    {
        if (_auxVector.sqrMagnitude <= 0f) return;

        _rb.MovePosition(transform.position + _auxVector * (_speed * Time.fixedDeltaTime));
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
