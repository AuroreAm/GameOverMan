using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

public class MovingPlatform : PixIndex < s_moving_platform >
{
    public static MovingPlatform o;

    public MovingPlatform ()
    {
        o = this;
    }

    public Vector3 GetMovingDir ( int MovingPlatformIndex )
    {
        return o.ptr [ MovingPlatformIndex ].DirMoved;
    }
}

public class s_moving_platform : indexed_pix < s_moving_platform >
{
    [Depend]
    s_skin ss;

    public override void Create()
    {
        Register ( ss.Ani.gameObject.GetInstanceID () );
    }

    public Vector3 DirMoved;
}

public class s_loop : action
{
    [Depend]
    character c;
    [Depend]
    s_moving_platform smp;

    Vector3 PointA;
    Vector3 PointB;
    
    // 0 to point A
    // 1 to point B
    int direction;
    float speed;

    public class package : PreBlock.Package <s_loop>
    {
        public package ( int direction, float speed, Vector3 PointA, Vector3 PointB )
        {
            o.direction = direction;
            o.speed = speed;
            o.PointA = PointA;
            o.PointB = PointB;
        }
    }

    public override void Create()
    {
        Stage.Start (this);
    }

    protected override void Step()
    {
        var position = c.gameObject.transform.position;
        if (direction == 0)
        {
            c.gameObject.transform.position = Vector3.MoveTowards ( c.position, PointA, speed * Time.deltaTime);
            if (c.position == PointA) direction = 1;
        }
        else if (direction == 1)
        {
            c.gameObject.transform.position = Vector3.MoveTowards ( c.position, PointB, speed * Time.deltaTime);
            if (c.position == PointB) direction = 0;
        }
        smp.DirMoved = c.gameObject.transform.position - position;
    }

}