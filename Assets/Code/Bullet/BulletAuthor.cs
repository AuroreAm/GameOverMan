using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

[RequireComponent(typeof(SkinModel))]
public class BulletAuthor : Writer
{
    public float Radius;
    public float Speed;
    public string sfx;

    public float Damage = 1;

    public override void OnWriteBlock()
    {
        new s_bullet_1d.package ( Speed, Radius, Damage, new term ( sfx ) );
    }

    public override void RequiredPix(in List<Type> a)
    {
        a.A < s_bullet_1d > ();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);
        Gizmos.color = Color.green;
    }
}