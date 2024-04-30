using System.Collections;
using System.Linq;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] Transform[] _points;
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _speed;
    Vector3 _auxVector;
    Transform _currentPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Model>(out var model))
        {
            model.transform.SetParent(transform);
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

        _rb.MovePosition(_auxVector);
    }
    IEnumerator Move(Model model)
    {
        foreach (var p in _points)
        {
            _currentPoint = p;
            _auxVector = transform.position + (_currentPoint.position - transform.position).normalized * (_speed * Time.fixedDeltaTime);

            while (Vector3.Distance(transform.position, p.position) > .1f)
            {
                yield return null;
            }

        }
        _points.Reverse().ToArray();
    }
}
