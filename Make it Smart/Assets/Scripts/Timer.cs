using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static float timeRemaining = 600;
    bool timerIsRunning = false;
    public TextMeshProUGUI timeText;
    public GameObject ResultPanel;

    void Start()
    {
        timeText = gameObject.GetComponent<TextMeshProUGUI>();
        timerIsRunning = true;
    }
    void Update()
    {
        if (!PauseMenu.isGamePaused())
        {
            if (timerIsRunning)
            {
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                    DisplayTime(timeRemaining);
                }
                else
                {
                    Debug.Log("Time has run out!");
                    timeRemaining = 0;
                    Time.timeScale = 0f;
                    ResultPanel.SetActive(true);
                    timerIsRunning = false;
                }
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
