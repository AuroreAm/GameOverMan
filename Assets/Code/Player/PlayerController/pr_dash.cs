using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

public class pr_dash : reflexion, IMotorHandler
{
    [Depend]
    d_ground_data dgd;

    [Depend]
    ac_dash ad;

    [Depend]
    s_motor sm;

    [Depend]
    ss_2d s2d;

    public void OnMotorEnd(motor m)
    {
    }

    protected override void Reflex()
    {
        if ( dgd.onGround && ( Player.Dash.OnActive || Input.GetKeyDown (KeyCode.Mouse1) ) && !ad.on )
        sm.SetState ( ad, this );

        if ( ad.on && Player.HMove.Raw != 0 )
            s2d.SetDirection ( Mathf.Sign (Player.HMove.Raw) );
    }
}

public sealed class ac_dash_jump : ac_jump
{}