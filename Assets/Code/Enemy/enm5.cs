using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

public class enm5 : Writer
{
    public Transform PointA;
    public Transform PointB;
    public override void OnWriteBlock()
    {
        new enm5_move.package ( PointA.position, PointB.position );
    }

    public override void RequiredPix(in List<System.Type> a)
    {
        a.A <enm5_move>();
    }
}

[Category("enm")]
public class enm5_cortex : cortex
{
    public override void Setup()
    {
        AddReflexion < enm5_move > ();
    }
}

public class enm5_move : reflexion, IMotorHandler
{
    [Depend]
    d_dimension dd;

    [Depend]
    enm_standard_hit_receiver eshr;

    [Depend]
    enm enm;

    [Depend]
    ss_2d s2d;

    [Depend]
    ac_ground ag;
    [Depend]
    s_motor sm;
    
    [Depend]
    ac_jump aj;

    [Depend]
    d_gun dg;

    [Depend]
    d_ground_data dgd;

    int state;

    Vector3 PlayerPos => play.o.Player.dd.position;

    public class package : PreBlock.Package < enm5_move >
    {
        public package ( Vector3 pointA, Vector3 pointB ) 
        {
            o.PointA = pointA;
            o.PointB = pointB;
        }
    }

    public void OnMotorEnd(motor m)
    {}

    public static enm5_move o;
    public static float HP => o.enm.HP;
    public void StartBoss ()
    {
        Stage.Start (this);
    }

    protected override void Start()
    {
        state = 0;
        ad.time = .75f;
    }

    public override void Create()
    {
        o = this;
        KiteBullet = SubResources <CharacterAuthor>.q (new term ("kite_bullet"));
        aj.Set( 1, 1 );
    }

    const float Interval = .5f;
    float interval;
    CharacterAuthor KiteBullet;


    protected override void Step()
    {

        phase1 ();
        phase2 ();
    }

    void phase1()
    {
        switch (state)
        {
            case 0:
            if ( sm.SetState (ag, this) )
            {
                state = 1;
                interval = Interval;
            }
            break;


            case 1:
            if (interval <= 0)
                state = Random.value > .5f ? 2 : 3;

            if ( enm.HP < 25 && state < 5 )
            state = 5;

            interval -= Time.deltaTime;
            s2d.SetDirection ( Mathf.Sign (  PlayerPos.x - dd.position.x ) );
            break;

            case 2:
                ShootKite ();
                interval = Interval;
                state = 1;
            break;

            case 3:
                if (  dgd.onGround )
                {
                    if (sm.SetState ( aj, this ))
                        state = 4;
                }
                else
                state = 3;
            break;

            case 4:
                if (sm.state != aj)
                {
                    sm.SetState (ag, this);
                    state = 1;
                }
            break;
        }
    }

    void ShootKite ()
    {
        var bullet = KiteBullet.Spawn ( dg.GunPostion, Quaternion.identity ).GetPix <s_bullet_1d> ();
                bullet.Fire ( s2d.direction );
    }

    [Depend]
    ac_fly af;

    [Depend]
    ac_dashX adx;

    [Depend]
    ac_dash ad;

    [Depend]
    s_skin ss;

    readonly term get_kite = new term ("get_kite");
    Vector3 PointA, PointB;

    float time;

    void phase2 ()
    {
        switch ( state )
        {
            case 5:
            ss.Play ( get_kite );
            time = .5f;
            state = 6;
            break;
            
            case 6:
            if ( time <= 0 )
                {
                    state = 7;
                    time = 5;
                }
            time -= Time.deltaTime;
            break;

            case 7:
                if ( time <= 0 )
                {
                    state = 10;
                    time = .5f;
                    sm.SetState ( ag, this );
                    interval = Interval;
                    break;
                }

                af.TargetPos = Random.value > .5f ? PointA : PointB;
                if ( sm.SetState ( af, this ) )
                state = 8;
            break;

            case 8:
                if ( sm.state != af )
                {
                    adx.Direction = ( PlayerPos - dd.position ).normalized;
                   if ( sm.SetState ( adx, this ) )
                   state = 9;
                }
                
                time -= Time.deltaTime;
            break;

            case 9:
                if ( sm.state != adx )
                    state = 7;

                time -= Time.deltaTime;
            break;

            case 10:
                if (interval <= 0)
                state = Random.value > .5f ? 11 : 12;

                interval -= Time.deltaTime;
                s2d.SetDirection ( Mathf.Sign (  PlayerPos.x - dd.position.x ) );
            break;

            case 11:
                ShootKite ();
                interval = Interval;
                state = 10;
            break;

            case 12:
                s2d.SetDirection ( Mathf.Sign ( Random.Range ( -1, 1 ) ) );
                if ( sm.SetState (ad, this ))
                state = 13;
            break;

            case 13:
                if ( sm.state != ad )
                {
                    sm.SetState (ag, this);
                    interval = Interval;
                    state = 10;
                }
            break;
        }
    }
}