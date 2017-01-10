using UnityEngine;
using System.Collections.Generic;

using SQLite;

[System.Serializable]
public class TrojanDatabase
{
    [PrimaryKey, Unique]
    //свойства троянов
    public string Id { get; set; }
    public int Type { get; set; }
    public int Lvl { get; set; }
    public float LifeTime { get; set; }
    public float ThreatPSecond { get; set; }
    public string CostIdRes { get; set; }
    public string CostCountRes { get; set; }

    //кирпичики троянов
    public float trRes { get; set; }
    public float tr_found { get; set; }
    public float tr_trick { get; set; }
    public float tr_hook { get; set; }
    public float tr_time { get; set; }
    public float tr_threat { get; set; }

}

[System.Serializable]
public class TrojanItem
{
    public string Id;
    public int Type;
    public int Lvl;
    public float LifeTime;
    public List<GameResStor> Cost;

    //кирпичики
    public float trRes;
    public float tr_found;
    public float tr_trick;
    public float tr_hook;
    public float tr_time;
    public float tr_threat;

    public TrojanItem(/*TrojanDatabase t*/)
    {/*
        Id = t.Id;
        Type = t.Type;
        Lvl = t.Lvl;
        LifeTime = t.LifeTime;
        Cost = new List<GameResStor>();
        trRes = t.trRes;
        tr_found = t.tr_found;
        tr_trick = t.tr_trick;
        tr_hook = t.tr_hook;
        tr_time = t.tr_time;
        tr_threat = t.tr_threat;
      //  Debug.LogError("Test");
        string reses = t.CostIdRes;
        while (reses.Length > 0)
        {
            string s = "";
            while (reses[0] != ' ')
            {
                s += reses[0];
                reses = reses.Remove(0, 1);
                if (reses.Length == 0)
                    break;
            }
            if (reses.Length > 0)
                reses = reses.Remove(0, 1);
            GameResStor r = new GameResStor();
            r.Id = s;
            Cost.Add(r);
        }
        reses = t.CostCountRes;
        int c = 0;
        while (reses.Length > 0)
        {
            string s = "";
            while (reses[0] != ' ')
            {
                s += reses[0];
                reses = reses.Remove(0, 1);
                if (reses.Length == 0)
                    break;
            }
            if (reses.Length > 0)
                reses = reses.Remove(0, 1);
            Cost[c].count = int.Parse(s);
            c++;
    }*/
    }
}

[System.Serializable]
public class TrojanInServer
{
    public string Id;
    public int Type;
    public int Lvl;
    public float MaxLifeTime;
    public float LifeTime;

    //кирпичики
    public float trRes;
    public float tr_found;
    public float tr_trick;
    public float tr_hook;
    public float tr_time;
    public float tr_threat;

    public TrojanInServer()
    {
        Id = "non";
        Type = -1;
        Lvl = -1;
        MaxLifeTime = 0;
        LifeTime = 0;
        trRes = 0;
    }

    public TrojanInServer(TrojanItem t)
    {
        trRes = t.trRes;
        tr_found = t.tr_found;
        tr_trick = t.tr_trick;
        tr_hook = t.tr_hook;
        tr_time = t.tr_time;
        tr_threat = t.tr_threat;
        Id = t.Id;
        Type = t.Type;
        Lvl = t.Lvl;
        MaxLifeTime = t.LifeTime;
        LifeTime = 0;

    }
}

public class ScrTrojanController : MonoBehaviour {

    public static ScrTrojanController inst;

    public int MaxTrojanCount = 10;

    public List<TrojanItem> trojans = new List<TrojanItem>();

    public ScrUITrojanList UI;

    private int TrojanCount;

    public void CreateSQLdb()//Одноразовая функция, для создания базы данных.
    {
        var db = new SQLiteConnection(Application.dataPath + "/DataBase.db");

        db.CreateTable<TrojanDatabase>();

        db.Dispose();
    }
    
    void Awake()
    {
        CreateSQLdb();
        inst = this;
    }

    public void LoadDB()
    {
        var db = new SQLiteConnection(Application.dataPath + "/DataBase.db");

        var TDB = db.Table<TrojanDatabase>();

        for (int i = 0; i < TDB.Count(); i++)
        {
            trojans.Add(new TrojanItem());
            trojans[i].Id = TDB.ElementAt(i).Id;
            trojans[i].Type = TDB.ElementAt(i).Type;
            trojans[i].Lvl = TDB.ElementAt(i).Lvl;
            trojans[i].LifeTime = TDB.ElementAt(i).LifeTime;
            trojans[i].Cost = new List<GameResStor>();
            trojans[i].trRes = TDB.ElementAt(i).trRes;
            trojans[i].tr_found = TDB.ElementAt(i).tr_found;
            trojans[i].tr_trick = TDB.ElementAt(i).tr_trick;
            trojans[i].tr_hook = TDB.ElementAt(i).tr_hook;
            trojans[i].tr_time = TDB.ElementAt(i).tr_time;
            trojans[i].tr_threat = TDB.ElementAt(i).tr_threat;
        }
        db.Dispose();
    }

    public void AfterLoadDB()
    {
        UI.SetTrojans();
        TrojanCount = 0;
    }

    public int GetCount()
    {
        return TrojanCount;
    }
	
    public bool AddTrojan()
    {
        if(TrojanCount<MaxTrojanCount)
        {
            TrojanCount++;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DelTrojan()
    {
        TrojanCount--;
    }

	void Update () {
	}
}
