using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

public class s_rigidbody : pix
{
    [Depend]
    s_skin ss;


    public Rigidbody rb {  get; private set; }

    public override void Create()
    {
        rb = ss.Ani.GetComponent <Rigidbody> ();
    }

}