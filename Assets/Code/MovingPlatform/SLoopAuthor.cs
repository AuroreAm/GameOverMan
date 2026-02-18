using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

public class SLoopAuthor : Writer
{
    public Vector3 StartPositionOffset;
    public Vector3 PointBOffset;
    public float speed = 3;
    public int direction;

    public override void OnWriteBlock()
    {
        new s_loop.package ( direction, speed, transform.position, transform.position +PointBOffset );
    }

    public override void AfterSpawn(Vector3 position, Quaternion rotation, block b)
    {
        b.GetPix <character> ().gameObject.transform.position += StartPositionOffset;
    }

    public override void RequiredPix(in List<Type> a)
    {
        a.A <s_loop> ();
    }

    void OnDrawGizmos ()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + PointBOffset, 0.05f);
        Gizmos.DrawWireSphere(transform.position + StartPositionOffset, 0.05f);
    }
}
