using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{

    public r_gameover gameover;

    void OnCollisionEnter(Collision other)
    {
        if ( gameover != null && other.gameObject.layer == Vecteur.HITBOX )
        {
            gameover.OnHit (0);
        }
    }

}