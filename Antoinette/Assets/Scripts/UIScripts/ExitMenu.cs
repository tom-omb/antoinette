using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMenu : MonoBehaviour
{
    public GameObject ConfirmQuitUI;

    public void ExitGamePressed()
    {
        ConfirmQuitUI.SetActive(true);
    }

    public void QuitTrue()
    {
        Verify(true);
    }

    public void QuitFalse()
    {
        Verify(false);
    }

    public void Verify(bool quit)
    {
        if (quit)
        {
            Debug.Log("GAME QUIT");
            Application.Quit();
        }
        else
        {
            ConfirmQuitUI.SetActive(false);
        }
    }
}
