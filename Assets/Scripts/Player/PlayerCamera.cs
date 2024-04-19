using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] Transform _pivot;
    [SerializeField] float _offset, _blockedOffset, _defaultSensivity, _auxSensivity;
    float mouseX, mouseY;
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
        _pivot.forward = transform.forward + (_pivot.transform.position - transform.position);
    }

    public void CameraMovement(float x, float y)
    {
        if (x != 0f || y != 0f)
        {
            transform.position += (transform.right * -x + transform.up * -y) * (_auxSensivity * Time.deltaTime);
            transform.LookAt(_pivot);
        }

        transform.position = _pivot.position - transform.forward * CameraDistance();
    }

    float CameraDistance()
    {
        if (Physics.Raycast(_pivot.position - transform.forward * _offset, _pivot.position - transform.position, out _hit, _offset, _everythingButPlayerLayer))
        {
            _auxSensivity = Mathf.Lerp(_defaultSensivity * .2f, _defaultSensivity, (_pivot.position - transform.position).sqrMagnitude / (_offset * _offset));
            return (_pivot.position - _hit.point).magnitude - _blockedOffset;
        }

        _auxSensivity = _defaultSensivity;

        return _offset;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(_pivot.position - transform.forward * _offset, _pivot.position - transform.position);
        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position, (transform.position - transform.forward * (_blockedOffset + .2f)));
    }
}
