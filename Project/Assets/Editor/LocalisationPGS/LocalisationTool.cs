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

public class LocalisationTool : EditorWindow
{
    [MenuItem("Tools/Localisation")]
    public static void Init()
    {
        var window = EditorWindow.GetWindow(typeof(LocalisationTool));
        window.position = new Rect(Screen.width/2, Screen.height/2, 400,200);
        window.title = "Localisation";
    }

    void OnGUI()
    {
        
    }
}

