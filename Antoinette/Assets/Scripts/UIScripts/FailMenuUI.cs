using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FailMenuUI : MonoBehaviour
{
    public GameObject FailMenuCanv;
    public GameObject pauseButtonUI;
   


    // Update is called once per frame
    void Update()
    {
        
    }
    public void PauseGame(){
//pop death menu - freeze time - change audio 
        FailMenuCanv.SetActive(true);
        pauseButtonUI.SetActive(false);
        Time.timeScale = 0f;
    }

    public void MainMenuPressed(){
        SceneManager.LoadScene("MainMenu");
    }

    public void RetryPressed(){
        FailMenuCanv.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void ExitGamePressed(){
        Application.Quit();
    }

}
