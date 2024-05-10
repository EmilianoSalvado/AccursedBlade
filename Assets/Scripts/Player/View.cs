using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] Animator _animator;
    public Animator GetAnimator {  get { return _animator; } } 
    [SerializeField] string _runningParameterTag;

    [SerializeField] ParticleSystem _bladeAbsorvingParticle;
    [SerializeField] ParticleSystem _bladeAttackParticle;
    [SerializeField] ParticleSystem _bladeCurseParticle;

    public void OnMovement(float magnitude)
    {
        _animator.SetFloat(_runningParameterTag, magnitude);
    }

    public void OnAttack(bool b)
    {
        if (b)
        {
            _bladeAttackParticle.Play();
            return;
        }

        _bladeAttackParticle.Stop();
    }

    public void OnSheathOrUnsheath(bool b)
    {
        if (b)
        {
            _bladeCurseParticle.Play();
            return;
        }

        _bladeCurseParticle.Stop();
    }

    public void OnCurse(bool b)
    {
        if (b)
        {
            _bladeAbsorvingParticle.Play();
            return;
        }

        _bladeAbsorvingParticle.Stop();
    }

    public void OnBlock(bool b)
    {
        //_shieldRenderer.enabled = b;
    }
}