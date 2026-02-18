using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;
using Triheroes.Code;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public static bool Paused;
    public GameObject PauseText;

    void Update ()
    {
        if (InputPause () && !Paused)
        {
            Paused = true;
            PauseText.SetActive ( true );
            Time.timeScale = 0f;
            GetComponent <AudioSource> ().Play ();
        }
        else if (InputPause () && Paused)
        {
            Paused = false;
            PauseText.SetActive ( false );
            Time.timeScale = 1f;
            
            GetComponent <AudioSource> ().Play ();
        }

        if (Paused && Input.GetKeyDown (KeyCode.R))
        {
            Time.timeScale = 1f;
            Paused = false;
            PauseText.SetActive ( false );
            SceneManager.LoadScene (0);
            
            GetComponent <AudioSource> ().Play ();
        }
    }
    
    bool InputPause ()
    {
        return Input.GetKeyDown(KeyCode.Return) ||
        Input.GetKeyDown(KeyCode.JoystickButton7) ||
        Input.GetKeyDown(KeyCode.P);
    }
}
