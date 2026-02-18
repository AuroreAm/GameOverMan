using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

public class ss_2d : pix
{

    [Depend]
    s_sprite sprite;

    public float direction {private set; get;} = 1;


    public void SetDirection ( float d )
    {
        direction = d;
        sprite.sprite.flipX = d == -1;
    }
}

public class s_sprite : pix
{
    [Depend]
    s_skin ss;

    public SpriteRenderer sprite;

    public override void Create()
    {
        sprite = ss.Ani.GetComponent <SpriteRenderer> ();
    }
}

public class s_flash : action
{
    [Depend]
    s_sprite sprite;

    Material defaultMaterial;
    Material flash;

    public override void Create()
    {
        sprite.Create ();
        defaultMaterial = sprite.sprite.sharedMaterial;
        flash = SubResources <Material>.q (new term ("flash") );
    }

    float duration;
    public void Flash ()
    {
        sprite.sprite.sharedMaterial = flash;
        duration = 0.1f;

        if (!on)
        Stage.Start (this);
    }

    protected override void Step()
    {
        if (duration <= 0)
        {
            sprite.sprite.sharedMaterial = defaultMaterial;
            SelfStop ();
        }

        duration -= Time.deltaTime;
    }
}