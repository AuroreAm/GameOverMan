using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

[RequireComponent (typeof (ActorWriter))]
[RequireComponent (typeof (SkinWriter))]
public class PlayerWriter : Writer
{
    public CharacterAuthor Bullet0;
    public CharacterAuthor Bullet1;
    public CharacterAuthor Bullet2;
    public GameObject DestroyedEffect;

    public override void OnWriteBlock()
    {
        new r_gameover.package ( DestroyedEffect );
        new pr_shoot.package ( Bullet0, Bullet1, Bullet2 );
    }

    public override void AfterWrite(block b)
    {
        b.GetPix <s_mind> ().SetCortex ( new Player_cortex () );
    }

    public override void RequiredPix(in List<Type> a)
    {
        a.A <s_mind> ();
        a.A <pr_shoot> ();
        a.A < hit_box > ();
        
        a.A <r_gameover> ();
    }
}