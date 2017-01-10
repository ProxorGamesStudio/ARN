using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
Читы для разработки
*/
public class Cheats : MonoBehaviour {

    public GameObject Panel;
    public InputField text;
    string _code;
    ScrUINewsMessage news;
    ScrUILog log;

    void Awake()
    {
        news = FindObjectOfType<ScrUINewsMessage>();
        log = FindObjectOfType<ScrUILog>();
    }

    public void Start()
    {
        
    }

	void Update()
    {
        if (Input.GetKeyDown("`"))
            Panel.SetActive(!Panel.activeInHierarchy);
    }



    public void Use()
    {
        _code = text.text;
        string[] CODE = _code.Split('.');
        if(CODE.Length > 0)
        switch (CODE[0])
        {
                case "News":
                    if(CODE.Length > 1)
                    switch (CODE[1])
                    {
                        case "Show":
                            news.ShowPanel();
                            break;

                        case "Hide":
                            news.HidePanel();
                            break;
                    }
                    break;

                case "Hub":
                    if (CODE.Length > 1)
                        switch (CODE[1])
                        {
                            case "ScanAll":
                                ScanAllHubs();
                                break;

                            case "GrubAll":
                                GrabAllHubs();
                                break;
                        }
                    break;

                case "Log":
                    if (CODE.Length > 1)
                    {
                        string[] comand = CODE[1].Split('(', '"', ')','=');
                         if(comand.Length > 0)
                         switch (comand[0])
                         {
                               case "SendMassage":
                                     if (comand.Length > 4)
                                        for(int i = 0; i  < System.Convert.ToInt32(comand[comand.Length - 1].Split(' ')[1]); i++)
                                          log.AddMessage(comand[2]);
                                   break;
                                case "Clear":
                                    foreach (GameObject go in GameObject.FindGameObjectsWithTag("Log"))
                                        DestroyImmediate(go);
                                    break;
                            }
                    }
                    break;
        }
        text.text = "";
    }

    /*Чит - просканировать все узлы*/
    public void ScanAllHubs()
    {
        ScrHub[] hubs = GameObject.FindObjectsOfType<ScrHub>();
        foreach(var h in hubs)
        {
            h.Scan();
        }
    }

    /*Чит - захватить все узлы*/
    public void GrabAllHubs()
    {
        ScrHub[] hubs = GameObject.FindObjectsOfType<ScrHub>();
        foreach (var h in hubs)
        {
            h.Grab();
        }
    }
}
