using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossEvent : MonoBehaviour
{

    public GameObject Block;
    public GameObject Block1;
    public GameObject EndMessage;
    public AudioSource BGM;
    public AudioClip BossMusic;
    public AudioClip WinMusic;

    public Text BossHPText;

    bool BossStarted;
    bool BossDefeated;

    void OnTriggerEnter ()
    {
        if ( BossStarted ) return;
        
        BGM.Stop ();
        BGM.clip = BossMusic;
        BGM.Play ();


        enm5_move.o.StartBoss ();
        Block.SetActive ( true );
        Block1.SetActive ( false );
        BossStarted = true;

        BossHPText.transform.parent.gameObject.SetActive (true);
    }

    void LateUpdate ()
    {
        if ( !BossStarted ) return;

        BossHPText.text = string.Concat ( "Papango: ", enm5_move.HP, " / 60" );

        if ( enm5_move.HP <= 0 && !BossDefeated )
        {
            BGM.Stop ();
            BGM.loop = false;
            BGM.clip = WinMusic;
            BGM.Play ();
            BossDefeated = true;
            EndMessage.SetActive (true);
            Invoke ("End", 5);
        }
    }

    void End ()
    {
        Application.Quit ();
    }
}
