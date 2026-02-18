using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify.Spirit;
using Pixify;

[Category ("enm")]
public class enm0 : cortex
{
    [Depend]
    enm_standard_hit_receiver eshr;
    public override void Setup()
    {
        AddReflexion <enm0_move> ();
    }
}


public class enm0_move : reflexion
{
    [Depend]
    s_capsule_character_controller2d ccc;

    [Depend]
    s_gravity_ccc sgc;

    [Depend]
    d_dimension dd;

    float speed = -1f;

    protected override void Reflex()
    {
        if ( enm.PlayerInRange (dd) )
        Stage.Start (this);
    }

    public override void Create()
    {
        Link (ccc);
        Link (sgc);
    }

    protected override void Step()
    {
        ccc.dir += new Vector3 ( speed * Time.deltaTime, 0, 0 );
    }

}