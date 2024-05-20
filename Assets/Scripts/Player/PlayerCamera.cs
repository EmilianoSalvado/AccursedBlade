using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] Transform _pivot, _cameraRelative;
    [SerializeField] float _offset, _blockedOffset, _defaultSensivity, _auxSensivity;
    float mouseX = 0, mouseY = 0;
    [SerializeField] LayerMask _everythingButPlayerLayer;
    RaycastHit _hit;
    Action _move;

    public Vector3 CameraRelativeForward
    {
        get
        { return _cameraRelative.forward; }
    }

    public Vector3 CameraRelativeRight
    {
        get
        { return _cameraRelative.right; }
    }

    private void Awake()
    {
        _move = () =>
        {
            transform.position = _pivot.position - _pivot.forward * CameraDistance();
            transform.LookAt(_pivot.position);
        };
    }

    public void UpdatePivot()
    {
        _pivot.position = _target.position + Vector3.up * 1f;
        _cameraRelative.position = _pivot.position;
        _cameraRelative.rotation = Quaternion.Euler(_target.rotation.eulerAngles.x, _pivot.rotation.eulerAngles.y, 0f); ;
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

        _move();
    }

    float CameraDistance()
    {
        if (Physics.SphereCast(_pivot.position - transform.forward * _offset, .3f, _pivot.position - transform.position, out _hit, _offset, _everythingButPlayerLayer))
        {
            return (_pivot.position - _hit.point).magnitude - _blockedOffset;
        }

        _auxSensivity = _defaultSensivity;

        return _offset;
    }

    public void CameraShakingPosition(bool isShaking, Vector3 pos, Vector3 initPos)
    {
        if (!isShaking)
        {
            _move = () =>
            {
                transform.position = _pivot.position - _pivot.forward * CameraDistance();
                transform.LookAt(_pivot.position);
            };
            return;
        }

        _move = () =>
        {
            transform.position = pos;
            transform.LookAt(pos + (_pivot.position - initPos));
        };
    }
}
