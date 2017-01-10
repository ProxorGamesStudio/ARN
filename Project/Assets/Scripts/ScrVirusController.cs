using UnityEngine;
using System.Collections.Generic;
using SQLite;

public class VirusDB
{
    public string id { get; set; }
    public int GrabTime { get; set; }
    public float AntivirusCrackChance { get; set; }
    public float FirewallCrackChance { get; set; }
    public float AdmincontrolCrackChance { get; set; }
    public string CostId { get; set; }
    public string CostCount { get; set; }
    public int Threat { get; set; }
    public float Disquise { get; set; }
    public int AddGrabHub { get; set; }

    //кирпичики вирусов

    public int v_worm { get; set; }
    public int v_worm_HC = 1;
    public int v_low_threat { get; set; }
    public int v_cloack { get; set; }
    public int v_rootkit { get; set;  }
    public int v_copy { get; set; }
    public int v_destroyer { get; set; }
    public int v_vandal_bomb { get; set; }
    public int v_vandal_distract { get; set; }
    public int v_res { get; set; }
}

[System.Serializable]
public class Virus
{
    public string id;
    public string name;
    public float GrabTime;
    public float AntivirusCrackChance;
    public float FirewallCrackChance;
    public float AdmincontrolCrackChance;
    public List<GameResStor> Cost;
    public float GrabThreat;
    public float Disquise;
    public int AddGrabHub;
    public List<string> Pieces;


    public int v_worm;
    public int v_worm_HC;
    public int v_low_threat;
    public int v_cloack;
    public int v_rootkit;
    public int v_copy;
    public int v_destroyer;
    public int v_vandal_bomb;
    public int v_vandal_distract;
    public int v_res;

    public Virus()
    {
        id = "non";
        name = "";
        GrabTime = 0;
        GrabThreat = 0;
        AntivirusCrackChance = 0;
        FirewallCrackChance = 0;
        AdmincontrolCrackChance = 0;
        Cost = new List<GameResStor>();
        Disquise = 0;
        AddGrabHub = 0;
        Pieces = new List<string>();
    }

    public Virus(VirusDB db)
    {
        id = db.id;
        name = ScrLocalizationSystem.GetLocalString(id);
        GrabTime = db.GrabTime;
        GrabThreat = db.Threat;
        AntivirusCrackChance = db.AntivirusCrackChance;
        FirewallCrackChance = db.FirewallCrackChance;
        AdmincontrolCrackChance = db.AdmincontrolCrackChance;
        
        List<string> bs = ScrLoader.StringToArrayString(db.CostId);
        List<int> bi1 = ScrLoader.StringToArrayInt(db.CostCount);
        int c = 0;
        Cost = new List<GameResStor>();
        foreach (var r in bs)
        {
            Cost.Add(new GameResStor(r, bi1[c]));
            c++;
        }

        Disquise = db.Disquise;
        AddGrabHub = db.AddGrabHub;
        Pieces = new List<string>();


        v_worm = db.v_worm;
        v_worm_HC = db.v_worm_HC;
        v_low_threat = db.v_low_threat;
        v_cloack = db.v_cloack;
        v_rootkit = db.v_rootkit;
        v_copy = db.v_copy;
        v_destroyer = db.v_destroyer;
        v_vandal_bomb = db.v_vandal_bomb;
        v_vandal_distract = db.v_vandal_distract;
        v_res = db.v_res;
}

    public static bool operator == (Virus v1, Virus v2)
    {
        if(v1.Pieces.Count != v2.Pieces.Count)
        {
            return false;
        }else if(v1.Pieces.Count == 0 && v2.Pieces.Count == 0)
        {
            return true;
        }
        List<string> p1 = v1.Pieces;
        foreach(var p2 in v2.Pieces)
        {
            if(p1.Contains(p2))
            {
                p1.Remove(p2);
            }
        }
        if(p1.Count > 0)
        {
            return false;
        }

        return true;
    }

    public static bool operator !=(Virus v1, Virus v2)
    {
        if(v1 == v2)
        {
            return false;
        }else
        {
            return true;
        }
    }
}

public class ScrVirusController : MonoBehaviour {
    public static ScrVirusController inst;

    public List<Virus> VirusBase;

    public List<string> DeactivatedViruses;

    void CreateSQLdb()//Одноразовая функция, для создания базы данных.
    {
        var db = new SQLiteConnection(Application.dataPath + "/DataBase.db");

        db.CreateTable<VirusDB>();

        db.Dispose();
    }

    void Awake () {
        inst = this;
        DeactivatedViruses = new List<string>();
        CreateSQLdb();

    }
	
    public void LoadDB()
    {
        var db = new SQLiteConnection(Application.dataPath + "/DataBase.db");

        var vDB = db.Table<VirusDB>();

        VirusBase = new List<Virus>();
        for(int i = 0; i < vDB.Count(); i++)
        {
            VirusBase.Add(new Virus(vDB.ElementAt(i)));
        }

        db.Dispose();
    }

    public void DeactivateVirus(Virus v)
    {
        if (!DeactivatedViruses.Contains(v.id))
        {
            ScrUIMessanger.inst.ShowMessage(ScrLocalizationSystem.L_msgDeactivateVirus);
            DeactivatedViruses.Add(v.id);
        }
    }
}
