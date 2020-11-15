using System;
using TMPro;
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
    private Setup setup;
    private PauseMenu pause;
    private Boolean isPanelActive;
    private string currentBuilding;
    private Upgrade upgrade1;
    void Start()
    {
        isPanelActive = false;
        upgradePanelCanvas.SetActive(isPanelActive);
        setup = FindObjectOfType<Setup>();
        pause = FindObjectOfType<PauseMenu>();
        upgrade1 = FindObjectOfType<Upgrade>();
        currentBuilding = "Building";
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
        Image upgradeImage = buttons[0].transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();//This can be used to access the image of the UpgradeButtons
        upgradeImage.sprite = upgradeSprite;
    }
    void displayPanel(string buildingname)
    {
        this.buildingname.text = buildingname;
        panelSprite.sprite = upgradeSprite;
        upgradePanelCanvas.SetActive(true);   
    }
    public void upgrade(int index)
    {
        upgrade1.setVar(index, currentBuilding);
    }
}