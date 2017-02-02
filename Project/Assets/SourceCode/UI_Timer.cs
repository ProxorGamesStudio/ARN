using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UI_Timer : MonoBehaviour {

    public float TimeFactor;
    DateTime _Time;
    public Text DateUI, TimeUI;
    public string[] months = new string[] { "", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"};

    // Use this for initialization
    void Start () {
        _Time = DateTime.Now;
	}
	
	// Update is called once per frame
	void Update () {

        _Time = _Time.AddMinutes(Time.deltaTime * TimeFactor);
        TimeUI.text = _Time.ToString("HH:mm:ss");
        DateUI.text = _Time.Day.ToString() + months[_Time.Month];
    }
}
