using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScrAlertSystem : MonoBehaviour
{
    public static ScrAlertSystem Inst;

    public float StartAlertValue;
    public int MaxAlertValue;
    public int StartAlertLvl;
    public int MaxAlertLvl;

    public int HGAlert = 5;//Угроза за захват узла
    public int TGAlert = 1;//Угрза в секунду от работы трояна
    public float CDSAlert = 1;//Угроза от кулдауна сервера
    public int USDTAlert = 10;//Угроза при нештатном завершении работы трояна
    public int TFSAlert = 20;//Угроза от провальной установки трояна
    public int EUHAlert = -2;//Угроза при атаки ВИИ на наш узел

    public float AlertValue;
    private int AlertLvl;

    void Awake()
    {
        Inst = this;
    }

    public void AfterLoadDB()
    {
        AlertValue = StartAlertValue;
        AlertLvl = StartAlertLvl;
    }

    private void Update()
    {
        if(AlertValue >= MaxAlertValue && AlertLvl < MaxAlertLvl)
        {
            AlertLvl++;
            ScrNetController.inst.ThreatLvlvUp();
            if (AlertLvl < MaxAlertLvl)
            {
                ScrUILog.inst.AddMessage(ScrLocalizationSystem.L_msgThreatLvlUp);
                //ScrUIMessanger.inst.ShowMessage(ScrLocalizationSystem.L_msgThreatLvlUp);
                AlertValue -= MaxAlertValue;
            }
            else
            {
                ScrUILog.inst.AddMessage(ScrLocalizationSystem.L_msgThreatLvlMax);

                //ScrUIMessanger.inst.ShowMessage(ScrLocalizationSystem.L_msgThreatLvlMax);
            }
            ScrNewsSystem.inst.HapEvent("threat_lvl_up",AlertLvl.ToString());
        }
    }

    public void AddAlert(float AV)
    {
        if (AlertLvl < MaxAlertLvl)
            AlertValue += AV;
    }

    public float GetAlert()
    {
        return AlertValue;
    }

    public int GetAlertLvl()
    {
        return AlertLvl;
    }

    public float GetAllAlert()
    {
        return (AlertLvl * MaxAlertValue) + AlertValue;
    }
}
