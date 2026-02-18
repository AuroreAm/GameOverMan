using System.Collections;
using System.Collections.Generic;
using Pixify.Spirit;
using Pixify;
using System;
using UnityEngine;
using static UnityEngine.Random;

public class enm1 : Writer
{


    public CharacterAuthor Bullet;

    public override void OnWriteBlock()
    {
        new enm1_move.package ( Bullet );
    }

    public override void RequiredPix(in List<Type> a)
    {
        a.A <enm1_move> ();
    }
}

[Category ("enm")]
public class enm1_cortex : cortex
{
    
    [Depend]
    enm_standard_hit_receiver eshr;
    
    public override void Setup()
    {
        AddReflexion <enm1_move> ();
    }
}

public class enm1_move : reflexion, IMotorHandler
{
    [Depend]
    ac_ground ag;
    [Depend]
    s_motor sm;
    [Depend]
    d_dimension dd;
    [Depend]
    d_gun dg;
    [Depend]
    ss_2d s2d;

    int state = 0;
    CharacterAuthor Bullet;

    public class package : PreBlock.Package <enm1_move>
    {
        public package ( CharacterAuthor Bullet ) { o.Bullet = Bullet; }
    }

    public void OnMotorEnd(motor m)
    {}

    protected override void Reflex()
    {
        if ( enm.PlayerInRange ( dd ) )
            Stage.Start ( this );
    }

    protected override void Start()
    {
        DistanceToShoot = Range ( 1f, 2.5f );
        state = 0;
    }

    float DistanceToShoot;
    float speed = 2;
    float state_shoot_time = 0;

    protected override void Step()
    {
        switch (state)
        {
            case 0:
                if ( sm.SetState (ag, this) )
                state ++;
            break;

            case 1:
                ag.Run ( new Vector3 ( speed , 0 ) * Mathf.Sign ( play.o.Player.dd.position.x - dd.position.x ) );

                if ( Mathf.Abs ( play.o.Player.dd.position.x - dd.position.x ) < DistanceToShoot )
                state ++;
            break;

            case 2:
                state_shoot_time = Range ( .5f, 2 );
                state ++;
            break;

            case 3:

                if ( state_shoot_time <= 0 )
                {
                    var b = Bullet.Spawn ( dg.GunPostion, Quaternion.identity ).GetPix <s_bullet_1d> ();
                    b.Fire ( s2d.direction );

                    state_shoot_time = Range ( .5f, 2 );
                        
                    if ( Mathf.Abs ( play.o.Player.dd.position.x - dd.position.x ) > DistanceToShoot )
                    state = 1;
                }
                
                s2d.SetDirection ( Mathf.Sign ( play.o.Player.dd.position.x - dd.position.x ) );

                state_shoot_time -= Time.deltaTime;
            break;
        }
    }
}