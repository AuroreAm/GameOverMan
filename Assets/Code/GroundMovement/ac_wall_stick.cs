using Pixify;
using System.Collections.Generic;
using Pixify.Spirit;
using UnityEngine;

public class ac_wall_stick : motor
{
    [Depend]
    s_capsule_character_controller2d sccc;
    [Depend]
    s_skin ss;

    public override int Priority => Pri.Action;
    const float pause = .05f;
    float time;

    public override void Create()
    {
        Link ( sccc );
    }

    protected override void Start()
    {
        time = pause;
        ss.Play ( AnimationKey.wall_stick );
    }

    protected override void Step()
    {
        if ( time <= 0 )
        SelfStop ();

        time -= Time.deltaTime;
    }
}
