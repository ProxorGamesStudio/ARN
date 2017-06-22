using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.Text;
using System;
using UnityEngine.UI;

public class Localisation : MonoBehaviour
{
    [System.Serializable]
    public struct Word
    {
        public string id;
        public string[] localiztionStrings;
    }
    public string Language;
    byte currentLanguage;
    List<string> lang_ids = new List<string>();
    public List<Word> words = new List<Word>();
    public UnityEngine.Object LocalizationDB;
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
                string filePath = AssetDatabase.GetAssetPath(LocalizationDB);
        string fileName = Path.GetFileNameWithoutExtension(filePath);
        using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            IWorkbook book = null;
            if (Path.GetExtension(filePath) == ".xls")
            {
                book = new HSSFWorkbook(stream);
            }
            else {
                book = new XSSFWorkbook(stream);
            }
            ISheet sheet = book.GetSheetAt(0);
            for(byte i = 1; sheet.GetRow(0).GetCell(i).ToString() != "Описание"; i++)
                lang_ids.Add(sheet.GetRow(2).GetCell(i).ToString());
            for (int i = 3, k = 0; sheet.GetRow(i) != null && sheet.GetRow(i).GetCell(0).ToString() != string.Empty; i++)
            {
                Word word = new Word();
                word.id = sheet.GetRow(i).GetCell(0).ToString();
                word.localiztionStrings = new string[lang_ids.Count];
                for (k = 0; k < lang_ids.Count; k++)
                    word.localiztionStrings[k] = sheet.GetRow(i).GetCell(1 + k).ToString();
                words.Add(word);
            }
        }
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


