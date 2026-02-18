using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

public class GunSkinWriter : Writer
{
    public Vector3 GunPosition;

    public override void OnWriteBlock()
    {
        new d_gun.package(GunPosition);
    }

    public override void RequiredPix(in List<Type> a)
    {
        a.A<d_gun>();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(GunPosition + transform.position, 0.05f);
    }
}