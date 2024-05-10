using UnityEngine;

public class Controller
{
    float _horizontalAxis, _verticalAxis, _mouseHorizontal, _mouseVertical;
    Model _model;

    public Controller(Model m, View v)
    {
        m.AddToOnMovement(v.OnMovement);
        m.AddToOnAttackA(v.OnAttackA);
        m.AddToOnAttackB(v.OnAttackB);
        m.AddToOnBlock(v.OnBlock);
        _model = m;
    }

    public void OnUpdate()
    {
        _horizontalAxis = Input.GetAxis("Horizontal");
        _verticalAxis = Input.GetAxis("Vertical");
        _mouseHorizontal = Input.GetAxis("Mouse X");
        _mouseVertical = Input.GetAxis("Mouse Y");

        //if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
        //{
        //    if (Input.GetKey(KeyCode.Mouse0) && Input.GetKey(KeyCode.Mouse1))
        //    { _model.AttackC(); return; }
        //}

        if (Input.GetKeyDown(KeyCode.Mouse0))
        { _model.Attack(); return; }
        //if (Input.GetKeyDown(KeyCode.Mouse1))
        //{ _model.AttackB(); return; }
        //if (Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Mouse1))
        //{ _model.SetAttacksFalse(); }
        if ( Input.GetKeyDown(KeyCode.LeftControl))
        { _model.ShieldOn(true); }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        { _model.ShieldOn(false); }
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
