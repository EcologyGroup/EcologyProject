using System;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class rayCast : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject upgradePanelCanvas;
    [SerializeField] private TextMeshProUGUI buildingname;
    [SerializeField] private Image panelSprite;
    [SerializeField] private GameObject[] buttons;
    private Sprite upgradeSprite;
    private Sprite[] buttonSprites;
    private Setup setup;
    private Boolean isPanelActive;
    private string currentBuilding;

    [SerializeField] float padding = 8f;
    private GameObject popup;
    private Button currentButton;
    private Color def;
    [SerializeField] private Color enabled;
    [SerializeField] private Color disabled;
    void Start()
    {
        isPanelActive = false;
        upgradePanelCanvas.SetActive(isPanelActive);
        setup = FindObjectOfType<Setup>();
        currentBuilding = "Building";

        def = buttons[0].transform.GetChild(0).GetComponent<Image>().color;
        enabled = new Color(0, 0.90f, 0.30f, 1);
        disabled = new Color(0.8f, 0.05f, 0.05f, 1);
    }
    void Update()
    {
        if (!PauseMenu.isGamePaused())
        {
            if (Input.GetMouseButtonDown(0))//LeftMouseClick - 0
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hitInfo = Physics2D.Raycast(ray.origin, ray.direction);
                if (hitInfo)
                {
                    if (hitInfo.collider.CompareTag("UpgradeCanvas") && isPanelActive) 
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
                        else
                            isPanelActive = false;
                        if (setup.upgradeList.ContainsKey(currentBuilding))
                            setButtons();
                    }
                    else
                        isPanelActive = false;
                }
                else
                    isPanelActive = false;

            }
            if (isPanelActive)
                displayPanel(currentBuilding);
            else
                upgradePanelCanvas.SetActive(isPanelActive);
        }
        else
            upgradePanelCanvas.SetActive(false);
    }
    public static void refreshPanel()
    {
        FindObjectOfType<rayCast>().setButtons();
    }
    private void setButtons()
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
            else
                upgradeImage.color = new Color(1, 1, 1, 1f);
        }
    }
    private void displayPanel(string buildingname)
    {
        this.buildingname.text = buildingname;
        panelSprite.sprite = upgradeSprite;
        upgradePanelCanvas.SetActive(true);   
    }
    private void upgrade(int index)
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
        if (!setup.isButtonDisabled[currentBuilding][i - 1]) 
        {
            popupText.text = upgradeDesc;
            currentButton.GetComponent<Image>().color = enabled;
        }
        else
        {
            popupText.text = "Completed";
            currentButton.GetComponent<Image>().color = disabled;
        }
        Vector2 preferredSize = new Vector2(popupText.preferredWidth + padding * 2f, popupText.preferredHeight + padding * 2f);
        Vector2 anchorPos = new Vector2(0, preferredSize.y / 2f);
        popupTransform.sizeDelta = preferredSize;
        popupTransform.anchoredPosition = anchorPos;
        popup.SetActive(true);
    }
    public void mouseExit(int i)
    {
        //Debug.Log("Mouse Exit");
        currentButton.GetComponent<Image>().color = def;
        popup.SetActive(false);
    }
}