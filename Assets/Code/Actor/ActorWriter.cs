using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

public class ActorWriter : Writer
{
    public bool IsPlayer;

    public override void OnWriteBlock()
    {
        new d_actor.package(IsPlayer);
    }

    public override void RequiredPix(in List<Type> a)
    {
        a.A <d_actor> ();
    }
}