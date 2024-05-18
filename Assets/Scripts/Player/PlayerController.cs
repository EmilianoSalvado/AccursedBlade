using UnityEngine;

public class PlayerController
{
    float _horizontalAxis, _verticalAxis, _mouseHorizontal, _mouseVertical;
    PlayerModel _model;

    public PlayerController(PlayerModel m, View v)
    {
        m.AddToOnMovement(v.OnMovement);
        m.AddToOnAttack(v.OnAttack);
        m.AddToOnSheathOrUnsheath(v.OnSheathOrUnsheath);
        m.AddToOnCurse(v.OnCurse);
        m.AddToOnBlock(v.OnBlock);
        _model = m;
    }

    public void OnUpdate()
    {
        _horizontalAxis = Input.GetAxis("Horizontal");
        _verticalAxis = Input.GetAxis("Vertical");
        _mouseHorizontal = Input.GetAxis("Mouse X");
        _mouseVertical = Input.GetAxis("Mouse Y");


        if (Input.GetKeyDown(KeyCode.Mouse0))
        { _model.Attack(); return; }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        { _model.Sheath(); return; }
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
