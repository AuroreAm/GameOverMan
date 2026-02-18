using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;


[CreateAssetMenu(fileName = "Trail", menuName = "RPG/Trail")]
public class TrailAuthor : VirtusAuthor
{
    protected override void RequiredPix(in List<Type> a)
    {
        a.A<a_sprite_frame>();
    }

    public override void OnWriteBlock()
    {
        new a_sprite_frame.package ( new GameObject().AddComponent<SpriteRenderer> () );
    }
}
