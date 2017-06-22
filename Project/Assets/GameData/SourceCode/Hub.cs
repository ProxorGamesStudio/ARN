using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hub : MonoBehaviour {
    
    public string Name;
    public int Level = 1;
    public enum type { Common, Scientific, Millitary, Commercial, Goverment }
    public type Type;
    [HideInInspector]
    public GameObject[] lines;
    [HideInInspector]
    public GameObject[] links;

    void Start()
    {
        SwitchLanguage();
    }

    public void SwitchLanguage()
    {
        Name = FindObjectOfType<Localisation>().GetLocalizationString(Name);
    }

}
