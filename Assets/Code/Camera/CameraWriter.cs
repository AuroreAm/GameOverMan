using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

public class CameraWriter : Writer
{
    public override void OnWriteBlock()
    {
        new s_camera.package ( GetComponent <Camera> () );
    }

    public override void RequiredPix(in List<Type> a)
    {
        a.A <s_camera> ();
    }
}
