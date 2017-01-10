using UnityEngine;
using System.Collections.Generic;

using SQLite;

[System.Serializable]
public class PieceOfVirus
{
    public string id;
    public bool IsVisible;
    public float FindInServer;
    public float GrabTimeFaktor;
    public float GrabTimeSum;
    public float AntivirusCrackChanceFaktor;
    public float AntivirusCrackChanceSum;
    public float FirewallCrackChanceFaktor;
    public float FirewallCrackChanceSum;
    public float AdmincontrolCrackChanceFaktor;
    public float AdmincontrolCrackChanceSum;
    public List<GameResStor> CostSum;
    public float CostFaktor;
    public float GrabThreatFaktor;
    public float GrabThreatSum;
    public float DisquiseFaktor;
    public float DisquiseSum;
    public int AddGrabHubSum;

    public PieceOfVirus()
    {
        id = "non";
        IsVisible = false;
        FindInServer = 0;
        GrabTimeFaktor = 1f;
        GrabTimeSum = 0;
        AntivirusCrackChanceFaktor = 1f;
        AntivirusCrackChanceSum = 0;
        FirewallCrackChanceFaktor = 1f;
        FirewallCrackChanceSum = 0;
        AdmincontrolCrackChanceFaktor = 1f;
        AdmincontrolCrackChanceSum = 0;
        CostSum = new List<GameResStor>();
        CostFaktor = 1f;
        GrabThreatFaktor = 1f;
        GrabThreatSum = 0;
        DisquiseFaktor = 1f;
        DisquiseSum = 0;
        AddGrabHubSum = 0;
    }

    public PieceOfVirus(PieceOfVirusDB db)
    {
        id = db.id;
        IsVisible = db.VisibleOnStart;
        FindInServer = db.FindInServer;
        GrabTimeFaktor = db.GrabTimeFaktor;
        GrabTimeSum = db.GrabTimeSum;
        AntivirusCrackChanceFaktor = db.AntivirusCrackChanceFaktor;
        AntivirusCrackChanceSum = db.AntivirusCrackChanceSum;
        FirewallCrackChanceFaktor = db.FirewallCrackChanceFaktor;
        FirewallCrackChanceSum = db.FirewallCrackChanceSum;
        AdmincontrolCrackChanceFaktor = db.AdmincontrolCrackChanceFaktor;
        AdmincontrolCrackChanceSum = db.AdmincontrolCrackChanceSum;
        List<string> bs = ScrLoader.StringToArrayString(db.CostSumID);
        List<int> bi1 = ScrLoader.StringToArrayInt(db.CostSumCount);
        int c = 0;
        CostSum = new List<GameResStor>();
        foreach (var r in bs)
        {
            CostSum.Add(new GameResStor(r, bi1[c]));
            c++;
        }
        CostFaktor = db.CostFaktor;
        GrabThreatFaktor = db.GrabThreatFaktor;
        GrabThreatSum = db.GrabThreatSum;
        DisquiseFaktor = db.DisquiseFaktor;
        DisquiseSum = db.DisquiseSum;
        AddGrabHubSum = db.AddGrabHubSum;
    }

    public static PieceOfVirus operator + (PieceOfVirus p1, PieceOfVirus p2)
    {
        PieceOfVirus ret = new PieceOfVirus();
        ret.GrabTimeSum = p1.GrabTimeSum + p2.GrabTimeSum;
        ret.GrabTimeFaktor = p1.GrabTimeFaktor * p2.GrabTimeFaktor;
        ret.AntivirusCrackChanceSum = p1.AntivirusCrackChanceSum + p2.AntivirusCrackChanceSum;
        ret.AntivirusCrackChanceFaktor = p1.AntivirusCrackChanceFaktor * p2.AntivirusCrackChanceFaktor;
        ret.FirewallCrackChanceSum = p1.FirewallCrackChanceSum + p2.FirewallCrackChanceSum;
        ret.FirewallCrackChanceFaktor = p1.FirewallCrackChanceFaktor * p2.FirewallCrackChanceFaktor;
        ret.AdmincontrolCrackChanceSum = p1.AdmincontrolCrackChanceSum + p2.AdmincontrolCrackChanceSum;
        ret.AdmincontrolCrackChanceFaktor = p1.AdmincontrolCrackChanceFaktor * p2.AdmincontrolCrackChanceFaktor;
        ret.CostSum = new List<GameResStor>();
        foreach(var c in p1.CostSum)
        {
            ret.CostSum.Add(c);
        }
        foreach (var c in p2.CostSum)
        {
            if(ret.CostSum.Exists(x => x.Id == c.Id))
            {
                ret.CostSum.Find(x => x.Id == c.Id).count += c.count;
            }
            else
            {
                ret.CostSum.Add(c);
            }
        }
        ret.CostFaktor = p1.CostFaktor * p2.CostFaktor;
        ret.GrabThreatSum = p1.GrabThreatSum + p2.GrabThreatSum ;
        ret.GrabThreatFaktor = p1.GrabTimeFaktor * p2.GrabThreatFaktor;
        ret.DisquiseSum = p1.DisquiseSum + p2.DisquiseSum;
        ret.DisquiseFaktor = p1.DisquiseFaktor * p2.DisquiseFaktor;
        ret.AddGrabHubSum = p1.AddGrabHubSum + p2.AddGrabHubSum;
        return ret;
    }

