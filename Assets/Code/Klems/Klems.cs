using System;
using UnityEngine;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using Triheroes.Code;

public class Klems : Stage
{
    public static int checkPoint;

    public Script MapStartScript;
    action EventStart;

    public override void SetDirectorPix(in List<Type> a)
    {
        a.A <Player> ();
        a.A <VirtualPoolMaster> ();
        a.A <Vecteur> ();
        a.A <MovingPlatform> ();
        a.A <HitBoxList> ();
        a.A <SFXMaster> ();
    }

    protected override Type[] PixiExecutionOrder()
    {
        return new Type []
        {
            typeof (bios),

            typeof ( s_ground_data_ccc ),

            typeof ( action ),
            typeof ( spirit ),
            typeof ( reaction ),

            typeof (s_skin),

            typeof ( camera_shot ),
            typeof (s_camera),

            typeof ( s_gravity_ccc ),
            typeof ( s_capsule_character_controller2d ),

            typeof ( a_sprite_frame ),
            typeof ( a_sfx )
        };
    }

    public override void StageStart()
    {
        Act.Start ( EventStart );
    }

    public GameObject DefaultCheckPoint;
    protected override void SetDirector()
    {
        EventStart = MapStartScript.WriteTree ( Director );
        
        if (checkPoint == 0)
            checkPoint = DefaultCheckPoint.GetInstanceID ();

        SubResources <CharacterAuthor> .q (new term ("player")).Spawn ( CheckPoint.CheckPoints [checkPoint], Quaternion.identity );
    }
}