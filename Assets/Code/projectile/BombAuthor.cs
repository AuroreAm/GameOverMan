using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

public class BombAuthor : Writer
{
    public override void OnWriteBlock()
    {
    }

    public override void RequiredPix(in List<Type> a)
    {
        a.A <s_rigidbody> ();
        a.A <s_bomb> ();    
    }
}