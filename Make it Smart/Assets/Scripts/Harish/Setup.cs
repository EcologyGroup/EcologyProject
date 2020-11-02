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
    //public Dictionary<string, Sprite> background;
    void Start()
    {
        sprite = new Dictionary<string, Sprite>();        
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
    }
}