using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

public class a_sprite_frame : virtus.pixi
{
    SpriteRenderer spriteRenderer;

    public class package : PreBlock.Package < a_sprite_frame >
    {
        public package ( SpriteRenderer spriteRenderer )
        {
            o.spriteRenderer = spriteRenderer;
        }
    }

    static readonly term trail = new term ( "trail" );

    const float CoolDown = .05f;
    static float LastFire;
    static Sprite _sprite;
    static Vector3 _position;
    public static void Fire ( Sprite sprite, Vector3 position )
    {
        if ( Time.time - LastFire < CoolDown ) return;

        _sprite = sprite;
        _position = position;
        VirtualPoolMaster.RentVirtus ( trail );
        LastFire = Time.time;
    }

    protected override void Start()
    {
        spriteRenderer.sprite = _sprite;
        spriteRenderer.transform.position = _position;
        spriteRenderer.color = Color.white;
        t = .25f;
        
        spriteRenderer.gameObject.SetActive (true);
    }

    float t;

    protected override void Step()
    {
        if ( t <= 0 )
        {
        v.Return_ ();
        return;
        }

        var a = t * 4;
        a = Mathf.Floor(a * 10f) / 10f;

        spriteRenderer.color = Color.Lerp (  Color.clear, Color.white , a );

        t -= Time.deltaTime;
    }

    protected override void Stop()
    {
        spriteRenderer.gameObject.SetActive (false);
    }
}
