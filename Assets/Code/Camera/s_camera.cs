using System.Collections;
using System.Collections.Generic;
using Pixify;
using Triheroes.Code;
using UnityEngine;

public class s_camera : pixi
{
    public static s_camera o;
    Transform Coord;
    Camera cam;
    public static Vector3 position => o.Shot.CamPos;

    [Depend]
    camera_dummy dummy;

    [Depend]
    transition transitionShot;

    public void PlayGameOver ()
    {
        cam.GetComponent <Animator> ().Play ( "gameover" );
        a_sfx.Play ( new term ("gameover"), position );
        GameObject.Find ( "BGM" ).GetComponent <AudioSource> ().Stop ();
    }

    public override void Create()
    {
        o = this;
        Stage.Start (this);
        SetCameraShot (dummy);
        
        Coord.position = play.o.Player.dd.position;
    }

    public bool Transitionning => (! ( Shot is cs_scroll ) ) && transitionBuffer.on;
    input_buffer transitionBuffer = new input_buffer ( 1.5f );

    protected override void Start()
    {
        Coord.position = play.o.Player.dd.position;
    }

    protected override void Step()
    {
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            // Letterbox (add black bars top/bottom)
            Rect rect = cam.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            cam.rect = rect;
        }
        else
        {
            // Pillarbox (add black bars left/right)
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = cam.rect;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
            cam.rect = rect;
        }

        Coord.position = Shot.CamPos;
    }

    public class package : PreBlock.Package <s_camera>
    {
        public package ( Camera camera )
        {
            o.cam = camera;
            o.Coord = camera.transform;
        }
    }

        public float targetAspect = 4f / 3f;  // 4:3


    int key_cs;
    camera_shot Shot;
    void SetCameraShot(camera_shot Shot)
    {
        if (this.Shot == Shot)
        return;

        if (this.Shot != null)
            Stage.Stop ( key_cs );

        this.Shot = Shot;
        key_cs = Stage.Start (Shot);
    }

    public void TransitionShot ( camera_shot _shot )
    {
        transitionShot.Set ( Shot, _shot );
        SetCameraShot ( transitionShot );
        transitionBuffer.Buffer ();
    }

    public class transition : camera_shot
    {
        Vector3 INPos;

        camera_shot IN;
        camera_shot OUT;

        public void Set ( camera_shot _in, camera_shot _out )
        {
            IN = _in;
            OUT = _out;
/*
            if (on)
            {
                INPos = IN.CamPos;
                t = 0;
            }*/
        }

        int key_cs;
        protected override void Start()
        {
            t = 0;
            INPos = IN.CamPos;
            key_cs = Stage.Start ( OUT );
        }

        float t;
        protected override void Step()
        {
            t = Mathf.Lerp ( t, 1, .1f);
            CamPos = Vector3.Lerp (INPos, OUT.CamPos, t);

            if (t >= .99f)
            {
                Stage.Stop ( key_cs );
                o.SetCameraShot ( OUT );
            }
        }
    }

}

[PixiBase]
public abstract class camera_shot : pixi
{
    public Vector3 CamPos;
}

public class camera_dummy : camera_shot
{
    protected override void Start()
    {
        CamPos = play.o.Player.dd.position;
    }
    protected override void Step()
    {
        CamPos = play.o.Player.dd.position;
    }
}