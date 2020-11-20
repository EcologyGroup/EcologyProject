using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MoneyScript : MonoBehaviour
{
    [SerializeField] private float changeTime = 10.0f;
    [SerializeField] private int incomeValue = 500;
    private static int totalAmount;
    private TextMeshProUGUI moneyText;
    public static Boolean checkCash(int amt)
    {
        return amt <= totalAmount;
    }
    public static void updateCash(int amt, char c)
    {
        if (c == '+') totalAmount += amt;
        else if (c == '-') totalAmount -= amt;
    }
    void Start()
    {
        moneyText =gameObject.GetComponent<TextMeshProUGUI>();
        moneyText.text = string.Format("{0:0}", totalAmount);
        StartCoroutine(income());
    }
    void Update()
    {
        moneyText.text = "" + totalAmount;
    }
    IEnumerator income()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeTime);
            totalAmount += incomeValue;
        }
    }
}