    public static PieceOfVirus operator -(PieceOfVirus p1, PieceOfVirus p2)
    {
        PieceOfVirus ret = new PieceOfVirus();
        ret.GrabTimeSum = p1.GrabTimeSum - p2.GrabTimeSum;
        ret.GrabTimeFaktor = p1.GrabTimeFaktor / p2.GrabTimeFaktor;
        ret.AntivirusCrackChanceSum = p1.AntivirusCrackChanceSum - p2.AntivirusCrackChanceSum;
        ret.AntivirusCrackChanceFaktor = p1.AntivirusCrackChanceFaktor / p2.AntivirusCrackChanceFaktor;
        ret.FirewallCrackChanceSum = p1.FirewallCrackChanceSum - p2.FirewallCrackChanceSum;
        ret.FirewallCrackChanceFaktor = p1.FirewallCrackChanceFaktor / p2.FirewallCrackChanceFaktor;
        ret.AdmincontrolCrackChanceSum = p1.AdmincontrolCrackChanceSum - p2.AdmincontrolCrackChanceSum;
        ret.AdmincontrolCrackChanceFaktor = p1.AdmincontrolCrackChanceFaktor / p2.AdmincontrolCrackChanceFaktor;
        ret.CostSum = new List<GameResStor>();
        foreach (var c in p1.CostSum)
        {
            ret.CostSum.Add(c);
        }
        foreach (var c in p2.CostSum)
        {
            if (ret.CostSum.Exists(x => x.Id == c.Id))
            {
                ret.CostSum.Find(x => x.Id == c.Id).count -= c.count;
            }
            else
            {
                ret.CostSum.Add(new GameResStor(c.Id,-c.count));
            }
        }
        ret.CostFaktor = p1.CostFaktor / p2.CostFaktor;
        ret.GrabThreatSum = p1.GrabThreatSum - p2.GrabThreatSum;
        ret.GrabThreatFaktor = p1.GrabTimeFaktor / p2.GrabThreatFaktor;
        ret.DisquiseSum = p1.DisquiseSum - p2.DisquiseSum;
        ret.DisquiseFaktor = p1.DisquiseFaktor / p2.DisquiseFaktor;
        ret.AddGrabHubSum = p1.AddGrabHubSum - p2.AddGrabHubSum;
        return ret;
    }
}

public class PieceOfVirusDB
{
    public string id { get; set; }
    public bool VisibleOnStart { get; set; }
    public float FindInServer { get; set; }
    public float GrabTimeFaktor { get; set; }
    public float GrabTimeSum { get; set; }
    public float AntivirusCrackChanceFaktor { get; set; }
    public float AntivirusCrackChanceSum { get; set; }
    public float FirewallCrackChanceFaktor { get; set; }
    public float FirewallCrackChanceSum { get; set; }
    public float AdmincontrolCrackChanceFaktor { get; set; }
    public float AdmincontrolCrackChanceSum { get; set; }
    public string CostSumID { get; set; }
    public string CostSumCount { get; set; }
    public float CostFaktor { get; set; }
    public float GrabThreatFaktor { get; set; }
    public float GrabThreatSum { get; set; }
    public float DisquiseFaktor { get; set; }
    public float DisquiseSum { get; set; }
    public int AddGrabHubSum { get; set; }
}

public class ScrVirusKonstructor : MonoBehaviour {
    public static ScrVirusKonstructor inst;

    public string IdOfUserVirus = "#Virus_";

    public List<PieceOfVirus> pieces;
    public List<int> pcsInServ;

    public Virus CreatedVirus;

    private int VirusCount;
    private PieceOfVirus SumPiece;

