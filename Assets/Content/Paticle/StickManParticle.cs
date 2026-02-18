using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickManParticle : MonoBehaviour
{
    void Start ()
    {
        List <Rigidbody> rigidbodies = new List<Rigidbody> ();
        foreach (Transform child in transform) {
            if (child.GetComponent<Rigidbody> ())
            rigidbodies.Add ( child.GetComponent<Rigidbody> ());
        }
        foreach (Rigidbody rigidbody in rigidbodies) {
            rigidbody.AddForce (Random.insideUnitCircle * Random.Range (1,10));
        }

        Invoke ("End", 5);
    }

    void End ()
    {
        Destroy (gameObject);
    }

}
