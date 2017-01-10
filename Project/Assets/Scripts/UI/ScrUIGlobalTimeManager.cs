using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrUIGlobalTimeManager : MonoBehaviour
{
    public Text DataLabel;
    public Button[] BtnMods;
    ScrGlobalTimeManager GT;

    public void Start()
    {
        GT = GameObject.FindObjectOfType<ScrGlobalTimeManager>();
    }

    public void SetMult(int value)
    {
        ScrGlobalTimeManager.Instance.SetTimeMode(value);
    }

    void Update()
    {
        DataLabel.text = /*DataText + */GT.GetDateStr(); 
    }
}
