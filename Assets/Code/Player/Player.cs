using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

public class Player : pix
{

    public override void Create()
    {
        VMove = new InputAction ("Vertical", true);
        HMove= new InputAction ("Horizontal", true);
        Jump= new InputAction ("Action0");
        Dash= new InputAction ("L2",true);
        Shoot= new InputAction ("Action2");
    }

    public static Vector2 MoveAxis2 => new Vector3(HMove.Raw, VMove.Raw);
    public static InputAction VMove, HMove, Dash, Shoot, Jump;
}


public sealed class InputAction : bios
{
    /// <summary>
    /// is the corresponding button held down
    /// </summary>
    public bool Active { get; private set; }
    /// <summary>
    /// is the corresponding button pressed down this frame
    /// </summary>
    public bool OnActive => _OnDown;
    /// <summary>
    /// is the corresponding button released this frame
    /// </summary>
    public bool OnRelease => _OnUp;
    /// <summary>
    /// raw value of the corresponding button
    /// </summary>
    public float Raw => _IsAxis ? Input.GetAxis ( _InputManagerAccessName ) : Input.GetButton ( _InputManagerAccessName )? 1f : 0f;

    bool _OnUp;
    bool _OnDown;
    bool _IsAxis;
    string _InputManagerAccessName;

    public InputAction ( string InputManagerAccessName, bool IsInputManagerAccessNameAxis = false )
    {
        _IsAxis = IsInputManagerAccessNameAxis;
        _InputManagerAccessName = InputManagerAccessName;
        Stage.Start (this);
    }

    protected override void Step()
    {
        _OnDown = false;
        _OnUp = false;

        if (Active == false)
        {
            if ( (_IsAxis && Mathf.Abs (Raw)>0.1f) || (!_IsAxis && Input.GetButton ( _InputManagerAccessName )) )
            {
                _OnDown = true;
                Active = true;
            }
        }
        else
        {
            if ( (_IsAxis && Mathf.Abs (Raw)<0.1f) || (!_IsAxis && !Input.GetButton ( _InputManagerAccessName )) )
            {
                _OnUp = true;
                Active = false;
            }
        }
    }

}