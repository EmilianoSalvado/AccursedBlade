using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] Transform _pivot;
    [SerializeField] float _offset, _blockedOffset, _sensivity;
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
            transform.position += (transform.right * -x + transform.up * -y) * (_sensivity * Time.deltaTime);
            transform.LookAt(_pivot);
        }

        transform.position = _pivot.position - transform.forward * CameraDistance();
    }

    float CameraDistance()
    {
        if (Physics.Raycast(transform.position, _pivot.position - transform.position, out _hit, _offset, _everythingButPlayerLayer))
        {
            return (_pivot.position - _hit.point).magnitude - _blockedOffset;
        }

        if (Physics.Raycast(transform.position, transform.position - transform.forward, out _hit, _blockedOffset + .1f, _everythingButPlayerLayer))
        {
            return (_pivot.position - _hit.point).magnitude - _blockedOffset;
        }

        return _offset;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, _pivot.position - transform.position);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, (transform.position - transform.forward * (_blockedOffset + .2f)));
    }
}
