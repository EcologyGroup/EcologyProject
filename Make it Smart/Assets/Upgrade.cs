using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Upgrade : MonoBehaviour
{
    private static int score=0;
    private int k=0;
    private string building;
    void Start()
    {
        
    }
    void Update()
    {
        if(k!=0)
            StartCoroutine(setUpgrade(k,building));
        k = 0;
    }
    public IEnumerator setUpgrade(int i, string buildingname)
    {
        Debug.Log("Upgrade Number :" + i + " of " + buildingname);
        switch (buildingname)
        {
            case "Hospital":
                //if else ladder for each upgrade and seperate function for that
                switch (i)
                {
                    case 1:
                        yield return new WaitForSeconds(5f);
                        score += 100;
                        Debug.Log(score);
                        break;
                    case 2:
                        yield return new WaitForSeconds(7f);
                        score += 200;
                        Debug.Log(score);
                        break;
                    case 3:
                        yield return new WaitForSeconds(10f);
                        score += 300;
                        Debug.Log(score);
                        break;
                    case 4:

                        break;
                    case 5:

                        break;
                    case 6:

                        break;
                }
                break;

            case "PoliceStation":
                switch (i)
                {
                    case 1:
                        yield return new WaitForSeconds(10f);
                        score += 260;
                        Debug.Log(score);
                        break;
                    case 2:
                        yield return new WaitForSeconds(10f);
                        score += 350;
                        Debug.Log(score);
                        break;
                    case 3:

                        break;
                    case 4:

                        break;
                    case 5:

                        break;
                    case 6:

                        break;
                }
                break;

            case "Slums":
                switch (i)
                {
                    case 1:

                        break;
                    case 2:

                        break;
                    case 3:

                        break;
                    case 4:

                        break;
                    case 5:

                        break;
                    case 6:

                        break;
                    case 7:

                        break;
                }
                break;
            case "Office":
                switch (i)
                {
                    case 1:

                        break;
                    case 2:

                        break;
                    case 3:

                        break;
                    case 4:

                        break;
                    case 5:

                        break;
                    case 6:

                        break;

                }
                break;
            case "Municpality":
                switch (i)
                {
                    case 1:

                        break;
                    case 2:

                        break;
                    case 3:

                        break;
                    case 4:

                        break;
                    case 5:

                        break;
                    case 6:

                        break;
                    case 7:

                        break;
                    case 8:

                        break;
                    case 9:

                        break;
                    case 10:

                        break;
                    case 11:

                        break;
                    case 12:

                        break;

                }
                yield return null;
                break;
        }
    }
    /*public void setUpgrade1(int i, string buildingname)
    {
        setUpgrade(i, buildingname);
    }*/
    public void setVar(int i,string x)
    {
        this.k = i;
        this.building = x;
    }
}
