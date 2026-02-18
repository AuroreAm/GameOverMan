using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

public class s_bomb : action
{
    [Depend]
    ss_flash flash;

    [Depend]
    s_skin ss;

    const float duration = 1.25f;
    float time;
    float interval;

    CharacterAuthor Explosion;

    public override void Create()
    {
        Explosion = SubResources <CharacterAuthor>.q (new term ("exp1") );
        ss.Reverse = true;
        Stage.Start (this);
    }

    protected override void Start()
    {
        time = duration;
    }

    protected override void Step()
    {
        if ( time < 0 )
        {
            Explosion.Spawn ( ss.Coord.position, Quaternion.identity );
            b.Destroy ();
            return;
        }

        if (interval <= 0)
        {
            flash.Switch ();
            interval = time / 10;
        }

        interval -= Time.deltaTime;
        time -= Time.deltaTime;
    }
}

public class ss_flash : pix
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

    public void Switch ()
    {
        sprite.sprite.sharedMaterial = sprite.sprite.sharedMaterial == defaultMaterial ? flash : defaultMaterial;
    }

    public void Reset ()
    {
        sprite.sprite.sharedMaterial = defaultMaterial;
    }
}