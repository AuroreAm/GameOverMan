using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverCount : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke ("Restart", 1);
    }

    void Restart ()
    {
        SceneManager.LoadScene ( 2 );
    }
}
