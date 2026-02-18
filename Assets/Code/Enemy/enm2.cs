using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

[Category ("enm")]
public class enm2_cortex : cortex
{
    [Depend]
    enm_standard_hit_receiver eshr;

    public override void Setup()
    {
        AddReflexion <enm2_move> ();
    }
}

public class enm2_move : reflexion
{
    [Depend]
    d_dimension dd;

    [Depend]
    ss_2d s2d;

    float interval;

    CharacterAuthor bomb;

    public override void Create()
    {
        bomb = SubResources <CharacterAuthor>.q (new term ("bomb"));
    }

    protected override void Reflex()
    {
        if ( enm.PlayerInRange ( dd ) )
            Stage.Start ( this );
    }

    protected override void Step()
    {
        s2d.SetDirection ( Mathf.Sign (  play.o.Player.dd.position.x - dd.position.x ) );

        if (interval <= 0)
        {
            var b = bomb.Spawn ( dd.position, Quaternion.identity ).GetPix <s_rigidbody> ();
            b.rb.AddForce ( new Vector3 ( Random.Range (-5,5),5), ForceMode.Impulse);
            interval = Random.Range (.25f,1.5f);
        }

        interval -= Time.deltaTime;
    }
}