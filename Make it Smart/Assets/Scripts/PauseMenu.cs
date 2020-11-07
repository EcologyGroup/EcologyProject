using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private static Boolean gameIsPaused;
    [SerializeField] GameObject pauseMenuUI, gameStatusUI, upgradePanelCanvas;


    private void Start()
    {
        gameIsPaused = false;
        pauseMenuUI.SetActive(gameIsPaused);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume(){
        gameIsPaused = false;
        pauseMenuUI.SetActive(false);
        gameStatusUI.SetActive(true);
        Time.timeScale = 1f;        
    }

    public void Pause(){
        gameIsPaused = true;
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        gameStatusUI.SetActive(false);
        upgradePanelCanvas.SetActive(false);
    }

    public void LoadMenu(){
        //GameObject.Find("Main Camera").GetComponent<CameraController>().enabled = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    public Boolean isGamePaused()
    {
        return gameIsPaused;
    }

    

}
