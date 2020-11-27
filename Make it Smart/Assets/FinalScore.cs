using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FinalScore : MonoBehaviour
{
    
    private Text finalText;
    private int scoreFinal=0;
    private int increment = 5;
    private int scoreDisplayed = 0;

    // Start is called before the first frame update
    void Start()
    {
        finalText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreDisplayed < scoreFinal)
        {
            scoreDisplayed += increment;
            finalText.text = scoreDisplayed.ToString("0");
        }
    }
    private void OnEnable()
    {
        scoreFinal = Upgrade.score;
    }
}
