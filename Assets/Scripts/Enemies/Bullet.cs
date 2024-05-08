using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _speed, _duration;

    private void Start()
    {
        StartCoroutine(SelfDestruction());
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(transform.position + transform.forward * (_speed * Time.fixedDeltaTime));
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }

    IEnumerator SelfDestruction()
    {
        yield return new WaitForSeconds(_duration);
        Destroy(gameObject);
    }
}
