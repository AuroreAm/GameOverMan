using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

public class ExplosionAuthor : Writer
{
    public float time = 1;
    public string sfx;

    public override void OnWriteBlock()
    {
        new s_explosion.package ( time, new term ( sfx ) );
    }

    public override void RequiredPix(in List<Type> a)
    {
        a.A <s_explosion> ();
    }
}
