using System.Collections;
using System.Collections.Generic;
using Pixify.Spirit;
using Pixify;
using UnityEngine;

public class pr_jump : reflexion, IMotorHandler
{
    [Depend]
    d_dimension dd;

    [Depend]
    ss_2d s2d;

    [Depend]
    d_ground_data dgd;

    [Depend]
    ac_jump aj;

    [Depend]
    ac_dash ad;

    [Depend]
    ac_fall af;

    [Depend]
    ac_dash_jump adj;

    [Depend]
    s_motor sm;

    [Depend]
    ac_wall_stick aws;
    
    [Depend]
    s_sprite sss;

    float speed = 3;

    public override void Create()
    {
        aj.Set( .75f, .2f );
        adj.Set( 1f, 1f );
    }

    public void OnMotorEnd(motor m)
    { }

    protected override void Reflex()
    {
        JumpNormal();
        WallStick();
    }

    input_buffer JumpBuffer = new input_buffer(.2f);
    input_buffer GroundBuffer = new input_buffer(.2f);

    void JumpNormal()
    {
        if (Player.Jump.OnActive)
            JumpBuffer.Buffer();

        if ( dgd.onGround )
            GroundBuffer.Buffer();

        if ( GroundBuffer.CheckActive () && JumpBuffer.CheckActive() )
        {
            Jump();
            return;
        }

        if (sm.state == aj && Player.Jump.OnRelease)
            aj.StopJump();


        if (aj.on)
        {
            float InputAxis = Player.HMove.Raw * speed;
            aj.AirMove ( InputAxis, !WallJumpDirectionBuffer.on );
        }

        if (adj.on)
        {
            a_sprite_frame.Fire ( sss.sprite.sprite, dd.position );
            float InputAxis = Player.HMove.Raw * 5;
            adj.AirMove(InputAxis, !WallJumpDirectionBuffer.on);
        }
    }

    void Jump()
    {
        if (!ad.on)
        {
            if (af.on)
                af.StopFall();
            sm.SetState(aj, this);
        }
        else
        {
            ad.ForceStop();
            sm.SetState(adj, this);
        }
    }

    bool CanWallStick;
    input_buffer CanWallJumpBuffer = new input_buffer(.4f);

    input_buffer WallJumpBuffer = new input_buffer(.4f);
    input_buffer WallJumpDirectionBuffer = new input_buffer(.4f);

    void WallStick()
    {
        if (dgd.onGround)
            CanWallStick = true;

        if (!dgd.onGround && Player.Jump.OnActive)
            WallJumpBuffer.Buffer();

        if ( CanWallStick && !dgd.onGround && Physics.Raycast ( dd.position + new Vector3 ( 0 , dd.h / 2,0 ) , new Vector3( s2d.direction , 0), dd.r + .1f, Vecteur.Solid)  && Player.HMove.Raw != 0 )
        {
            if (ad.on)
                ad.ForceStop();

            if (aj.on)
                aj.ForceStop();

            if (adj.on)
                adj.ForceStop();

            if (af.on)
                af.StopFall();

            sm.SetState(aws, this);

            CanWallStick = false;
            CanWallJumpBuffer.Buffer();
        }

        if (!aws.on && CanWallJumpBuffer.CheckActive() && WallJumpBuffer.CheckActive())
        {
            s2d.SetDirection(s2d.direction * -1);
            Jump();
            WallJumpDirectionBuffer.Buffer ();
            CanWallStick = true;
        }
    }
}
