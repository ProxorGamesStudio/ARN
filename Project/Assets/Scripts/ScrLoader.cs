using UnityEngine;
using System.Collections.Generic;
using SQLite;
using UnityEngine.UI;

public class GlobalParametrsDB
{
    public string id { get; set; }
    public int ThreatAdminKillTrojan { get; set; }
    public int ThreatFailSetTrojan { get; set; }
    public int ThreatSetTrojan { get; set; }
    public int ThreatTrojanWork { get; set; }
    public int ThreatServCooldown { get; set; }
    public int ThreatEnemyAIAttack { get; set; }
    public int EventLiveTimeSecond { get; set; }
    public int EventTimeToNewMin { get; set; }
    public int EventTimeToNewMax { get; set; }
    public int EventStartCountPositivEventMin { get; set; }
    public int EventStartCountPositivEventMax { get; set; }
}



public class ScrLoader : MonoBehaviour
{

    public static List<int> StringToArrayInt(string s)
    {
        List<int> ri = new List<int>(0);
        string sb = s;
        while (sb.Length > 0)
        {
            string si = "";
            while (sb[0] != ' ')
            {
                si += sb[0];
                sb = sb.Remove(0, 1);
                if (sb.Length == 0)
                    break;
            }
            if (sb.Length > 0)
                sb = sb.Remove(0, 1);

            ri.Add(int.Parse(si));
        }
        return ri;
    }

    public static List<float> StringToArrayFloat(string s)
    {
        List<float> rf = new List<float>(0);
        string sb = s;
        while (sb.Length > 0)
        {
            string sf = "";
            while (sb[0] != ' ')
            {
                sf += sb[0];
                sb = sb.Remove(0, 1);
                if (sb.Length == 0)
                    break;
            }
            if (sb.Length > 0)
                sb = sb.Remove(0, 1);

            rf.Add(float.Parse(sf));
        }
        return rf;
    }

    public static List<string> StringToArrayString(string s)
    {
        List<string> rs = new List<string>(0);
        string sb = s;
        while (sb.Length > 0)
        {
            string ss = "";
            while (sb[0] != ' ')
            {
                ss += sb[0];
                sb = sb.Remove(0, 1);
                if (sb.Length == 0)
                    break;
            }
            if (sb.Length > 0)
                sb = sb.Remove(0, 1);

            rs.Add(ss);
        }
        return rs;
    }

    public void CreateSQLdb()//Одноразовая функция, для создания базы данных.
    {
        var db = new SQLiteConnection(Application.dataPath + "/Resources/DataBase.db");

        db.CreateTable<GlobalParametrsDB>();

        db.Dispose();
    }


    public static void LoadDB()
    {
        var db = new SQLiteConnection(Application.dataPath + "/DataBase.db");

        //List<GlobalParametrsDB> globalParametrs = new List<GlobalParametrsDB>(db.Table<GlobalParametrsDB>());
        var globalParametrs = db.Table<GlobalParametrsDB>();
        if (globalParametrs != null)
        {
           // GameObject.Find("Testing").GetComponent<Text>().text += " He" + globalParametrs.ElementAt(0).ThreatTrojanWork;
            ScrAlertSystem.Inst.TFSAlert = globalParametrs.First().ThreatAdminKillTrojan;
            ScrAlertSystem.Inst.USDTAlert = globalParametrs.First().ThreatFailSetTrojan;
            ScrAlertSystem.Inst.HGAlert = globalParametrs.First().ThreatSetTrojan;
            ScrAlertSystem.Inst.TGAlert = globalParametrs.First().ThreatTrojanWork;
            ScrAlertSystem.Inst.CDSAlert = globalParametrs.First().ThreatServCooldown;
            ScrAlertSystem.Inst.EUHAlert = globalParametrs.First().ThreatEnemyAIAttack;

            ScrEventController.inst.EventLifeTime = globalParametrs.First().EventLiveTimeSecond;
            ScrEventController.inst.EventTimerMax = globalParametrs.First().EventTimeToNewMax;
            ScrEventController.inst.EventTimerMin = globalParametrs.First().EventTimeToNewMin;
            ScrEventController.inst.MinPosEv = globalParametrs.First().EventStartCountPositivEventMin;
            ScrEventController.inst.MaxPosEv = globalParametrs.First().EventStartCountPositivEventMax;
        }



        db.Dispose();
    }

    void Start () {
        //загрузка параметров из базы данных
        //GameObject.Find("Testing").GetComponent<Text>().text += "Here: ";
        LoadDB();
        ScrAI.inst.LoadDB();
        ScrEAI.inst.LoadDB();
        ScrGameGenerator.inst.LoadDB();
        ScrNetController.inst.LoadDB();
        ScrTrojanController.inst.LoadDB();
        ScrNewsSystem.inst.LoadDB();

        //загрузка локализации
        ScrLocalizationSystem.inst.LoadDB();
        ScrLocalizationSystem.inst.AfterLoadDB();

        ScrVirusController.inst.LoadDB();
        ScrVirusKonstructor.inst.LoadDB();

        //постзагрузка и генерация игры
        ScrEAI.inst.AfterLoadDB();
        //ScrEventController.inst.AfterLoadDB();
        ScrGameGenerator.inst.AfterLoadDB();
        ScrGlobalTimeManager.Instance.AfterLoadDB();
        ScrAlertSystem.Inst.AfterLoadDB();
        ScrResesController.inst.AfterLoadDB();
        ScrTrojanController.inst.AfterLoadDB();

        //подготовка интерфейса
        ScrUIController.inst.AfterLoadDB();
    }

    void Update()
    {

    }
}
