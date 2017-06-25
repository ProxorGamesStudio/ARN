using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using ProxorGamesLocalisation;

public class Options : MonoBehaviour {

    [HideInInspector]
    public int currentSettingWindow;
    public Settings settings = new Settings();
    public Dropdown Language, windowsMode, vsync, resolution, graphicsQuality;
    public Slider brightness;
    public Localisation localisation;

    private void Awake()
    {
        if (PlayerPrefs.GetString("Language") == string.Empty || PlayerPrefs.GetString("Language") == null)
            SetOptimalSettings();
        else
            LoadSettings();
    }

    public void SetOptimalSettings()
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
        
        SetSettings();
    }

    public void LoadSettings()
    {
        settings.general.Language = PlayerPrefs.GetString("Language");
        SetSettings();
    }

     public void SetSettings()
     {
        localisation.Language = settings.general.Language;
        Language.value = settings.general.Language == "Русский" ? 1 : 0;

        PlayerPrefs.SetString("Language", settings.general.Language);
     }

    public void SetSettingWindowNum(int num)
    {
        currentSettingWindow = num;
    }

    void UpdateSettings()
    {
        settings.general.Language = Language.options[Language.value].text;
    }

  //  IEnumerator GlitchTransaction
   // {
//
  //  }
	
	// Update is called once per frame
	void Update () {
        UpdateSettings();
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
        Buttons.Add(new Button("RightArrow", KeyCode.RightArrow));
        Buttons.Add(new Button("LeftArrow", KeyCode.LeftArrow));
    }

    public KeyCode GetButton(string keycode)
    {
        return Buttons.Find(x => x.name == keycode).key;
    }
}