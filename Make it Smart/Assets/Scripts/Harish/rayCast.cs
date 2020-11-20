﻿using System;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class rayCast : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject upgradePanelCanvas;
    [SerializeField] TextMeshProUGUI buildingname;
    [SerializeField] Image panelSprite;
    [SerializeField] GameObject[] buttons;
    private Sprite upgradeSprite;
    private Sprite[] buttonSprites;
    private Setup setup;
    private PauseMenu pause;
    private Boolean isPanelActive;
    private string currentBuilding;

    [SerializeField] float padding = 8f;
    private GameObject popup;
    private Button currentButton;
    private Color def;
    [SerializeField] private Color chg;
    void Start()
    {
        isPanelActive = false;
        upgradePanelCanvas.SetActive(isPanelActive);
        setup = FindObjectOfType<Setup>();
        pause = FindObjectOfType<PauseMenu>();
        currentBuilding = "Building";

        def = buttons[0].transform.GetChild(0).GetComponent<Image>().color;
        chg = new Color(0, 0.90f, 0.30f, 1);
    }
    void Update()
    {
        if (!pause.isGamePaused())
        {
            
            if (Input.GetMouseButtonDown(0))//LeftMouseClick - 0
            {
                isPanelActive = false;
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hitInfo = Physics2D.Raycast(ray.origin, ray.direction);
                if (hitInfo)
                    if (hitInfo.collider.CompareTag("UpgradeCanvas"))
                    {
                        isPanelActive = true;
                    }
                    else if (hitInfo.collider.CompareTag("Building"))
                    {
                        currentBuilding = hitInfo.collider.name;
                        Debug.Log(currentBuilding);
                        if (setup.sprite.ContainsKey(currentBuilding))
                        {
                            isPanelActive = true;
                            upgradeSprite = setup.sprite[currentBuilding];
                            buttonSprites = setup.upgradeSprite[currentBuilding];
                        }
                        if (setup.upgradeList.ContainsKey(currentBuilding))
                            setButtons();
                    }
            }
            if (isPanelActive)
                displayPanel(currentBuilding);
            else
                upgradePanelCanvas.SetActive(isPanelActive);
        }
        else
            upgradePanelCanvas.SetActive(false);
    }
    void setButtons()
    {
        string[] upgrades=setup.upgradeList[currentBuilding];
        for (int i = 0; i < upgrades.Length; i++)
            buttons[i].SetActive(true);
        for (int i = upgrades.Length; i < buttons.Length; i++)
            buttons[i].SetActive(false);
        for (int i = 0; i < upgrades.Length; i++)
        {
            Image upgradeImage = buttons[i].transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
            upgradeImage.sprite = buttonSprites[i];
            //reduced opacity of sprite if locked
            if (setup.isButtonDisabled[currentBuilding][i])
                upgradeImage.color = new Color(1, 1, 1, 0.5f);
        }
    }
    void displayPanel(string buildingname)
    {
        this.buildingname.text = buildingname;
        panelSprite.sprite = upgradeSprite;
        upgradePanelCanvas.SetActive(true);   
    }
    public void upgrade(int index)
    {
        if (!setup.isButtonDisabled[currentBuilding][index - 1])
            FindObjectOfType<Upgrade>().setState(index, currentBuilding);
        else
            Debug.Log("Locked");
    }
    public void mouseEnter(int i)
    {
        //Debug.Log("Mouse Enter Button:" + i);
        currentButton = buttons[i - 1].transform.GetChild(0).GetComponent<Button>();
        popup = buttons[i - 1].transform.GetChild(1).gameObject;
        RectTransform popupTransform = popup.GetComponent<RectTransform>();
        String upgradeDesc = setup.upgradeList[currentBuilding][i - 1];
        TextMeshProUGUI popupText = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        popupText.text = upgradeDesc;
        Vector2 preferredSize = new Vector2(popupText.preferredWidth + padding * 2f, popupText.preferredHeight + padding * 2f);
        Vector2 anchorPos = new Vector2(0, preferredSize.y / 2f);
        popupTransform.sizeDelta = preferredSize;
        popupTransform.anchoredPosition = anchorPos;
        currentButton.GetComponent<Image>().color = chg;
        popup.SetActive(true);
    }
    public void mouseExit(int i)
    {
        //Debug.Log("Mouse Exit");
        currentButton.GetComponent<Image>().color = def;
        popup.SetActive(false);
    }
}