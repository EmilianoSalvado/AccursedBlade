using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] Transform _pivot;
    [SerializeField] float _offset, _sensivity;
    float mouseX, mouseY;

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
        _pivot.position = _target.transform.position;
        _pivot.forward = transform.forward + (_pivot.transform.position - transform.position);
    }

    public void CameraMovement(float x, float y)
    {
        if (x != 0f || y != 0f)
        {
            transform.position += (transform.right * -x + transform.up * -y) * (_sensivity * Time.deltaTime);
            transform.LookAt(_target);
        }

        transform.position = _target.position - transform.forward * _offset;
    }
}
