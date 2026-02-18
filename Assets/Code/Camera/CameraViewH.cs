using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewH : CameraView
{
    public float width;

    protected override float w => width;
    protected override float h => sh;

    camera_shot shot;

    void Start()
    {
        shot = new cs_scroll ( transform.position, width );
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + new Vector3(width, sh, 0) / 2, new Vector3(width, sh, .32f));
    }

    protected override void OnPlayerEnter()
    {
        s_camera.o.TransitionShot (shot);
    }
}