using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Options : MonoBehaviour {

    [HideInInspector]
    public int currentSettingWindow;
    public Settings settings;
    public Dropdown windowsMode, vsync, resolution, graphicsQuality;
    public Slider brightness;

    private void Awake()
    {
        if (PlayerPrefs.GetString("Language") == string.Empty || PlayerPrefs.GetString("Language") == null)
            SetOptimalSettings();
    }

    void SetOptimalSettings()
    {
        settings.general.Language = Application.systemLanguage.ToString() == "Russian"? "Русский" : "English";

        settings.video.brightness = 1f;
        settings.video.DisplayNum = Camera.main.targetDisplay;
        settings.video.Resolution = new Vector2(Screen.width, Screen.height);
        settings.video.GraphicQuality = SystemInfo.graphicsMemorySize >= 1000 ? "High" : SystemInfo.graphicsMemorySize >= 500 ? "Medium" : "Low";
        settings.video.WindowMode = "Full screen";

        settings.sound.GameplayVolume = 1;
        settings.sound.GeneralVolume = 1;
        settings.sound.MusicVolume = 1;

        settings.controls.controls = new Controller();

        resolution.options.Add(new Dropdown.OptionData(Screen.width + "x" + Screen.height, null));
        resolution.value = resolution.options.Count - 1;
    }

    public void SetSettingWindowNum(int num)
    {
        currentSettingWindow = num;
    }

  //  IEnumerator GlitchTransaction
   // {
//
  //  }
	
	// Update is called once per frame
	void Update () {
       
    }
}

[Serializable]
public class Settings
{
    [Serializable]
    public struct General
    {
        public string Language;
    }
    [Serializable]
    public struct Video
    {
        public string WindowMode, GraphicQuality;
        public Vector2 Resolution;
        public float brightness;
        public int DisplayNum;
        public bool Vsync;
    }
    [Serializable]
    public struct Sound
    {
        public float GeneralVolume, MusicVolume, GameplayVolume;
    }
    [Serializable]
    public struct Controls
    {
        public Controller controls;
    }
    public General general;
    public Video video;
    public Sound sound;
    public Controls controls;
}
[Serializable]
public class Controller
{
    [Serializable]
    public struct Button
    {
        public string name;
        public KeyCode key;

        public Button(string _name, KeyCode k)
        {
            name = _name;
            key = k;
        }
    }

    public List<Button> Buttons = new List<Button>();

    public Controller()
    {
        Buttons.Add(new Button("Escape", KeyCode.Escape));
        Buttons.Add(new Button("Mouse 1", KeyCode.Mouse1));
    }
}