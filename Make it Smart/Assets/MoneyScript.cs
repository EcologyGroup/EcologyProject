using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyScript : MonoBehaviour
{
    public float MoneyTime;
    public int totalAmount;
    public int incomeValue;
    private TextMeshProUGUI moneyText;
    // Start is called before the first frame update
    void Start()
    {
        moneyText =gameObject.GetComponent<TextMeshProUGUI>();
        moneyText.text = string.Format("{0:0}", totalAmount);
        StartCoroutine(income());
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "" + totalAmount;
    }

    IEnumerator income()
    {
        while (true)
        {
            yield return new WaitForSeconds(MoneyTime);
            totalAmount += incomeValue;
        }
    }
}
