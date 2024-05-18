using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesOnHit : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;
    Action<Collider> _playOnHit;

    private void Awake()
    {
        _playOnHit = (other) =>
        {
            if (!other.GetComponent<DamageDoer>())
                return;

            if (Physics.Raycast(transform.position + (other.transform.position - transform.position) * 2f,
                transform.position - other.transform.position,
                out var hitInfo))
            {

                _particleSystem.transform.position = hitInfo.point;
                _particleSystem.transform.forward = -hitInfo.normal;
                _particleSystem.Play();
            }

        };
    }

    private void OnTriggerEnter(Collider other)
    {
        _playOnHit(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _playOnHit(collision.collider);
    }
}