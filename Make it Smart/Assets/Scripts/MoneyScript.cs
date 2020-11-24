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
    [SerializeField] private static float changeTime = 10.0f;
    [SerializeField] private static int incomeValue = 500;
    private static int totalAmount;
    private static TextMeshProUGUI moneyText;
    private static IEnumerator currentCoroutine;
    public static Boolean checkCash(int amt)
    {
        return amt <= totalAmount;
    }
    public static void updateCash(int amt, char c)
    {
        if (c == '+') totalAmount += amt;
        else if (c == '-') totalAmount -= amt;
        moneyText.text = "" + totalAmount;
    }
    public void resumeIncome()
    {
        moneyText = gameObject.GetComponent<TextMeshProUGUI>();
        moneyText.text = string.Format("{0:0}", totalAmount);
        currentCoroutine = income();
        StartCoroutine(currentCoroutine);
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
