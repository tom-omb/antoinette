using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void startGame()
    {
        playerAnt.horizontalMovement = 0f;
        AutoScrollEvents.EnableInput = true;
        AutoScrollEvents.disableJump = false;

        CutsceneManager.currentScene = 0;
        SceneManager.LoadScene("cutscenes");
        Time.timeScale = 1f;
    }
}