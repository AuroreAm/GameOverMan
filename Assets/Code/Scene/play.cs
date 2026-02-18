using UnityEngine;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;

public class play : bios
{
    public static play o;
    public d_actor Player {  get; private set; }

    public play () { o = this; }

    public void SetPlayer ( d_actor player )
    {
        Player = player;
    }
}

[Category ("bios")]
public class g_bios_use_play : action
{
    [Depend]
    m_game_bios mgb;
    [Depend]
    play play;
    

    protected override void Start ()
    {
        mgb.Set (play);
        SelfStop ();
    }
}
