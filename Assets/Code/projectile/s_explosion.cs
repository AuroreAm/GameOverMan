using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using Triheroes.Code;
using UnityEngine;

public class s_explosion : action
{
    [Depend]
    d_dimension dd;

    float time;
    int sfx;

    public class package : PreBlock.Package < s_explosion >
    {
        public package ( float time, int sfx ) { o.time = time; o.sfx = sfx; }
    }

    public override void Create()
    {
        Stage.Start (this);
    }

    protected override void Start()
    {
        a_sfx.Play (sfx, dd.position);
    }

    protected override void Step()
    {
        if (time <= 0)
            b.Destroy ();

        time -= Time.deltaTime;
    }
}