using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;
    [SerializeField] Transform _player;
    [SerializeField] float _repelForce;

    private void Update()
    {
        transform.LookAt(new Vector3(_player.position.x, transform.position.y, _player.position.z));
    }

    public void GetRepeled()
    {
        _rb.AddForce(-transform.forward * _repelForce, ForceMode.Impulse);
    }
}