    void CreateSQLdb()//Одноразовая функция, для создания базы данных.
    {
        var db = new SQLiteConnection(Application.dataPath + "/DataBase.db");

        db.CreateTable<PieceOfVirusDB>();

        db.Dispose();
    }

    void Awake()
    {
        //CreateSQLdb();
        inst = this;
    }

    public void LoadDB()
    {
        pieces = new List<PieceOfVirus>();
        pcsInServ = new List<int>();
        var db = new SQLiteConnection(Application.dataPath + "/DataBase.db");

        var pieceDB = db.Table<PieceOfVirusDB>();

        for(int i = 0; i < pieceDB.Count(); i++)
        {
            pieces.Add(new PieceOfVirus(pieceDB.ElementAt(i)));
            if(!pieceDB.ElementAt(i).VisibleOnStart && pieceDB.ElementAt(i).FindInServer > 0)
            {
                pcsInServ.Add(i);
            }
            
        }

        db.Dispose();
    }

	void Start () {
        ResetVirus();
        VirusCount = 0;
	}

    public void DoneCreatedVirus()
    {
        RecalculateVirus();
        CreatedVirus.id = IdOfUserVirus + VirusCount.ToString();
        ScrVirusController.inst.VirusBase.Add(CreatedVirus);
        VirusCount++;
        ScrAftemat.inst.CountCreatedVirus++;
        ScrUILog.inst.AddMessage(ScrLocalizationSystem.GetLocalString("#msg_virus_created" + CreatedVirus.name));
        ResetVirus();
    }

    public void AddPiece(string p)
    {
        if(pieces.Exists(x => x.id == p))
        {
            AddPiece(pieces.Find(x => x.id == p));
        }
    }

    public void RemovePiece(string p)
    {
        if (pieces.Exists(x => x.id == p))
        {
            RemovePiece(pieces.Find(x => x.id == p));
        }
    }

    public void AddPiece(PieceOfVirus p)
    {
        SumPiece += p;
        RecalculateVirus();
        CreatedVirus.Pieces.Add(p.id);
    }

    public void RemovePiece(PieceOfVirus p)
    {
        SumPiece -= p;
        RecalculateVirus();
        if(CreatedVirus.Pieces.Contains(p.id))
        {
            CreatedVirus.Pieces.Remove(p.id);
        }
        else
        {
            Debug.LogError("Ошибка в расчете создаваемого вируса");
        }
    }

    public void RecalculateVirus()
    {
        CreatedVirus.GrabTime = SumPiece.GrabTimeSum;
        CreatedVirus.GrabTime *= SumPiece.GrabTimeFaktor;
        CreatedVirus.AntivirusCrackChance = SumPiece.AntivirusCrackChanceSum;
        CreatedVirus.AntivirusCrackChance *= SumPiece.AntivirusCrackChanceFaktor;
        CreatedVirus.FirewallCrackChance = SumPiece.FirewallCrackChanceSum;
        CreatedVirus.FirewallCrackChance *= SumPiece.FirewallCrackChanceFaktor;
        CreatedVirus.AdmincontrolCrackChance = SumPiece.AdmincontrolCrackChanceSum;
        CreatedVirus.AdmincontrolCrackChance *= SumPiece.AdmincontrolCrackChanceFaktor;
        CreatedVirus.Cost = new List<GameResStor>();
        foreach (var c in SumPiece.CostSum)
        {
            CreatedVirus.Cost.Add(c);
        }
        foreach (var c in CreatedVirus.Cost)
        {
            c.count = (int)(c.count * SumPiece.CostFaktor);
        }
        CreatedVirus.GrabThreat = SumPiece.GrabThreatSum;
        CreatedVirus.GrabThreat *= SumPiece.GrabThreatFaktor;
        CreatedVirus.Disquise = SumPiece.DisquiseSum;
        CreatedVirus.Disquise *= SumPiece.DisquiseFaktor;
        CreatedVirus.AddGrabHub = SumPiece.AddGrabHubSum;
    }

    public void ResetVirus()
    {
        CreatedVirus = new Virus();
        SumPiece = new PieceOfVirus();
    }

    public bool CheckSimilar(Virus v)
    {
        foreach(var vb in ScrVirusController.inst.VirusBase)
        {
            if(vb == v)
            {
                return true;
            }
        }
        return false;
    }

    public void FindPcsInServ()
    {
        int c = 0;
        foreach(var i in pcsInServ)
        {
            float ch = Random.value;
            if(ch < pieces[i].FindInServer)
            {
                pieces[i].IsVisible = true;
                pcsInServ.Remove(c);
                break;
            }
            c++;
        }
    }
}
