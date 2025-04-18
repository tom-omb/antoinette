using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenuUI : MonoBehaviour
{
    //public static bool playerIsDead = false; 
    public GameObject DeathMenuCanv;
    public GameObject pauseButtonUI;
   


    // Update is called once per frame
    void Update()
    {

    }
    public void PauseGame(){
//pop death menu - freeze time - change audio 
        DeathMenuCanv.SetActive(true);
        pauseButtonUI.SetActive(false);
        Time.timeScale = 0f;
       // PauseMenu.gameIsPaused =true;
    }

    public void MainMenuPressed(){
        SceneManager.LoadScene("MainMenu");
    }

    public void RetryPressed(){
        DeathMenuCanv.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void ExitGamePressed(){
        Application.Quit();
    }
}
