using UnityEngine;

public class AnimEvents : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] StaminaSystem _staminaSystem;
    public void PlayParticleSystem()
    {
        _particleSystem.Play();
    }

    public void StopParticleSystem()
    {
        _particleSystem.Stop();
    }

    public void TakeStamina(float amount)
    {
        _staminaSystem.SpendStamina(amount);
    }
}
