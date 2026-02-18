using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using Triheroes.Code;
using UnityEngine;

public class s_yoku_block : action
{

    readonly term yoku = new term ( "yoku" );

    [Depend]
    s_skin ss;

    float LoopDuration;
    float time;
    Collider col;

    public class package : PreBlock.Package < s_yoku_block >
    {
        public package ( float LoopDuration, float offset, Collider col )
        {
            o.LoopDuration = LoopDuration;
            o.col = col;
            o.time = LoopDuration - offset;
        }
    }

    public override void Create()
    {
        Stage.Start (this);
    }

    protected override void Step()
    {
        if ( time > LoopDuration )
            StartYoku ();

        if ( time > 0 && col.enabled )
        StopYoku ();

        time += Time.deltaTime;
    }

    readonly term bling = new term ( "bling" );
    void StartYoku ()
    {
        a_sfx.Play ( bling, ss.Coord.position );
        time = -2f;
        ss.Play ( yoku );
        col.enabled = true;
    }

    void StopYoku ()
    {
        ss.Play ( AnimationKey.idle );
        col.enabled = false;
    }

}
