using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using Triheroes.Code;
using UnityEngine;

public class ac_jump : motor
{
    public override int Priority => Pri.Action;
    
    public override bool AcceptSecondState => true;

    [Depend]
    s_capsule_character_controller2d sccc;

    [Depend]
    s_skin ss;

    [Depend]
    ss_2d s2d;
    
    delta_curve cu;
    
    float jumpHeight;
    float minimumJumpHeight;

    public override void Create()
    {
        Link ( sccc );
        cu = new delta_curve ( SubResources <CurveRes>.q (new term ("jump" )).Curve );
    }

    public void Set (float jumpHeight, float minimumJumpHeight)
    {
        this.jumpHeight = jumpHeight;
        this.minimumJumpHeight = minimumJumpHeight;
    }

    protected override void Start()
    {
        cu.Start ( jumpHeight, .3f );
        ss.Play ( AnimationKey.jump );
        a_sfx.Play ( AnimationKey.jump, ss.Coord.position );
    }

    protected override void Step()
    {
        sccc.dir += new Vector3 ( 0, cu.TickDelta() , 0 );
        
        if ( cu.Done )
        SelfStop ();
    }

    public void ForceStop()
    {
        SelfStop ();
    }
    
    public void StopJump ()
    {
        if ( cu.currentValue >= minimumJumpHeight && cu.currentValue < (jumpHeight + minimumJumpHeight)/ 2 )
        {
            SelfStop ();
            return;
        }
    }

    public void AirMove( float DirPerSecond, bool CanChangeDirection = true )
    {
        if (on)
        {
            if ( CanChangeDirection && DirPerSecond != 0 )
            s2d.SetDirection ( Mathf.Sign(DirPerSecond) );

            sccc.dir.x += Mathf.Abs ( Time.deltaTime * DirPerSecond ) * s2d.direction;
        }
    }
}
