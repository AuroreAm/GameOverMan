using System;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

public class HitBoxList : PixIndex <hit_box>
{

    public static HitBoxList o;

    public HitBoxList ()
    {
        o = this;
    }

    public static bool IsPlayer ( int id )
    {
        return o.ptr [id].da.IsPlayer;
    }

    public static void Hit ( int id, float dmg )
    {
        o.ptr [id].Hit (dmg);
    }

}

public class hit_box : indexed_pix <hit_box>
{
    [Depend]
    s_skin ss;

    [Depend]
    public d_actor da;

    public override void Create()
    {
        Register ( ss.Ani.gameObject.GetInstanceID () );
    }

    public void Hit (float dmg)
    {
        _OnHit (dmg);
    }

    event Action <float> _OnHit = delegate { };
    public event Action <float> OnHit
    {
        add { _OnHit += value; }
        remove { _OnHit -= value; }
    }
}