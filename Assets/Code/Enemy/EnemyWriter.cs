using System;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;
using Triheroes.Code;

[RequireComponent (typeof (CharacterAuthor))]
[RequireComponent (typeof (ActorWriter))]
[RequireComponent (typeof (SkinWriter))]
public class EnemyWriter : Writer
{
    public PixPaper <cortex> AI;
    public float HP = 1;
    public GameObject DestroyedEffect;

    public override void OnWriteBlock()
    {
        new enm.package ( HP, DestroyedEffect );
    }

    public override void AfterWrite(block b)
    {
        b.GetPix <s_mind> ().SetCortex ( AI.Write () );
    }

    public override void RequiredPix(in List<Type> a)
    {
        a.A <s_mind> ();
        a.A <enm> ();
        a.A <hit_box> ();
    }
}

public class enm : pix
{
    readonly term hit0 = new term ( "hit0" );
    readonly term hit1 = new term ( "hit1" );
    readonly term hit2 = new term ( "hit2" );

    [Depend]
    d_dimension dd;

    public float HP { get; private set; }
    GameObject DestroyedEffect;

    public class package : PreBlock.Package <enm>
    {
        public package ( float HP, GameObject DestroyedEffect ) { o.HP = HP;
            o.DestroyedEffect = DestroyedEffect;
        }
    }

    public void ReduceHP ( float dmg )
    {
        HP -= dmg;
        a_sfx.Play ( ChooseHitSfx (), dd.position );

        if ( HP <= 0 )
        {
            GameObject.Instantiate ( DestroyedEffect, dd.Coord.position + dd.h / 2 * Vector3.up , dd.Coord.rotation );
            b.Destroy ();
        }
    }

    term ChooseHitSfx ()
    {
        switch ( UnityEngine.Random.Range ( 0, 3 ) )
        {
            case 0: return hit0;
            case 1: return hit1;
            case 2: return hit2;
        }
        return hit0;
    }

    public static bool PlayerInRange ( d_dimension dd )
    {
        return Vector3.Distance (dd.position, play.o.Player.dd.position) < 3.2f;
    }
}

public class enm_standard_hit_receiver : pix
{
    [Depend]
    enm enm;

    [Depend]
    hit_box hb;

    [Depend]
    s_flash sf;

    public override void Create()
    {
        hb.OnHit += OnHit;
    }

    void OnHit ( float dmg )
    {
        sf.Flash ();
        enm.ReduceHP (dmg);
    }


}