using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using ProxorGamesLocalisation;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Options : MonoBehaviour {

    [HideInInspector]
    public int currentSettingWindow;
    public Settings settings = new Settings();
    [Header("General")]
    public Dropdown Language;
    public Dropdown DeficultDefaultLevel;

    [Header("Controller")]
    public Slider mouseSensivity;
    public GameObject ContentPrefab;
    public Transform linkedTo;
    GameObject[] Controls;
    [HideInInspector]
    public bool controlSelect;

    [Header("Sound")]
    public Slider Total;
    public Slider Music, Effects, Sound, Micro;
    public AudioSource Source;

    [Header("Video")]
    public Slider brightness;
    public Dropdown windowsMode, vsync, resolution, graphicsQuality;
    public ProxorBrightness PB;

    [Header("Other")]
    public Localisation localisation;
    public GameManager GM;
    public TheCamera cam;

    private void Awake()
    {
        Controls = new GameObject[settings.controls.controls.Buttons.Count];
        settings.video.resolutions.Add(new Settings.Video.Resolution(Screen.width, Screen.height));
        for (int i = 0; i < settings.video.resolutions.Count; i++)
            resolution.options.Add(new Dropdown.OptionData(settings.video.resolutions[i].x + "x" + settings.video.resolutions[i].y));
        resolution.value = resolution.options.Count - 1;
        for (int i = 0; i < settings.controls.controls.Buttons.Count; i++)
        {
            Controls[i] = Instantiate(ContentPrefab, linkedTo) as GameObject;
            Controls[i].GetComponentInChildren<CallbackControls>().options = this;
            Controls[i].GetComponentInChildren<CallbackControls>().n = i;
            Controls[i].GetComponentInChildren<CallbackControls>().localisation = localisation;
        }
        if (!File.Exists(Application.dataPath + "/settings" + ".sav"))
        {
            FileStream file = File.Create(Application.dataPath + "/settings" + ".sav");
            file.Close();
            SetOptimalSettings();
        }
        else
            LoadSettings();
       
    }

    public void SetOptimalSettings()
    {
        settings.general.Language = Application.systemLanguage.ToString() == "Russian"? "Русский" : "English";
        settings.general.DefucultyLevel = 1;

        settings.video = new Settings.Video(SystemInfo.graphicsMemorySize >= 1000 ? 2 : SystemInfo.graphicsMemorySize >= 500 ? 1 : 0);


        settings.sound.EffectsVolume = 1;
        settings.sound.GeneralVolume = 1;
        settings.sound.MusicVolume = 1;
        settings.sound.Michrophone = 1;
        settings.sound.SoundChat = 1;

        settings.controls.controls = new Controller();
        settings.controls.controls.MouseSensivity = 1f;
        
        SetSettings();
    }

    public void LoadSettings()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath + "/settings" + ".sav", FileMode.Open, FileAccess.Read);
        settings = (Settings)bf.Deserialize(file);
        file.Close();
        SetSettings();
    }

     public void SetSettings()
     {
        localisation.Language = settings.general.Language;
        Language.value = settings.general.Language == "Русский" ? 1 : 0;
        GM.Defucult = settings.general.DefucultyLevel;
        DeficultDefaultLevel.value = settings.general.DefucultyLevel; 

        cam.sensivity = mouseSensivity.value = settings.controls.controls.MouseSensivity;


        
        Total.value = settings.sound.GeneralVolume;
        Music.value = settings.sound.MusicVolume;
        Effects.value = settings.sound.EffectsVolume;
        Sound.value = settings.sound.SoundChat;
        Micro.value = settings.sound.Michrophone;

        resolution.value = settings.video.nowResolution;
        windowsMode.value = settings.video.WindowMode;
        Screen.SetResolution(settings.video.resolutions[settings.video.nowResolution].x, settings.video.resolutions[settings.video.nowResolution].y, settings.video.WindowMode == 0? true : false);
        QualitySettings.SetQualityLevel(settings.video.GraphicQuality, true);
        Application.targetFrameRate = settings.video.Vsync? 0 : -1;
        brightness.value = PB._Brightness = settings.video.brightness;
        graphicsQuality.value = settings.video.GraphicQuality;
        vsync.value = settings.video.Vsync ? 1 : 0;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath + "/settings" + ".sav", FileMode.Open, FileAccess.ReadWrite);
        bf.Serialize(file, settings);
        file.Close();
    }

    public void SetSettingWindowNum(int num)
    {
        currentSettingWindow = num;
    }

    void UpdateSettings()
    {

        settings.general.Language = Language.options[Language.value].text;
        settings.general.DefucultyLevel = DeficultDefaultLevel.value;

        settings.controls.controls.MouseSensivity = mouseSensivity.value;

        settings.sound.GeneralVolume = Total.value;
        settings.sound.MusicVolume = Music.value;
        settings.sound.EffectsVolume = Effects.value;
        settings.sound.SoundChat = Sound.value;
        settings.sound.Michrophone = Micro.value;

        settings.video.nowResolution = resolution.value;
        settings.video.brightness = brightness.value;
        settings.video.WindowMode = windowsMode.value;
        settings.video.Vsync = vsync.value == 0 ? true : false;
        settings.video.GraphicQuality = graphicsQuality.value;

        Source.volume = settings.sound.MusicVolume;
        Source.outputAudioMixerGroup.audioMixer.SetFloat("Volume", settings.sound.GeneralVolume);

        PB._Brightness = settings.video.brightness;

        for (int i = 0; i < Controls.Length; i++)
        {
            Controls[i].GetComponentsInChildren<Text>()[1].text = settings.controls.controls.Buttons[i].key.ToString();
            Controls[i].GetComponentsInChildren<Text>()[0].text = localisation.GetLocalizationString(settings.controls.controls.Buttons[i].lKey);
        }

    }

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
        public int DefucultyLevel;
    }
    [Serializable]
    public struct Video
    {
        [Serializable]
        public struct Resolution
        {
            public int x, y;

            public Resolution(int _x, int _y)
            {
                x = _x;
                y = _y;
            }
        }

        public List<Resolution> resolutions;
        public int WindowMode, GraphicQuality;
        public float brightness;
        public bool Vsync;
        public int nowResolution;

        public Video(int GQulity)
        {
            WindowMode = 1;
            GraphicQuality = GQulity;
            brightness = 1;
            Vsync = true;
            resolutions = new List<Resolution>();
            resolutions.Add(new Resolution(1366, 768));
            resolutions.Add(new Resolution(1920, 1080));
            resolutions.Add(new Resolution(1280, 1024));
            resolutions.Add(new Resolution(1600, 900));
            resolutions.Add(new Resolution(1680, 1050));
            resolutions.Add(new Resolution(1280, 800));
            resolutions.Add(new Resolution(1280, 720));
            nowResolution = 0;
        }
    }

    [Serializable]
    public struct Sound
    {
        public float GeneralVolume, MusicVolume, EffectsVolume, SoundChat, Michrophone;
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
        public string name, lKey;
        public KeyCode key;

        public Button(string _name, string _lKey, KeyCode k)
        {
            name = _name;
            lKey = _lKey;
            key = k;
        }
    }

    public float MouseSensivity;
    public List<Button> Buttons;

    public Controller()
    {
        Buttons = new List<Button>();
        Buttons.Add(new Button("Enter", "#lbl_enter", KeyCode.Return));
        Buttons.Add(new Button("Escape", "#lbl_escape", KeyCode.Escape));
        Buttons.Add(new Button("Mouse 1", "#lbl_mouse_1", KeyCode.Mouse0));
        Buttons.Add(new Button("Mouse 2", "#lbl_mouse_2", KeyCode.Mouse1));
        Buttons.Add(new Button("LeftArrow", "#lbl_arrow_left", KeyCode.LeftArrow));
        Buttons.Add(new Button("RightArrow", "#lbl_arrow_right", KeyCode.RightArrow));
    }

    public KeyCode GetButton(string keycode)
    {
        return Buttons.Find(x => x.name == keycode).key;
    }
}