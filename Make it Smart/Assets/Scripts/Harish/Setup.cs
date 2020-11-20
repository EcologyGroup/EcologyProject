﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setup : MonoBehaviour
{
    [SerializeField] private Transform[] Buildings;
    [SerializeField] private Sprite psUpgrade;
    [SerializeField] private Sprite hospUpgrade;
    [SerializeField] private Sprite gridUpgrade;
    [SerializeField] private Sprite indsUpgrade;
    [SerializeField] private Sprite offUpgrade;
    [SerializeField] private Sprite MunicipalityUpgrade;
    [SerializeField] private Sprite slumsUpgrade;

    public Sprite[] psUpgradeButtons;
    public Sprite[] hospUpgradeButtons;
    public Sprite[] gridUpgradeButtons;
    public Sprite[] indsUpgradeButtons;
    public Sprite[] offUpgradeButtons;
    public Sprite[] MunicipalityUpgradeButtons;
    public Sprite[] slumsUpgradeButtons;

    public Dictionary<string,Sprite> sprite;
    public Dictionary<string, string[]> upgradeList;
    public Dictionary<string, Sprite[]> upgradeSprite;//If required to fill images to all UpgradeButtons
    public Dictionary<string, int[]> upgradeScores;
    public Dictionary<string, float[]> upgradeTime;
    public Dictionary<string, int[]> upgradeCost;
    public Dictionary<string, Boolean[]> isButtonDisabled;
    void Start()
    {
        sprite = new Dictionary<string, Sprite>();
        upgradeList = new Dictionary<string, string[]>();
        upgradeSprite = new Dictionary<string, Sprite[]>();
        upgradeScores = new Dictionary<string, int[]>();
        upgradeTime = new Dictionary<string, float[]>();
        upgradeCost = new Dictionary<string, int[]>();
        isButtonDisabled = new Dictionary<string, Boolean[]>();
        foreach (Transform Building in Buildings)
        {
            if (Building.tag == "Untagged")
                Building.tag = "Building";
        }
        initialize();
    }
    void initialize()
    {
        sprite.Add("PoliceStation", psUpgrade);
        sprite.Add("Hospital", hospUpgrade);
        sprite.Add("Grid", gridUpgrade);
        sprite.Add("Industry", indsUpgrade);
        sprite.Add("Office", offUpgrade);
        sprite.Add("Municpality", MunicipalityUpgrade);
        sprite.Add("Slums", slumsUpgrade);

        //Add Upgrades for remaining Buildings similarly
        upgradeList.Add("Hospital", new string[] { "Super Specialty Upgrade\nThis is a Super Speciality Upgrade\nClick on the Button","XXX","YYY","4","5","6" });
        upgradeList.Add("PoliceStation", new string[] { "Virtual Police Station", "XXX", "YYY" , "ZZZ","5","6"});
        upgradeList.Add("Grid", new string[] { "B1", "XXX", "YYY", "ZZZ" });
        upgradeList.Add("Industry", new string[] { "I1", "XXX", "YYY", "ZZZ" });
        upgradeList.Add("Office", new string[] { "O1", "O2", "O3", "O4", "O5", "O6" });
        upgradeList.Add("Municpality", new string[] { "B1", "B2", "B3", "B4","B5", "B6", "B7", "B8", "B9", "B10",
                                    "B11","B12"});
        upgradeList.Add("Slums", new string[] { "S1", "S2", "S3", "S4","S5","S6","S7" });

        //default value of Boolean is false
        isButtonDisabled.Add("Hospital", new Boolean[6]);
        isButtonDisabled.Add("PoliceStation", new Boolean[6]);
        isButtonDisabled.Add("Grid", new Boolean[4]);
        isButtonDisabled.Add("Industry", new Boolean[4]);
        isButtonDisabled.Add("Office", new Boolean[6]);
        isButtonDisabled.Add("Municpality", new Boolean[12]);
        isButtonDisabled.Add("Slums", new Boolean[7]);

        upgradeCost.Add("Hospital", new int[] { 2000, 2000, 2000, 2000, 2000, 2000 });
        upgradeCost.Add("PoliceStation", new int[] { 1500, 1500, 1500, 1500, 1500, 1500 });
        upgradeCost.Add("Grid", new int[] { 1800, 1800, 1800, 1800 });
        upgradeCost.Add("Industry", new int[] { 2100, 2100, 2100, 2100 });
        upgradeCost.Add("Office", new int[] { 1250, 1250, 1250, 1250, 1250, 1250 });
        upgradeCost.Add("Municpality", new int[] { 2000, 2000, 2000, 2000, 2000, 2000, 2000, 2000, 2000, 2000, 2000, 2000 });
        upgradeCost.Add("Slums", new int[] { 1500, 1500, 1500, 1500, 1500, 1500, 1500 });

        upgradeSprite.Add("PoliceStation",psUpgradeButtons);
        upgradeSprite.Add("Hospital",hospUpgradeButtons);
        upgradeSprite.Add("Grid", gridUpgradeButtons);
        upgradeSprite.Add("Industry", indsUpgradeButtons);
        upgradeSprite.Add("Office", offUpgradeButtons);
        upgradeSprite.Add("Municpality", MunicipalityUpgradeButtons);
        upgradeSprite.Add("Slums", slumsUpgradeButtons);

        upgradeScores.Add("Hospital", new int[] { 100, 100, 100, 100, 100, 100 });
        upgradeScores.Add("PoliceStation", new int[] { 90, 90, 90, 90, 90, 90 });
        upgradeScores.Add("Grid", new int[] { 110, 110, 110, 110 });
        upgradeScores.Add("Industry", new int[] { 100, 100, 100, 100 });
        upgradeScores.Add("Office", new int[] { 80, 80, 80, 80, 80, 80 });
        upgradeScores.Add("Municpality", new int[] { 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150, 150 });
        upgradeScores.Add("Slums", new int[] { 120, 120, 120, 120, 120, 120, 120 });

        upgradeTime.Add("Hospital", new float[] { 5f, 6f, 7f, 8f, 9f, 10f });
        upgradeTime.Add("PoliceStation", new float[] { 5f, 6f, 7f, 8f, 9f, 10f });
        upgradeTime.Add("Grid", new float[] { 5f, 6f, 7f, 8f });
        upgradeTime.Add("Industry", new float[] { 5f, 6f, 7f, 8f });
        upgradeTime.Add("Office", new float[] { 5f, 6f, 7f, 8f, 9f, 10f });
        upgradeTime.Add("Municpality", new float[] { 5f, 6f, 7f, 8f, 9f, 10f, 5f, 6f, 7f, 8f, 9f, 10f });
        upgradeTime.Add("Slums", new float[] { 5f, 6f, 7f, 8f, 9f, 10f, 5f });
    }
}