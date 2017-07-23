using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.Model;
using System.Collections.Generic;
using System.Text;
using System;
using UnityEngine.UI;
using ProxorGamesLocalisation;

[Serializable]
public class LocalisationTool4 : EditorWindow
{
    [SerializeField]
    public UnityEngine.Object importDB;
    [SerializeField]
    public Localisation localisation;
    Vector2 scrollPos = new Vector2(-1, -1);
    bool[] checkBox;
    string exportPath = string.Empty;
    Text[] texts;

    [MenuItem("Tools/Localisation")]
    public static void Init()
    {
        var window = EditorWindow.GetWindow(typeof(LocalisationTool4));
        window.position = new Rect(200, 150, 1000, 870);
        window.ShowUtility();
        window.title = "Localisation";
    }

    void OnGUI()
    {
        
        GUILayout.BeginVertical();
        GUI.skin.label.fontSize = 20;
        GUILayout.Label("Proxor Localisation System v1.1");
        GUI.skin.label.fontSize = 12;
        if (GUILayout.Button("Clear"))
            Clear();
        if (!localisation)
        {
            localisation = FindObjectOfType<Localisation>();
            GUILayout.Label("Couldn't find Localisation script at this scene!");
        }
        else
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Import") && importDB != null)
                Import();
            importDB = EditorGUILayout.ObjectField(importDB, typeof(UnityEngine.Object), true);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (exportPath == string.Empty)
                exportPath = Application.dataPath;
            if (GUILayout.Button("Export") && exportPath != string.Empty)
                Export();
            exportPath = EditorGUILayout.TextField(exportPath);
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUI.contentColor = Color.red;
            GUILayout.Label("Selected", GUILayout.Width(80));
            GUILayout.Label("Id", GUILayout.Width(300));
            for(int i = 0; i < localisation.lang_ids.Count; i++)
                GUILayout.Label(localisation.lang_ids[i], GUILayout.Width(300));
            GUI.contentColor = Color.black;
            GUILayout.EndHorizontal();

            if (scrollPos == new Vector2(-1, -1))
                scrollPos = new Vector2(0, 15 * localisation.words.Count);
            Array.Resize(ref checkBox, localisation.words.Count);
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(1000), GUILayout.Height(700));
            for (int i = 0; i < localisation.words.Count; i++)
            {
                Localisation.Word word;
                GUILayout.BeginHorizontal();
                checkBox[i] = GUILayout.Toggle(checkBox[i], "");
                word.id = EditorGUILayout.TextField(localisation.words[i].id, GUILayout.Width(300));
                word.localiztionStrings = new string[localisation.lang_ids.Count];
                for (int k = 0; k < localisation.lang_ids.Count; k++)
                    word.localiztionStrings[k] = EditorGUILayout.TextField(localisation.words[i].localiztionStrings[k], GUILayout.Width(300));
                localisation.words[i] = word;
                GUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Add"))
                Add("#");
            if (GUILayout.Button("Find"))
                Find();
            if (GUILayout.Button("Delete Selected"))
                Delete();
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
    }

    void Delete()
    {
        List<Localisation.Word> temp = new List<Localisation.Word>();
        for (int i = 0; i < localisation.words.Count; i++)
        {
            if (checkBox[i])
                temp.Add(localisation.words[i]);
        }
        for (int i = 0; i < temp.Count; i++)
            localisation.words.Remove(temp[i]);
        checkBox = new bool[localisation.words.Count];
    }

    void Find()
    {
        texts = Resources.FindObjectsOfTypeAll<Text>();
        for (int i = 0; i < texts.Length; i++)
            if (texts[i].text.Length > 0 && texts[i].text.Trim()[0] == '#')
            {
                for (int k = 0; k < localisation.words.Count; k++)
                    if (texts[i].text.Trim() == localisation.words[k].id.Trim())
                        return;
                Add(texts[i].text.Trim());
            }
    }

    void Add(string id)
    {
        Localisation.Word word = new Localisation.Word();
        word.id = id;
        word.localiztionStrings = new string[localisation.lang_ids.Count];
        for (int i = 0; i < localisation.lang_ids.Count; i++)
            word.localiztionStrings[i] = "";
        localisation.words.Add(word);
        scrollPos += new Vector2(0, 15);
        Array.Resize(ref checkBox, localisation.words.Count);
    }

    void Clear()
    {
        localisation.words = new List<Localisation.Word>();
        localisation.lang_ids = new List<string>();
    }
    void Import()
    {
        Clear();
        string filePath = AssetDatabase.GetAssetPath(importDB);
        
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                IWorkbook book = null;
                if (Path.GetExtension(filePath) == ".xls")
                {
                    book = new HSSFWorkbook(stream);
                }
                else
                {
                    book = new XSSFWorkbook(stream);
                }
                ISheet sheet = book.GetSheetAt(0);
                for (byte i = 1; sheet.GetRow(0).GetCell(i).ToString() != "Описание"; i++)
                    localisation.lang_ids.Add(sheet.GetRow(2).GetCell(i).ToString());
                for (int i = 3, k = 0; sheet.GetRow(i) != null && sheet.GetRow(i).GetCell(0).ToString() != string.Empty; i++)
                {
                    ProxorGamesLocalisation.Localisation.Word word = new ProxorGamesLocalisation.Localisation.Word();
                    word.id = sheet.GetRow(i).GetCell(0).ToString();
                    word.localiztionStrings = new string[localisation.lang_ids.Count];
                    for (k = 0; k < localisation.lang_ids.Count; k++)
                        word.localiztionStrings[k] = sheet.GetRow(i).GetCell(1 + k).ToString();
                    localisation.words.Add(word);
                }
            }
    }

    void Export()
    {
        HSSFWorkbook wb;
        HSSFSheet sh;
        // create xls if not exists

            wb = HSSFWorkbook.Create(InternalWorkbook.CreateWorkbook());

            // create sheet
            sh = (HSSFSheet)wb.CreateSheet("Sheet1");
        // 3 rows, 2 columns
        var r = sh.CreateRow(0);
        r.CreateCell(0);
        r.Cells[0].SetCellValue("Id");

        for (int i = 1; i < localisation.lang_ids.Count + 1; i++)
            r.CreateCell(i);   
        r.CreateCell(localisation.lang_ids.Count + 1);
        r.Cells[localisation.lang_ids.Count + 1].SetCellValue("Описание");
         r = sh.CreateRow(1);
        r = sh.CreateRow(2);
        r.CreateCell(0);
        for (int i = 0; i < localisation.lang_ids.Count; i++)
           {
              r.CreateCell(1 + i);
              r.Cells[1 + i].SetCellValue(localisation.lang_ids[i]);
           }

         for (int i = 3; i < localisation.words.Count + 3; i++)
          {
              r = sh.CreateRow(i);
              r.CreateCell(0);
              r.Cells[0].SetCellValue(localisation.words[i - 3].id);
              for(int k = 1; k < localisation.lang_ids.Count + 1; k++)
              {
                  r.CreateCell(k);
                  r.Cells[k].SetCellValue(localisation.words[i - 3].localiztionStrings[k-1]);
              }
          }


        using (var fs = new FileStream(exportPath + "/LocalisationDB_Exported.xls", FileMode.Create, FileAccess.Write))
            {
                wb.Write(fs);
            }
        AssetDatabase.Refresh();
    }

}

