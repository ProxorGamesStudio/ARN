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
    public Image PauseEffect, PauseIcon, PlayIcon;
    bool startEffectPause, startEffectPlay, backPlay;
    float effectTargetValue;
    // Use this for initialization
    void Start () {
        _Time = DateTime.Now;
	}
	
	// Update is called once per frame
	void Update () {
        _Time = _Time.AddMinutes(Time.deltaTime * TimeFactor);
        TimeUI.text = _Time.ToString("HH:mm:ss");
        DateUI.text = _Time.Day.ToString() + months[_Time.Month];
        PauseEffect.color = Color.Lerp(PauseEffect.color, new Color(PauseEffect.color.r, PauseEffect.color.g, PauseEffect.color.b, effectTargetValue), 0.05f);
        if(startEffectPause)
        {
            PauseIcon.transform.localScale = Vector3.Lerp(PauseIcon.transform.localScale, new Vector3(1, 1, 1), 0.16f);
            PauseIcon.color = Color.Lerp(PauseIcon.color, new Color(PauseIcon.color.r, PauseIcon.color.g, PauseIcon.color.b, 1), 0.05f);
            PlayIcon.color = Color.Lerp(PlayIcon.color, new Color(PlayIcon.color.r, PlayIcon.color.g, PlayIcon.color.b, 0), 0.1f);
        }
        if(startEffectPlay)
        {
            PauseIcon.color = Color.Lerp(PauseIcon.color, new Color(PauseIcon.color.r, PauseIcon.color.g, PauseIcon.color.b, 0), 0.14f);
            PlayIcon.transform.localScale = Vector3.Lerp(PlayIcon.transform.localScale, new Vector3(1, 1, 1), 0.16f);
            if (PlayIcon.transform.localScale.x > 1.5f && !backPlay)
                PlayIcon.color = Color.Lerp(PlayIcon.color, new Color(PlayIcon.color.r, PlayIcon.color.g, PlayIcon.color.b, 1), 0.05f);
            else
                backPlay = true;
            if(backPlay)
                PlayIcon.color = Color.Lerp(PlayIcon.color, new Color(PlayIcon.color.r, PlayIcon.color.g, PlayIcon.color.b, 0), 0.1f);
        }
    }

    public void Play(float TimeScale)
    {
        if (Time.timeScale == 0)
        {
            effectTargetValue = 0;
            startEffectPlay = true;
            PlayIcon.transform.localScale = new Vector3(50, 50, 1);
            PlayIcon.color = new Color(PlayIcon.color.r, PlayIcon.color.g, PlayIcon.color.b, 0);
            startEffectPause = false;
        }
        Time.timeScale = TimeScale;
    }

    public void Pause()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
            effectTargetValue = 0.7f;
            startEffectPause = true;
            startEffectPlay = false;
            backPlay = false;
            PauseIcon.transform.localScale = new Vector3(50, 50, 1);
            PauseIcon.color = new Color(PauseIcon.color.r, PauseIcon.color.g, PauseIcon.color.b, 0);
        }
    }
}
