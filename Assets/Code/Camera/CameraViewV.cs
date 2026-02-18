using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewV : CameraView
{
    public float height;

    protected override float w => sw;
    protected override float h => height;

    camera_shot shot;

    void Start()
    {
        shot = new cs_scroll ( height, transform.position );
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + new Vector3(sw, height, 0) / 2, new Vector3(sw, height, .32f));
    }

    protected override void OnPlayerEnter()
    {
        s_camera.o.TransitionShot (shot);
    }
}