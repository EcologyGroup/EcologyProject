using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MoneyScript : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] private float ChangeTime = 10.0f;
    [SerializeField] private int IncomeValue = 500;
    private static float changeTime;
    private static int incomeValue;
    private static int totalAmount;
    private static TextMeshProUGUI moneyText;
    private Image cashPanelImage;
    
    public static Boolean checkCash(int amt)
    {
        return amt <= totalAmount;
    }
    public static void updateCash(int amt, char c)
    {
        if (c == '+') totalAmount += amt;
        else if (c == '-') totalAmount -= amt;
        moneyText.text = "" + totalAmount;
        FindObjectOfType<MoneyScript>().StartCoroutine(colorBlink());
    }
    private void resumeIncome()
    {
        moneyText = gameObject.GetComponent<TextMeshProUGUI>();
        moneyText.text = string.Format("{0:0}", totalAmount);
        StartCoroutine(income());
    }
    private static IEnumerator colorBlink()
    {
        yield return null;
    }
    public static IEnumerator refresh()
    {
        float t = Timer.timeRemaining % changeTime;
        if (t != 0)
        {
            yield return new WaitForSeconds(t);
            updateCash(incomeValue, '+');
        }
        FindObjectOfType<MoneyScript>().resumeIncome();
    }
    void Start()
    {
        cashPanelImage = panel.GetComponent<Image>();
        changeTime = ChangeTime;
        incomeValue = IncomeValue;
        resumeIncome();   
    }
    private IEnumerator income()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeTime);
            updateCash(incomeValue, '+');
        }
    }
}
