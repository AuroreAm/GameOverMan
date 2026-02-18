using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    public static Dictionary < int, Vector3 > CheckPoints = new Dictionary < int, Vector3 > ();

    void Awake()
    {
        CheckPoints.Add (gameObject.GetInstanceID (), transform.position);
    }

    void OnTriggerEnter ( Collider c )
    {
        if ( play.o.Player.c.gameObject.GetInstanceID () == c.gameObject.GetInstanceID () )
        {
            Klems.checkPoint = gameObject.GetInstanceID ();
            GetComponent < Animator > ().Play ( "check" );

            GetComponent <AudioSource> ().Play ();
        }
    }

}