using UnityEngine;
using System.Collections.Generic;
using Pixify.Spirit;
using Pixify;
using System.Drawing;

public class pr_move : reflexion, IMotorHandler
{

    [Depend]
    ac_ground ag;
    [Depend]
    s_motor sm;

    float speed = 3;


    public void OnMotorEnd(motor m)
    {}


    protected override void Reflex()
    {
        if (sm.state == null)
            sm.SetState (ag,this);

        float InputAxis = Player.HMove.Raw * speed;

        if (sm.state == ag)
        ag.Run ( new Vector3 (InputAxis ,0) );
        else if ( sm.state is ac_fall af )
        af.AirMove ( new Vector3 (InputAxis ,0) );
    }
}
