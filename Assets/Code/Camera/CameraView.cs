using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraView : MonoBehaviour
{
    protected abstract float w {get;}
    protected abstract float h {get;}
    protected static CameraView CurrentView;

    public const float sw = 6.4f;
    public const float sh = 4.8f;
    public static readonly float z = DistanceForPixelPerfect ();

    const float ppu = 100;
    const float fovDeg = 30;
    
    public static float DistanceForPixelPerfect()
    {
        float fovRad = fovDeg * Mathf.Deg2Rad;
        return 480f / (2f * ppu * Mathf.Tan(0.5f * fovRad)) * -1;
    }

    public void Awake()
    {
        gameObject.layer = Vecteur.TRIGGER;
        BoxCollider collider = gameObject.AddComponent<BoxCollider>();
        collider.size = new Vector3(w, h, .32f);
        collider.center = new Vector3(w,h,0) / 2;
        collider.isTrigger = true;
    }

    public void OnTriggerStay(Collider c)
    {
        if ( CurrentView == null &&c.id () == play.o.Player.c.gameObject.GetInstanceID ()  )
        {
            CurrentView = this;
            OnPlayerEnter ();
        }
    }

    void OnTriggerExit(Collider c)
    {
        if ( c.id () == play.o.Player.c.gameObject.GetInstanceID () && CurrentView == this )
        {
            CurrentView = null;
        }
    }

    protected virtual void OnPlayerEnter ()
    {}
}

public class cs_scroll : camera_shot
{
    float minx;
    float maxx;
    float miny;
    float maxy;

    public cs_scroll ( Vector2 position, float width )
    {
        miny = position.y + CameraView.sh / 2;
        maxy = position.y + CameraView.sh / 2;
        minx = position.x + CameraView.sw / 2;
        maxx = position.x + width - CameraView.sw/2;
    }

    public cs_scroll ( float height, Vector2 position )
    {
        miny = position.y + CameraView.sh / 2;
        maxy = position.y + height - CameraView.sh / 2;
        minx = position.x + CameraView.sw/2;
        maxx = position.x + CameraView.sw/2;
    }

    protected override void Start()
    {
        CamPos.x = Mathf.Clamp(play.o.Player.dd.position.x, minx, maxx);
        CamPos.y = Mathf.Clamp(play.o.Player.dd.position.y + .32f, miny, maxy);
        CamPos.z = CameraView.z;
    }

    protected override void Step()
    {
        CamPos.x = Mathf.Clamp(play.o.Player.dd.position.x, minx, maxx);
        CamPos.y = Mathf.Clamp(play.o.Player.dd.position.y + .32f, miny, maxy);
        CamPos.z = CameraView.z;
    }
}