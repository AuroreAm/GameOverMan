using System;
using System.Collections.Generic;
using UnityEngine;
using Pixify;

[RequireComponent(typeof(Animator))]
public class SkinModel : MonoBehaviour
{

    [Header("Character dimension")]
    [Tooltip("height")]
    public float h;
    [Tooltip("radius")]
    public float r;
    [Tooltip("mass")]
    public float m;

    public void RequiredPix(in List<Type> a)
    {
        var writers = GetComponents<Writer>();
        foreach (var writer in writers)
            writer.RequiredPix(in a);
    }

    public void OnWriteBlock()
    {
        var writers = GetComponents<Writer>();
        foreach (var a in writers)
            a.OnWriteBlock();
    }

    public void AfterSpawn(Vector3 position, Quaternion rotation, block b)
    {
        var writers = GetComponents<Writer>();
        foreach (var a in writers)
            a.AfterSpawn(position, rotation, b);
    }

    public void AfterWrite(block b)
    {
        var writers = GetComponents<Writer>();
        foreach (var a in writers)
        {
            a.AfterWrite(b);
            Destroy(a);
        }
    }

#if UNITY_EDITOR
    public void OnDrawGizmosSelected()
    {
        Transform t = transform;
        // draw the character dimension capsule
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(t.position + r / 2 * Vector3.up, r);
        Gizmos.DrawWireSphere(t.position + ((h - r / 2) * Vector3.up), r);
        Gizmos.DrawLine(t.position + r / 2 * Vector3.up + (r * Vector3.left), t.position + ((h - r / 2) * Vector3.up) + (r * Vector3.left));
        Gizmos.DrawLine(t.position + r / 2 * Vector3.up + (r * Vector3.right), t.position + ((h - r / 2) * Vector3.up) + (r * Vector3.right));
        Gizmos.DrawLine(t.position + r / 2 * Vector3.up + (r * Vector3.forward), t.position + ((h - r / 2) * Vector3.up) + (r * Vector3.forward));
        Gizmos.DrawLine(t.position + r / 2 * Vector3.up + (r * Vector3.back), t.position + ((h - r / 2) * Vector3.up) + (r * Vector3.back));
    }

#endif
}