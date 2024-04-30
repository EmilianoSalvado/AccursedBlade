using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvents : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;

    public void PlayParticleSystem()
    {
        _particleSystem.Play();
    }

    public void StopParticleSystem()
    {
        _particleSystem.Stop();
    }
}
