using Pixify;
using System.Collections.Generic;
using Pixify.Spirit;
using UnityEngine;
using Triheroes.Code;

public class ac_dash : motor
{
    public override int Priority => Pri.Action;

    [Depend]
    s_capsule_character_controller2d sccc;

    [Depend]
    s_skin ss;

    [Depend]
    ss_2d s2d;

    [Depend]
    s_sprite sss;

    delta_curve cu;

    public override void Create()
    {
        Link (sccc);
        cu = new delta_curve(SubResources<CurveRes>.q(new term("jump")).Curve);
    }

    public float time = .3f;

    protected override void Start()
    {
        cu.Start ( 1.5f, time );
        ss.Play ( AnimationKey.dash );
        a_sfx.Play ( AnimationKey.dash, ss.Coord.position );
    }

    public void ForceStop()
    {
        if (on)
        SelfStop ();
    }

    protected override void Step()
    {
        sccc.dir += new Vector3 ( cu.TickDelta () * s2d.direction, 0 );

        if (cu.Done)
        SelfStop ();

        a_sprite_frame.Fire ( sss.sprite.sprite, ss.Coord.position );
    }
}