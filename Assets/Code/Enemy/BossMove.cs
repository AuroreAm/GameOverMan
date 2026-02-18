using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using Triheroes.Code;
using UnityEngine;

public class ac_fly : motor
{
    public override int Priority => Pri.Action;

    [Depend]
    s_capsule_character_controller2d ccc;

    [Depend]
    ss_2d s2d;

    [Depend]
    s_skin ss;

    public Vector3 TargetPos;
    readonly term fly = new term ( "fly" );

    const float speed = 3;

    float timeOut;

    public override void Create()
    {
        Link (ccc);
    }

    protected override void Start()
    {
        timeOut = 10;
        ss.Play ( fly );
    }

    protected override void Step()
    {
        ccc.dir += Time.deltaTime * ( TargetPos - ccc.Coord.position ).normalized * speed;
        s2d.SetDirection ( Mathf.Sign ( ccc.dir.x ) );

        timeOut -= Time.deltaTime;
        if (timeOut < 0 || CloseEnough ())
            SelfStop ();
    }

    bool CloseEnough()
    {
        return Vector2.Distance ( ccc.Coord.position, TargetPos ) < speed * Time.deltaTime * 2;
    }
}

public class ac_dashX : motor
{
    public override int Priority => Pri.Action;

    [Depend]
    s_capsule_character_controller2d sccc;

    [Depend]
    s_skin ss;

    [Depend]
    ss_2d s2d;

    [Depend]
    d_ground_data dgd;

    [Depend]
    s_sprite sss;

    delta_curve cu;

    public Vector3 Direction;

    public override void Create()
    {
        Link (sccc);
        cu = new delta_curve(SubResources<CurveRes>.q(new term("jump")).Curve);
    }

    protected override void Start()
    {
        cu.Start ( 4.8f, 1 );
        ss.Play ( dashx );
        a_sfx.Play ( dashx, ss.Coord.position );
    }

    readonly term dashx = new term ( "dashx" );

    protected override void Step()
    {
        sccc.dir += cu.TickDelta () * Direction;
        s2d.SetDirection ( Mathf.Sign ( sccc.dir.x ) );

        if (cu.Done || dgd.onGround)
        SelfStop ();
        
        a_sprite_frame.Fire ( sss.sprite.sprite, ss.Coord.position );
    }
}