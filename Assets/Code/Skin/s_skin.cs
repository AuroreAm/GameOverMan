using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

public class s_skin : pixi
{
    [Depend]
    character c;
    public Transform Coord {private set; get;}
    public Animator Ani { private set; get; }

    public class package : PreBlock.Package <s_skin>
    {
        public package ( GameObject skinGameObject )
        {
            o.Ani = skinGameObject.GetComponent<Animator> ();
        }
    }

    public void Play ( term Animation )
    {
        Ani.Play ( Animation );
    }

    public override void Create()
    {
        Coord = c.gameObject.transform;

        // disable unity fire events
        Ani.fireEvents = false;

        Ani.transform.position = Coord.position;
        Stage.Start (this);
    }

    public bool Reverse;

    protected override void Step()
    {
        if (!Reverse)
         Ani.transform.position = Coord.position;
        else
            Coord.position = Ani.transform.position;
    }

    public override void Destroy1()
    {
        Object.Destroy (Ani.gameObject);
    }
}

