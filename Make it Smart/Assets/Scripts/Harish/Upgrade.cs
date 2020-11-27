using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private GameObject UpgradeAnimationPrefab;
    [SerializeField] private GameObject UpgradeAnimations;
    public static int score;
    private int upgradeIndex;
    private string currentBuilding;
    private Setup setup;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject DebugMessagePanel;
    [SerializeField] private GameObject Slum1;
    [SerializeField] private GameObject Slum2;
    [SerializeField] private Sprite Slums;
    private IEnumerator currentCoroutine;
    private Dictionary<string, GameObject> toggleObjects;//to turn them on or off
    void Start()
    {
        setupToggle();
        score = 0;
        upgradeIndex = 0;
        currentBuilding = null;
        
        setup = FindObjectOfType<Setup>();
    }
    void Update()
    {
        scoreText.text = "" + score;
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
    private void playAnimation(string currentBuilding)
    {
        foreach (Transform building in setup.Buildings)
        {
            if (building.gameObject.name == currentBuilding)
            {
                GameObject animator = Instantiate(UpgradeAnimationPrefab, building.position, Quaternion.identity);
                animator.transform.parent = UpgradeAnimations.transform;
                StartCoroutine(animator.GetComponent<InvokeAnimation>().playAnimation());
            }
        }
    }
    private IEnumerator DebugMessage(string msg)
    {
        float fadeTime = 2f;
        int padding = 4;
        CanvasGroup panel=DebugMessagePanel.gameObject.GetComponent<CanvasGroup>();
        TextMeshProUGUI message = DebugMessagePanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        message.text = msg;
        RectTransform panelTransform = DebugMessagePanel.GetComponent<RectTransform>();
        panelTransform.sizeDelta = Vector2.zero;
        panelTransform.sizeDelta = new Vector2(message.preferredWidth + padding * 2f, message.preferredHeight + padding * 2f);
        panelTransform.anchoredPosition = new Vector2(-200f + panelTransform.sizeDelta.x / 2, panelTransform.anchoredPosition.y);
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
    private IEnumerator constructBuilding(string buildingName)
    {
        float fadeTime = 1f;
        GameObject buildingObject = toggleObjects[buildingName];
        SpriteRenderer sprite = buildingObject.GetComponent<SpriteRenderer>();
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
        buildingObject.SetActive(true);
        while (sprite.color.a <= 1)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a + Time.deltaTime / fadeTime);
            yield return null;
        }
        DisplayMessage("Hurray " + buildingName + " has been Constructed!!");
    }
    private IEnumerator setUpgrade(int upgradeIndex, string currentBuilding, int incScore, float time)//paramaters upgradeIndex and currentBuilding in case we need it later
    {
        Debug.Log("Upgrade Number :" + upgradeIndex + " of " + currentBuilding);
        //All Upgrades in common
        //Locked all upgrades after clicking button (Please change accordingly, using switch case below) 
        disableButton(currentBuilding, upgradeIndex);
        yield return new WaitForSeconds(time);
        DisplayMessage("Upgrade Done " + setup.upgradeList[currentBuilding][upgradeIndex - 1]);
        playAnimation(currentBuilding);
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
                        Slum1.GetComponent<SpriteRenderer>().sprite=Slums;
                        Slum2.GetComponent<SpriteRenderer>().sprite = Slums;
                        DisplayMessage("Slum Houses have had a major Revamp. Looks very refreshing now!!");
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
                        case 1: StartCoroutine(constructBuilding("Solar Farm"));
                            break;

                        case 4:StartCoroutine(constructBuilding("Windmill"));
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
