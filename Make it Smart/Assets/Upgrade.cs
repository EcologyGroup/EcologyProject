using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Upgrade : MonoBehaviour
{
    private static int score=0;
    void Start()
    {
        
    }
    public static void setUpgrade(int i, string buildingname)
    {
        Debug.Log("Upgrade Number :" + i + " of " + buildingname);
        switch (buildingname)
        {
            case "Hospital":
                //if else ladder for each upgrade and seperate function for that
                switch (i)
                {
                    case 1:
                        
                        break;
                }
                break;

            case "PoliceStation":
                break;
        }
    }
}
