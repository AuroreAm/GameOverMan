using System.Collections;
using System.Collections.Generic;
using Pixify.Spirit;
using Pixify;
using UnityEngine;

public class ac_fall : motor
{
    public override int Priority => Pri.Action;
    public override bool AcceptSecondState => true;

    [Depend]
    s_capsule_character_controller2d sccc;
    [Depend]
    protected s_gravity_ccc sgc;

    [Depend]
    public d_ground_data dgd;

    [Depend]
    s_skin ss;
    [Depend]
    ss_2d s2d;

    public override void Create()
    {
        Link (sccc);
        Link (sgc);
    }

    protected override void Start()
    {
        ss.Play ( AnimationKey.fall );
    }

    protected override void Step()
    {
        if (dgd.onGround && sgc.gravity < 0 && Vector3.Angle(Vector3.up, dgd.groundNormal) <= 45)
            SelfStop ();
    }

    public void StopFall ()
    {
        if (on)
        SelfStop ();
    }


    public void AirMove(Vector3 DirPerSecond)
    {
        if (on)
        {
            sccc.dir += Time.deltaTime * DirPerSecond;

            if ( DirPerSecond.magnitude > 0 )
            s2d.SetDirection(Mathf.Sign(DirPerSecond.x));
        }
    }

}
