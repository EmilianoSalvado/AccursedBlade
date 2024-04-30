using UnityEngine;
using UnityEngine.Rendering;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] Transform _pivot;
    [SerializeField] float _offset, _blockedOffset, _defaultSensivity, _auxSensivity;
    float mouseX = 0, mouseY = 0;
    [SerializeField] LayerMask _everythingButPlayerLayer;
    RaycastHit _hit;

    public Vector3 CameraRelativeForward
    {
        get
        { return _pivot.forward; }
    }

    public Vector3 CameraRelativeRight
    {
        get
        { return _pivot.right; }
    }

    public void UpdatePivot()
    {
        _pivot.position = _target.transform.position + Vector3.up * 1f;
    }

    public void CameraMovement(float x, float y)
    {
        if (x != 0f || y != 0f)
        {
            mouseX += x * (_auxSensivity * Time.deltaTime);
            mouseY -= y * (_auxSensivity * Time.deltaTime);

            mouseY = Mathf.Clamp(mouseY, -20f, 80f);

            _pivot.rotation = Quaternion.Euler(mouseY, mouseX, 0f);
        }

        transform.position = _pivot.position - _pivot.forward * CameraDistance();
        transform.LookAt(_pivot.position);
    }

    float CameraDistance()
    {
        if (Physics.Raycast(_pivot.position - transform.forward * _offset, _pivot.position - transform.position, out _hit, _offset, _everythingButPlayerLayer))
        {
            return (_pivot.position - _hit.point).magnitude - _blockedOffset;
        }

        _auxSensivity = _defaultSensivity;

        return _offset;
    }
}
