using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class Upgrade : MonoBehaviour
{
    private static int score;
    private int upgradeIndex;
    private string currentBuilding;
    private Setup setup;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject DebugMessagePanel;
    private IEnumerator currentCoroutine;
    private Dictionary<string, GameObject> toggleObjects;//to turn them on or off
    void Start()
    {
        setupToggle();
        score = 0;
        upgradeIndex = 0;
        currentBuilding = null;
        scoreText.text = "" + score;
        setup = FindObjectOfType<Setup>();
    }
    void Update()
    {
        if (currentBuilding != null)
        {
            int upgradeCost = setup.upgradeCost[currentBuilding][upgradeIndex - 1];
            if (MoneyScript.checkCash(upgradeCost))
            {
                float time = setup.upgradeTime[currentBuilding][upgradeIndex - 1];
                int incScore = setup.upgradeScores[currentBuilding][upgradeIndex - 1];
                FindObjectOfType<MoneyScript>().updateCash(upgradeCost, '-');
                StartCoroutine(setUpgrade(upgradeIndex, currentBuilding, incScore, time));
            }
            else
                DisplayMessage("Insufficient Cash");
        }
        currentBuilding = null;
    }
    private void setupToggle()
    {
        toggleObjects = new Dictionary<string, GameObject>();
        toggleObjects.Add("Windmill", GameObject.Find("Building Objects/Windmill"));
        toggleObjects.Add("Solar Farm", GameObject.Find("Building Objects/Solar Farm"));
        foreach (GameObject building in toggleObjects.Values)
            building.SetActive(false);
    }
    private void DisplayMessage(string msg)
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = DebugMessage(msg);
        StartCoroutine(currentCoroutine);
    }
    private void disableButton(string currentBuilding, int upgradeIndex)
    {
        setup.isButtonDisabled[currentBuilding][upgradeIndex - 1] = true;
        rayCast.refreshPanel();
    }
    private IEnumerator DebugMessage(string msg)
    {
        float fadeTime = 1.5f;
        int padding = 4;
        CanvasGroup panel=DebugMessagePanel.gameObject.GetComponent<CanvasGroup>();
        TextMeshProUGUI message = DebugMessagePanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        message.text = msg;
        DebugMessagePanel.GetComponent<RectTransform>().sizeDelta = new Vector2(message.preferredWidth + padding * 2f, message.preferredHeight + padding * 2f);
        panel.alpha = 1;
        DebugMessagePanel.SetActive(true);
        yield return new WaitForSeconds(fadeTime);
        while (panel.alpha > 0) 
        {
            panel.alpha -= Time.deltaTime / fadeTime;
            yield return null;
        }
        DebugMessagePanel.SetActive(false);
    }
    private IEnumerator setUpgrade(int upgradeIndex, string currentBuilding, int incScore, float time)//paramaters upgradeIndex and currentBuilding in case we need it later
    {
        Debug.Log("Upgrade Number :" + upgradeIndex + " of " + currentBuilding);
        //All Upgrades in common
        //Locked all upgrades after clicking button (Please change accordingly, using switch case below) 
        disableButton(currentBuilding, upgradeIndex);
        yield return new WaitForSeconds(time);
        score += incScore;
        //I don't think we need a switch case unless we want to give special changes for some of the upgrades ie Sprite Change etc.
        switch (currentBuilding)
        {
            case "Hospital":
                switch (upgradeIndex)
                {
                    case 1:
                        
                        break;
                    case 2:
                        
                        break;
                    case 3:
                        
                        break;
                    case 4:

                        break;
                    case 5:

                        break;
                    case 6:

                        break;
                }
                break;

            case "PoliceStation":
                switch (upgradeIndex)
                {
                    case 1:
                        
                        break;
                    case 2:
                        
                        break;
                    case 3:

                        break;
                    case 4:

                        break;
                    case 5:

                        break;
                    case 6:

                        break;
                }
                break;

            case "Slums":
                switch (upgradeIndex)
                {
                    case 1:

                        break;
                    case 2:

                        break;
                    case 3:

                        break;
                    case 4:

                        break;
                    case 5:

                        break;
                    case 6:

                        break;
                    case 7:

                        break;
                }
                break;
            case "Office":
                switch (upgradeIndex)
                {
                    case 1:

                        break;
                    case 2:

                        break;
                    case 3:

                        break;
                    case 4:

                        break;
                    case 5:

                        break;
                    case 6:

                        break;

                }
                break;
            case "Municpality":
                switch (upgradeIndex)
                {
                    case 1:

                        break;
                    case 2:

                        break;
                    case 3:

                        break;
                    case 4:

                        break;
                    case 5:

                        break;
                    case 6:

                        break;
                    case 7:

                        break;
                    case 8:

                        break;
                    case 9:

                        break;
                    case 10:

                        break;
                    case 11:

                        break;
                    case 12:

                        break;

                }
                break;
            case "Grid":
                {
                    switch (upgradeIndex)
                    {
                        case 1:toggleObjects["Solar Farm"].SetActive(true);
                            DisplayMessage("Congratulations Solar Farm has been Constructed");
                            break;

                        case 4:toggleObjects["Windmill"].SetActive(true);
                            DisplayMessage("Hurray Windmill has been Constructed");
                            break;
                    }
                }
                break;
        }

        scoreText.text = "" + score;
    }
    public void setState(int upgradeIndex,string currentBuilding)
    {
        this.upgradeIndex = upgradeIndex;
        this.currentBuilding = currentBuilding;
    }
}
