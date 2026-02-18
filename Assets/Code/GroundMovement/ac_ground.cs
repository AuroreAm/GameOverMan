using System.Collections;
using System.Collections.Generic;
using Pixify.Spirit;
using Pixify;
using UnityEngine;

public class ac_ground : motor
{
    public override int Priority => Pri.def2nd;
    public override bool AcceptSecondState => true;

    
    [Depend]
    s_capsule_character_controller2d sccc;

    [Depend]
    s_gravity_ccc sgc;

    [Depend]
    d_ground_data dgd;

    [Depend]
    ss_2d s2d;

    [Depend]
    s_skin ss;
    
    public term state { private set; get; }
    Vector3 runDir;
    
    int CurrentFrame;

    public override void Create()
    {
        Link (sccc);
        Link (sgc);
    }

    protected override void Start()
    {
        if ( CurrentFrame != Time.frameCount )
        {
            runDir = Vector3.zero;
            ToIdle ();
        }
        else
        {
            if ( state ==  idle )
                ToIdle ();
                else
                ToRun ();
        }
    }

    protected override void Step()
    {
        Animation ();
        Direction ();
        ResetDir ();
    }

    void ResetDir () => runDir = Vector3.zero;

    void Animation ()
    {
        // idle => run
        if ( state == idle && runDir.magnitude > 0.01f )
        {
            ToRun ();
            Animation ();
            return;
        }
        else if ( state == run && runDir.magnitude < 0.01f )
        {
            ToIdle ();
            Animation ();
            return;
        }
    }
    
    void Direction ()
    {
        if (runDir.magnitude > 1)
            s2d.SetDirection ( Mathf.Sign ( runDir.x ) );
    }

    void ToIdle ()
    {
        state = idle;
        ss.Play (AnimationKey.idle);
    }

    void ToRun ()
    {
        state = run;
        ss.Play (AnimationKey.run);
    }

    // public methos
    public void Run ( Vector3 DirPerSecond )
    {
        if ( on )
        {
            runDir += DirPerSecond;
            sccc.dir += Time.deltaTime * SlopeProjection (DirPerSecond, dgd.groundNormal);
        }
    }

    public static Vector3 SlopeProjection ( Vector3 Dir,Vector3 GroundNormal ) => Vector3.ProjectOnPlane (Dir, GroundNormal).normalized * Dir.magnitude;
    // states
    public static term idle = new term ("idle");
    public static term run = new term ("run");
}