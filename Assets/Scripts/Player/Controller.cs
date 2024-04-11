using UnityEngine;

public class Controller
{
    float _horizontalAxis, _verticalAxis, _mouseHorizontal, _mouseVertical;
    Model _model;

    public Controller(Model m, View v)
    {
        m.AddToOnMovement(v.OnMovement);
        _model = m;
    }

    public void OnUpdate()
    {
        _horizontalAxis = Input.GetAxis("Horizontal");
        _verticalAxis = Input.GetAxis("Vertical");
        _mouseHorizontal = Input.GetAxis("Mouse X");
        _mouseVertical = Input.GetAxis("Mouse Y");
    }

    public void OnFixedUpdate()
    {
        _model.Movement(_horizontalAxis, _verticalAxis);
    }

    public void OnLateUpdate()
    {
        _model.CameraAim(_mouseHorizontal, _mouseVertical);
    }
}
