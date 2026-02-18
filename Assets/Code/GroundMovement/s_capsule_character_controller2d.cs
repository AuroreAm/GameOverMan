using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

public class s_capsule_character_controller2d : pixi
{
    [Depend]
    character c;

    [Depend]
    d_dimension dd;

    [Depend]
    s_ground_data_ccc sgdc;

    int CurrentFrame;

    /// <summary>
    /// add vector to command where to move
    /// </summary>
    public Vector3 dir;

    public Transform Coord;
    public CharacterController CCA { get; private set; }

    public override void Create()
    {
        Link(sgdc);

        Coord = dd.Coord;

        CCA = c.gameObject.AddComponent<CharacterController>();
        CCA.skinWidth = .01f;

        UpdateCCADimension();
    }

    void UpdateCCADimension()
    {
        CCA.height = dd.h;
        CCA.radius = dd.r;
        CCA.center = new Vector3(0, dd.h / 2, 0);
    }

    protected override void Start()
    {

        // don't reset anything if this is started/stopped on the same frame or next frame
        if (Time.frameCount == CurrentFrame || Time.frameCount == CurrentFrame + 1)
            return;

        dir = Vector3.zero;
    }

    protected override void Stop()
    {
        CurrentFrame = Time.frameCount;
    }

    protected override void Step()
    {
        Physics.IgnoreLayerCollision(Coord.gameObject.layer, Vecteur.CHARACTER, true);
        Physics.IgnoreLayerCollision(Coord.gameObject.layer, Vecteur.ATTACK, true);
        CCA.Move(dir);
        // make sure the character stays in 2D space
        Coord.position = new Vector3(Coord.position.x, Coord.position.y, 0);
        Physics.IgnoreLayerCollision(Coord.gameObject.layer, Vecteur.CHARACTER, false);
        Physics.IgnoreLayerCollision(Coord.gameObject.layer, Vecteur.ATTACK, false);

        dir = Vector3.zero;
    }
}


public class s_gravity_ccc : pixi
{
    [Depend]
    d_ground_data dgd;

    [Depend]
    s_capsule_character_controller2d sccc;

    [Depend]
    d_dimension dd;

    public override void Create()
    {
        mass = dd.m;
    }

    protected override void Start()
    {
        gravity = -.2f;
    }

    public float mass;
    public float gravity;

    protected override void Step()
    {
        // add gravity force // limit falling velocity when it reach terminal velocity
        if (gravity > -1000)
            gravity += Physics.gravity.y * Time.deltaTime/*a*/ * mass;

        if (dgd.onGround && gravity < 0)
            gravity = -0.2f;

        Vector3 GravityForce = new Vector3(0, gravity * Time.deltaTime, 0);

        if ( Vector3.Angle(Vector3.up, dgd.groundNormal) > 45)
        {
            GravityForce = new Vector3(dgd.groundNormal.x, -dgd.groundNormal.y, dgd.groundNormal.z) * GravityForce.magnitude;
            dgd.groundNormal = Vector3.up;
        }

        sccc.dir += GravityForce;
    }
}