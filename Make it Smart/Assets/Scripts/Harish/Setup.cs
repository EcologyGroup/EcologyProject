using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setup : MonoBehaviour
{
    [SerializeField] private Transform[] Buildings;
    [SerializeField] private Sprite psUpgrade;
    [SerializeField] private Sprite hospUpgrade;
    [SerializeField] private Sprite bankUpgrade;
    [SerializeField] private Sprite indsUpgrade;
    [SerializeField] private Sprite offUpgrade;
    [SerializeField] private Sprite schlUpgrade;
    [SerializeField] private Sprite slumsUpgrade;

    public Dictionary<string,Sprite> sprite;
    public Dictionary<string, string[]> upgradeList;
    public Dictionary<string, Sprite[]> upgradeSprite;//If required to fill images to all UpgradeButtons
    void Start()
    {
        sprite = new Dictionary<string, Sprite>();
        upgradeList = new Dictionary<string, string[]>();
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
        sprite.Add("Bank", bankUpgrade);
        sprite.Add("Industry", indsUpgrade);
        sprite.Add("Office", offUpgrade);
        sprite.Add("School", schlUpgrade);
        sprite.Add("Slums", slumsUpgrade);

        //Add Upgrades for remaining Buildings similarly
        upgradeList.Add("Hospital", new string[] { "Super Specialty Upgrade","XXX","YYY" });
        upgradeList.Add("PoliceStation", new string[] { "Virtual Police Station", "XXX", "YYY" , "ZZZ"});
        upgradeList.Add("Bank", new string[] { "B1", "XXX", "YYY", "ZZZ" });
        upgradeList.Add("Industry", new string[] { "B1", "XXX", "YYY", "ZZZ" });
        upgradeList.Add("Office", new string[] { "B1", "XXX", "YYY", "ZZZ" });
        upgradeList.Add("School", new string[] { "B1", "XXX", "YYY", "ZZZ" });
        upgradeList.Add("Slums", new string[] { "B1", "XXX", "YYY", "ZZZ" });

    }
}