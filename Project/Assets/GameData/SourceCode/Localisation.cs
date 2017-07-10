using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.Text;
using System;
using UnityEngine.UI;

namespace ProxorGamesLocalisation
{
    public class Localisation : MonoBehaviour
    {
        [System.Serializable]
        public struct Word
        {
            public string id;
            public string[] localiztionStrings;
        }

        [HideInInspector]
        public string Language;
        byte currentLanguage;
        [HideInInspector]
        public List<string> lang_ids = new List<string>();
        [HideInInspector]
        public List<Word> words = new List<Word>();
        Text[] texts;
        List<Text> Texts = new List<Text>();
        List<string> Text_data = new List<string>();


        public void Awake()
        {
            texts = Resources.FindObjectsOfTypeAll<Text>();
            for (int i = 0; i < texts.Length; i++)
                if (texts[i].text.Length > 0 && texts[i].text.Trim()[0] == '#')
                    Texts.Add(texts[i]);
            texts = null;

            for (byte i = 0; i < lang_ids.Count; i++)
                if (Language == lang_ids[i])
                    currentLanguage = i;
            for (int i = 0; i < Texts.Count; i++)
            {
                Texts[i].gameObject.name = Texts[i].text;
                Texts[i].text = GetLocalizationString(Texts[i].text);
            }
        }

        public void Update()
        {
            for (byte i = 0; i < lang_ids.Count; i++)
                if (Language == lang_ids[i])
                    if (currentLanguage != i)
                    {
                        currentLanguage = i;
                        ChangeLanguage();
                    }
        }

        public void ChangeLanguage()
        {
            for (int i = 0; i < Texts.Count; i++)
                Texts[i].text = GetLocalizationString(Texts[i].gameObject.name);
        }

        public string GetLocalizationString(string id)
        {

            int index = words.FindIndex(x => x.id.Trim() == id.Trim());
            if (index >= 0)
                return words[index].localiztionStrings[currentLanguage];
            else
                return id;
        }

    }

}
