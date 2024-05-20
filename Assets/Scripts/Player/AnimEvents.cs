using UnityEngine;

public class AnimEvents : MonoBehaviour
{
    [SerializeField] PlayerModel _model;

    public void Impulse()
    {
        _model.GetPlayerMovement.Impulse();
    }
}