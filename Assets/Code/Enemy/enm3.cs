using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

public class enm3 : Writer
{
    public float AnimationDuration;
    public float ShootTime;
    public CharacterAuthor Bullet0;

    public override void OnWriteBlock()
    {
        new enm3_move.package ( ShootTime, AnimationDuration, Bullet0 );
    }

    public override void RequiredPix(in List<Type> a)
    {
        a.A <enm3_move> ();
    }
}

[Category ("enm")]
public class enm3_cortex : cortex
{
    public override void Setup()
    {
        AddReflexion <enm3_move> ();
    }
}


public class enm3_move : reflexion
{
    [Depend]
    d_dimension dd;

    [Depend]
    s_skin ss;

    [Depend]
    ss_2d s2d;
    
    [Depend]
    enm enm;

    [Depend]
    hit_box hb;

    [Depend]
    s_flash sf;

    int state;

    float ShootTime;
    float AnimationDuration;
    CharacterAuthor Bullet0;

    public class package : PreBlock.Package <enm3_move>
    {
        public package ( float ShootTime, float AnimationDuration, CharacterAuthor Bullet0 ) { o.ShootTime = ShootTime; o.AnimationDuration = AnimationDuration; o.Bullet0 = Bullet0; }
    }

    float time;
    static readonly term shoot = new term ("shoot");

    protected override void Reflex()
    {
        if ( enm.PlayerInRange (dd) )
        Stage.Start (this);
    }

    public override void Create()
    {
        hb.OnHit += OnHit;
        Stage.Start (this);
    }

    protected override void Start()
    {
        state = 0;
        time = 0;
    }

    protected override void Step()
    {
        switch (state)
        {
            case 0:
                ss.Play (AnimationKey.idle);
                time = 1;
                state ++;
            break;

            case 1:
                if (time <= 0)
                {
                    ss.Play (shoot);
                    time = ShootTime;
                    state ++;
                    break;
                }
                time -= Time.deltaTime;
            break;

            case 2:
                if (time <= 0)
                {
                    time += AnimationDuration - ShootTime;
                    state++;
                    Shoot ();

                    break;
                }
                time -= Time.deltaTime;
            break;

            case 3:

                if (time <= 0)
                {
                    state = 1;
                    time = 1;
                    ss.Play (AnimationKey.idle);
                    break;
                }
                time -= Time.deltaTime;
            break;
        }
    }

    void Shoot ()
    {
        s_bullet_1d bullet = Bullet0.Spawn ( dd.position, Quaternion.identity ).GetPix <s_bullet_1d> ();
        bullet.Fire ( s2d.direction );
    }

    void OnHit ( float dmg )
    {
        if (state == 2)
        {
            sf.Flash ();
            enm.ReduceHP (dmg);
        }
    }

}
