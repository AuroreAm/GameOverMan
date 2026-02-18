using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleCursor : MonoBehaviour, IPixiHandler
{

    int MenuCount = 2;
    public Image Cursor;

    int ptr = 0;
    float CursorCoolDown;

    void LateUpdate()
    {
        if ( CursorCoolDown > 0)
            CursorCoolDown -= Time.deltaTime;

        if (InputUp() && CursorCoolDown <= 0)
        {
            ptr--;
            CursorCoolDown = .25f;
        }
        if (InputDown() && CursorCoolDown <= 0)
        {
            ptr++;
            CursorCoolDown = .25f;
        }
        
        if (ptr < 0) ptr = MenuCount - 1;
        if (ptr > MenuCount - 1) ptr = 0;

        Cursor.rectTransform.anchoredPosition = new Vector2(0, - ptr * 16);

        if ( InputStart() )
        {
            Klems.checkPoint = 0;
            switch (ptr)
            {
                case 0:
                SceneManager.LoadScene ( 2 );
                break;

                case 1:
                SceneManager.LoadScene ( 3 );
                break;
            }
        }

    }

    bool InputUp()
    {
        return Input.GetKeyDown(KeyCode.UpArrow) ||
        Input.GetKeyDown(KeyCode.W) ||
        Input.GetKeyDown(KeyCode.Z) ||
        Input.GetAxis("Vertical") > 0;
    }

    bool InputDown()
    {
        return Input.GetKeyDown(KeyCode.DownArrow) ||
        Input.GetKeyDown(KeyCode.S) ||
        Input.GetAxis("Vertical") < 0;
    }

    bool InputStart ()
    {
        return Input.GetKeyDown(KeyCode.Return) ||
        Input.GetKeyDown(KeyCode.Space) ||
        Input.GetKeyDown(KeyCode.JoystickButton7);
    }

    public void OnPixiEnd(pixi p)
    {
    }
}