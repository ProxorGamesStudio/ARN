using UnityEngine;
using UnityEngine.UI;

public class ScrUIAlertViewer : MonoBehaviour {

    public Text lvl;
    public Slider value;
    ScrAlertSystem alert;

    void Start () {
        alert = GameObject.FindObjectOfType<ScrAlertSystem>();
        value.maxValue = alert.MaxAlertValue;    
    }
	
	void Update () {
        lvl.text = alert.GetAlertLvl().ToString();
        value.value = alert.AlertValue;
    }
}
