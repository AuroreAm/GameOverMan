using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

public class d_gun : pix
{
    [Depend]
    ss_2d s2d;

    [Depend]
    s_skin ss;

    Vector3 GunPosition;

    public class package : PreBlock.Package < d_gun >
    {
        public package (  Vector3 GunPosition ) { o.GunPosition = GunPosition; }
    }

    public Vector3 GunPostion => new Vector3 ( Mathf.Abs ( GunPosition.x ) * s2d.direction + ss.Coord.position.x, ss.Coord.position.y + GunPosition.y, 0 );
}