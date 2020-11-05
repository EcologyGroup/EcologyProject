using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class rayCast : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject upgradePanelCanvas;
    [SerializeField] TextMeshProUGUI buildingname;
    [SerializeField] Image panelSprite;
    private Sprite upgradeSprite;
    Setup setup;
    PauseMenu pause;
    void Start()
    {
        upgradePanelCanvas.SetActive(false);
        setup = FindObjectOfType<Setup>();
        pause = FindObjectOfType<PauseMenu>();
    }
    void Update()
    {
        if (!pause.isGamePaused())
        {
            if (Input.GetMouseButtonDown(0))//LeftMouseClick - 1
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hitInfo = Physics2D.Raycast(ray.origin, ray.direction);
                if (hitInfo)
                {
                    if (hitInfo.collider.CompareTag("Building"))
                    {
                        string name = hitInfo.collider.name;
                        Debug.Log(name);
                        if (setup.sprite.ContainsKey(name))
                        {
                            upgradeSprite = setup.sprite[name];
                            displayPanel(name);
                        }
                        else
                            upgradePanelCanvas.SetActive(false);
                    }
                }
                else
                    upgradePanelCanvas.SetActive(false);
            }
        }
        else
            upgradePanelCanvas.SetActive(false);
    }
    void displayPanel(string buildingname)
    {
        this.buildingname.text = buildingname;
        panelSprite.sprite = upgradeSprite;
        upgradePanelCanvas.SetActive(true);   
    }
}