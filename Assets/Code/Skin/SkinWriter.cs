using System;
using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

public class SkinWriter : Writer
{
    public SkinModel Model;
    SkinModel model;

    public override void OnWriteBlock()
    {
        new d_dimension.package(model.h, model.r, model.m);
        new s_skin.package(model.gameObject);

        model.OnWriteBlock ();
    }

    public override void RequiredPix(in List<Type> a)
    {
        // Instantiate the model
        model = Instantiate(Model).GetComponent<SkinModel>();

        a.A<s_skin>();
        a.A<d_dimension>();
        
        model.RequiredPix ( in a );
    }

    public override void AfterSpawn(Vector3 position, Quaternion rotation, block b)
    {
        model.AfterSpawn(position, rotation, b);
    }

    public override void AfterWrite(block b)
    {
        model.AfterWrite (b);
        
        Destroy(model);
    }
}
