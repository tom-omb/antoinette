using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject pauseButtonUI;
    public GameObject pauseBoxUI;
    public GameObject InstructionsPanelUI;

    audioManager audioManager;
  

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audioManager>();
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused){
                ResumePlay();

            } else{
                PausePlay();
            }
        }
    }

    public void PausePlay(){
//pop pause menu - freeze time - change audio - change gameIsPaused var
        pauseMenuUI.SetActive(true);
        pauseButtonUI.SetActive(false);
        Time.timeScale = 0f;
        gameIsPaused= true;
        audioManager.stopsound();
        
    }
    public void ResumePlay(){
        pauseMenuUI.SetActive(false);
        pauseButtonUI.SetActive(true);
        Time.timeScale = 1f;
        audioManager.playsound();
       
        gameIsPaused= false;
    }

    public void MainMenuPressed(){
        SceneManager.LoadScene("MainMenu");
    }
    public void InstructionsPressed(){
        InstructionsPanelUI.SetActive(true);
        pauseBoxUI.SetActive(false);
    }
    public void BackPressed(){
        pauseBoxUI.SetActive(true);
        InstructionsPanelUI.SetActive(false);
    }

    public void ExitGamePressed(){
        Application.Quit();
    }
    
}
