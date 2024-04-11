using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] string _runningParameterTag;

    public void OnMovement(float magnitude)
    {
        _animator.SetFloat(_runningParameterTag, magnitude);
    }
}