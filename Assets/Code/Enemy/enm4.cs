using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;


[Category("enm")]
public class enm4 : cortex
{
    public override void Setup()
    {
        AddReflexion<enm4_move>();
    }
}


public class enm4_move : reflexion
{
    
    [Depend]
    enm_standard_hit_receiver eshr;

    [Depend]
    d_dimension dd;

    CharacterAuthor Bullet;

    const float Speed = .25f;

    protected override void Reflex()
    {
        if ( enm.PlayerInRange ( dd ) )
            Stage.Start ( this );
    }

    float interval;

    public override void Create()
    {
        Bullet = SubResources <CharacterAuthor>.q (new term ("bullet"));
    }

    protected override void Start()
    {
        interval = 0;
    }

    int i = 0;
    protected override void Step()
    {
        dd.Coord.position = Vector3.MoveTowards ( dd.Coord.position, play.o.Player.dd.Coord.position, Speed * Time.deltaTime );

        interval -= Time.deltaTime;

        if (interval <= 0)
        {
            if ( i==0 )
            Shoot ( Vector3.up );
            
            if ( i==1 )
            Shoot ( Vector3.down );
            
            if ( i==2 )
            Shoot ( Vector3.left );
            
            if ( i==2 )
            Shoot ( Vector3.right );
            interval = .25f;
            
            i++;
            if ( i == 4 )
                i = 0;
        }

    }

    void Shoot ( Vector3 Direction )
    {
        var b = Bullet.Spawn ( dd.position, Quaternion.identity ).GetPix <s_bullet_1d> ();
        b.Fire ( Direction );
    }

}