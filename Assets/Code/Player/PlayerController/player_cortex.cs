using Pixify;
using Pixify.Spirit;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[Category("player controller")]
public class Player_cortex : cortex
{
    public override void Setup()
    {
        AddReflexion <r_gameover> ();
        AddReflexion <pr_move> ();
        AddReflexion <r_fall> ();
        AddReflexion <pr_jump> ();
        AddReflexion <pr_dash> ();
        AddReflexion <pr_shoot> ();
    }
}

public class r_gameover : reflexion
{
    [Depend]
    hit_box HitBox;

    [Depend]
    d_dimension dd;

    [Depend]
    s_skin ss;

    gameover go;
    public GameObject DestroyedEffect;

    public class package : PreBlock.Package <r_gameover>
    {
        public package (  GameObject DestroyedEffect ) {
            o.DestroyedEffect = DestroyedEffect;
        }
    }

    public override void Create()
    {
        go = new gameover ();
        ss.Ani.GetComponent <PlayerCollider> ().gameover = this;
        HitBox.OnHit += OnHit;
    }

    protected override void Reflex()
    {
        if ( !s_camera.o.Transitionning && Vector2.Distance ( new Vector2 ( s_camera.position.x, s_camera.position.y ), new Vector2 ( dd.Coord.position.x, dd.Coord.position.y ) ) > 15  )
        OnHit ( 0 );
    }

    public void OnHit ( float dmg )
    {
        if ( !go.on )
        {
            GameObject.Instantiate ( DestroyedEffect, dd.Coord.position + dd.h / 2 * Vector3.up , Quaternion.identity );
            Stage.Start (go);
            b.Destroy ();
        }
    }
}

public class input_buffer : action
{
    float Duration;
    float time;

    public input_buffer ( float duration )
    {
        Duration = duration;
    }

    public void Buffer ()
    {
        if (!on)
        Stage.Start (this);
        else
        time = Duration;
    }

    protected override void Start()
    {
        time = Duration;
    }

    public bool CheckActive ()
    {
        if (on)
        {
            SelfStop ();
            return true;
        }
        return false;
    }

    protected override void Step()
    {
        if (time <= 0)
        SelfStop ();

        time -= Time.deltaTime;
    }
}


public class gameover : action
{
    float timeleft;
    protected override void Start()
    {
        s_camera.o.PlayGameOver ();
        timeleft = 1.2f;
    }

    protected override void Step()
    {
        if (timeleft <= 0)
        {
            SceneManager.LoadScene ( 1 );
        }

        timeleft -= Time.deltaTime;
    }
}