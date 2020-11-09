using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
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
                break;

            case "PoliceStation":
                break;
        }
    }
}
