using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] string _runningParameterTag;

    [SerializeField] Animator _bladeAnimator;
    [SerializeField] string _attackAParameter;
    [SerializeField] string _attackBParameter;
    [SerializeField] MeshRenderer _shieldRenderer;

    public void OnMovement(float magnitude)
    {
        _animator.SetFloat(_runningParameterTag, magnitude);
    }

    public void OnAttackA(bool b)
    {
        _bladeAnimator.SetBool(_attackAParameter, b);
    }

    public void OnAttackB(bool b)
    {
        _bladeAnimator.SetBool(_attackBParameter, b);
    }

    public void OnBlock(bool b)
    {
        _shieldRenderer.enabled = b;
    }
}