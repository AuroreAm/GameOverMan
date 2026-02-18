using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

public class YokuAuthor : Writer
{
    public float LoopDuration;
    public float offset;

    public override void OnWriteBlock()
    {
        new s_yoku_block.package ( LoopDuration, offset, GetComponent <Collider> () );
    }

    public override void RequiredPix(in List<Type> a)
    {
        a.A <s_yoku_block> ();
    }
}
