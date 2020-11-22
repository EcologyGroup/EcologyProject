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
    void Start()
    {
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
                MoneyScript.updateCash(upgradeCost, '-');
                StartCoroutine(setUpgrade(upgradeIndex, currentBuilding, incScore, time));
            }
            else 
            {
                //take reference of a panel close to the Cash field and display something : "Insufficient Cash"
                //Debug.Log("Insufficient Cash");
                if (currentCoroutine != null)
                    StopCoroutine(currentCoroutine);
                currentCoroutine = DebugMessage("Insufficient Cash");
                StartCoroutine(currentCoroutine);
            }
        }
        currentBuilding = null;
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
        DebugMessagePanel.SetActive(true);
        yield return new WaitForSeconds(fadeTime);
        while (panel.alpha > 0) 
        {
            panel.alpha -= Time.deltaTime / fadeTime;
            yield return null;
        }
        DebugMessagePanel.SetActive(false);
        panel.alpha = 1;
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
        /*
        switch (buildingname)
        {
            case "Hospital":
                switch (i)
                {
                    case 1:
                        yield return new WaitForSeconds(5f);
                        score += 100;
                        Debug.Log(score);
                        break;
                    case 2:
                        yield return new WaitForSeconds(7f);
                        score += 200;
                        Debug.Log(score);
                        break;
                    case 3:
                        yield return new WaitForSeconds(10f);
                        score += 300;
                        Debug.Log(score);
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
                switch (i)
                {
                    case 1:
                        yield return new WaitForSeconds(10f);
                        score += 260;
                        Debug.Log(score);
                        break;
                    case 2:
                        yield return new WaitForSeconds(10f);
                        score += 350;
                        Debug.Log(score);
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
                switch (i)
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
                switch (i)
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
                switch (i)
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
                yield return null;
                break;
        }
        */
        scoreText.text = "" + score;
    }
    public void setState(int upgradeIndex,string currentBuilding)
    {
        this.upgradeIndex = upgradeIndex;
        this.currentBuilding = currentBuilding;
    }
}
