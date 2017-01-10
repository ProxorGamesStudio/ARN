using UnityEngine;
using System.Collections.Generic;
using SQLite;

public class NetParametrsDB
{
    public string ID { get; set; }

    public int ServCDTime { get; set; }
    public int TTrojanCheck { get; set; }
    public int TVirusCheck { get; set; }
    public float ThreatFromCDServ { get; set; }
    public float HubLvlUpTime { get; set; }
    public float HubLvlUpResFactor { get; set; }
    public float HubLvlUpThreatFactor { get; set; }
    public float ThreatLvlUpPartChangedHubs { get; set; }

    public string ResesInCommonServID { get; set; }
    public string ResesInCommonServMin { get; set; }
    public string ResesInCommonServMax { get; set; }
    public string ResesInComercServID { get; set; }
    public string ResesInComercServMin { get; set; }
    public string ResesInComercServMax { get; set; }
    public string ResesInScienceServID { get; set; }
    public string ResesInScienceServMin { get; set; }
    public string ResesInScienceServMax { get; set; }
    public string ResesInMilitaryServID { get; set; }
    public string ResesInMilitaryServMin { get; set; }
    public string ResesInMilitaryServMax { get; set; }

    public string PieceOnCommonHubID { get; set; }
    public string PieceOnCommonHubParam { get; set; }
    public string PieceOnComercHubID { get; set; }
    public string PieceOnComercHubParam { get; set; }
    public string PieceOnScienceHubID { get; set; }
    public string PieceOnScienceHubParam { get; set; }
    public string PieceOnMilitaryHubID { get; set; }
    public string PieceOnMilitaryHubParam { get; set; }

    public string ProtectChanses { get; set; }
    public string CountProtects { get; set; }
    public string LevelsPProtect { get; set; }

    public float HubDefenceTrimFactor { get; set; }
    public float ServDefenceTrimFactor { get; set; }

    public float ResOnServDifficultFactor { get; set; }
    public float ServDefenceDifficultFactor { get; set; }
    public float HubDefenceDifficultFactor { get; set; }
    public float ResOnServLvlFactor { get; set; }
    public float ServDefenceLvlFactor { get; set; }
    public float HubDefenceLvlFactor { get; set; }
}

[System.Serializable]
public class NetParametrs
{
    [Header("Параметры серверов")]
    [Tooltip("Время кулдауна сервера после завершения работы трояна")]
    public int ServCDTime;
    [Tooltip("Периуд проверки серевров на трояны")]
    public int TTrojanCheck;
    [Tooltip("Периуд проверки узлов на вирусы")]
    public int TVirusCheck;
    [Tooltip("Угроза в секунду от перезагрузки сервера")]
    public float ThreatFromCDServ;
    [Tooltip("Длительность повышения уровня узла")]
    public float HubLvlUpTime;
    [Tooltip("изменение выработки ресурсов во время повышения уровня узла")]
    public float HubLvlUpResFactor;
    [Tooltip("изменение генерации угрозы во время повышения уровня узла")]
    public float HubLvlUpThreatFactor;
    [Tooltip("Количество узлов, затронутых повышением уровня")]
    public float ThreatLvlUpPartChangedHubs;

    [Header("Ресурсы на серверах")]
    [Tooltip("Ресурсы на обычном сервере")]
    public GameResSet[] ResesInCommonServ;
    [Tooltip("Ресурсы на коммерческом сервере")]
    public GameResSet[] ResesInComercServ;
    [Tooltip("Ресурсы на научном сервере")]
    public GameResSet[] ResesInScienceServ;
    [Tooltip("Ресурсы на военном сервере")]
    public GameResSet[] ResesInMilitaryServ;

    [Header("Бонусы от узлов")]
    [Tooltip("Бонус от базы на обычном сервере")]
    public Piece[] PieceOnCommonHub;
    [Tooltip("Бонус от базы на комерческом сервере")]
    public Piece[] PieceOnComercHub;
    [Tooltip("Бонус от базы на научном сервере")]
    public Piece[] PieceOnScienceHub;
    [Tooltip("Бонус от базы на военном сервере")]
    public Piece[] PieceOnMilitaryHub;

    [Header("Защита на узлах")]
    [Tooltip("шанс выпадения той или иной защиты")]
    public float[] ProtectChanses;
    [Tooltip("количество видов защит на узле")]
    public int[] CountProtects;
    [Tooltip("через сколько повышений уровня узла кол-во типов защит на узле увеличится")]
    public int[] LevelsPProtect;

    [Header("Подстроечные коэффициенты")]
    [Tooltip("Коэффициент защиты узла")]
    public float HubDefenceTrimFactor;
    [Tooltip("коэффициент защиты сервера")]
    public float ServDefenceTrimFactor;

    [Header("коэффициенты сложности")]
    [Tooltip("коэффициент ресурсов на сервере")]
    public float ResOnServDifficultFactor;
    [Tooltip("коэффициент защиты сервера")]
    public float ServDefenceDifficultFactor;
    [Tooltip("коэффициент защиты узла")]
    public float HubDefenceDifficultFactor;

    [Header("коэффициенты уровней")]
    [Tooltip("коэффициент ресурсов на сервере от уровня сервера")]
    public float ResOnServLvlFactor;
    [Tooltip("коэффициент защиты сервера")]
    public float ServDefenceLvlFactor;
    [Tooltip("коэффициент защиты узла")]
    public float HubDefenceLvlFactor;

    public float GK1 = 1;
    public float GK2 = 1;
    public float GK3 = 0.94f;
    public float GK4 = 1;

    public NetParametrs()
    {
        ServCDTime = 150;
        TTrojanCheck = 120;
        TVirusCheck = 120;
        ThreatFromCDServ = 0;
        HubLvlUpTime = 0;
        HubLvlUpResFactor = 1f;
        HubLvlUpThreatFactor = 1f;
        ThreatLvlUpPartChangedHubs = 1f;
        ResesInComercServ = new GameResSet[0];
        ResesInCommonServ = new GameResSet[0];
        ResesInMilitaryServ = new GameResSet[0];
        ResesInScienceServ = new GameResSet[0];
        PieceOnCommonHub = new Piece[0];
        PieceOnComercHub = new Piece[0];
        PieceOnScienceHub = new Piece[0];
        PieceOnMilitaryHub = new Piece[0];
        ProtectChanses = new float[0];
        CountProtects = new int[0];
        LevelsPProtect = new int[0];
        HubDefenceTrimFactor = 1;
        ServDefenceTrimFactor = 1;
        ResOnServDifficultFactor = 1;
        ServDefenceDifficultFactor = 1;
        HubDefenceDifficultFactor = 1;
        ResOnServLvlFactor = 1;
        ServDefenceLvlFactor = 1;
        HubDefenceLvlFactor = 1;
    }

    public NetParametrs(NetParametrsDB db)
    {
        ServCDTime = db.ServCDTime;
        TTrojanCheck = db.TTrojanCheck;
        TVirusCheck = db.TVirusCheck;
        ThreatFromCDServ = db.ThreatFromCDServ;
        HubLvlUpTime = db.HubLvlUpTime;
        HubLvlUpResFactor = db.HubLvlUpResFactor;
        HubLvlUpThreatFactor = db.HubLvlUpThreatFactor;
        ThreatLvlUpPartChangedHubs = db.ThreatLvlUpPartChangedHubs;

        List<string> bs = ScrLoader.StringToArrayString(db.ResesInComercServID);
        List<int> bi1 = ScrLoader.StringToArrayInt(db.ResesInComercServMax);
        List<int> bi2 = ScrLoader.StringToArrayInt(db.ResesInComercServMin);
        int c = 0;
        ResesInComercServ = new GameResSet[bs.Count];
        foreach(var r in bs)
        {
            ResesInComercServ[c] = new GameResSet();
            ResesInComercServ[c].Id = r;
            ResesInComercServ[c].Max = bi1[c];
            ResesInComercServ[c].Min = bi2[c];
            c++;
        }

        bs = ScrLoader.StringToArrayString(db.ResesInCommonServID);
        bi1 = ScrLoader.StringToArrayInt(db.ResesInCommonServMax);
        bi2 = ScrLoader.StringToArrayInt(db.ResesInCommonServMin);
        c = 0;
        ResesInCommonServ = new GameResSet[bs.Count];
        foreach (var r in bs)
        {
            ResesInCommonServ[c] = new GameResSet();
            ResesInCommonServ[c].Id = r;
            ResesInCommonServ[c].Max = bi1[c];
            ResesInCommonServ[c].Min = bi2[c];
            c++;
        }

        bs = ScrLoader.StringToArrayString(db.ResesInMilitaryServID);
        bi1 = ScrLoader.StringToArrayInt(db.ResesInMilitaryServMax);
        bi2 = ScrLoader.StringToArrayInt(db.ResesInMilitaryServMin);
        c = 0;
        ResesInMilitaryServ = new GameResSet[bs.Count];
        foreach (var r in bs)
        {
            ResesInMilitaryServ[c] = new GameResSet();
            ResesInMilitaryServ[c].Id = r;
            ResesInMilitaryServ[c].Max = bi1[c];
            ResesInMilitaryServ[c].Min = bi2[c];
            c++;
        }

        bs = ScrLoader.StringToArrayString(db.ResesInScienceServID);
        bi1 = ScrLoader.StringToArrayInt(db.ResesInScienceServMax);
        bi2 = ScrLoader.StringToArrayInt(db.ResesInScienceServMin);
        c = 0;
        ResesInScienceServ = new GameResSet[bs.Count];
        foreach (var r in bs)
        {
            ResesInScienceServ[c] = new GameResSet();
            ResesInScienceServ[c].Id = r;
            ResesInScienceServ[c].Max = bi1[c];
            ResesInScienceServ[c].Min = bi2[c];
            c++;
        }

        bs = ScrLoader.StringToArrayString(db.PieceOnCommonHubID);
        bi1 = ScrLoader.StringToArrayInt(db.PieceOnCommonHubParam);
        c = 0;
        PieceOnCommonHub = new Piece[bs.Count];
        foreach (var r in bs)
        {
            PieceOnCommonHub[c].id = r;
            PieceOnCommonHub[c].Param = bi1[c];
            c++;
        }

        bs = ScrLoader.StringToArrayString(db.PieceOnComercHubID);
        bi1 = ScrLoader.StringToArrayInt(db.PieceOnComercHubParam);
        c = 0;
        PieceOnComercHub = new Piece[bs.Count];
        foreach (var r in bs)
        {
            PieceOnComercHub[c].id = r;
            PieceOnComercHub[c].Param = bi1[c];
            c++;
        }

        bs = ScrLoader.StringToArrayString(db.PieceOnScienceHubID);
        bi1 = ScrLoader.StringToArrayInt(db.PieceOnScienceHubParam);
        c = 0;
        PieceOnScienceHub = new Piece[bs.Count];
        foreach (var r in bs)
        {
            PieceOnScienceHub[c].id = r;
            PieceOnScienceHub[c].Param = bi1[c];
            c++;
        }

        bs = ScrLoader.StringToArrayString(db.PieceOnMilitaryHubID);
        bi1 = ScrLoader.StringToArrayInt(db.PieceOnMilitaryHubParam);
        c = 0;
        PieceOnMilitaryHub = new Piece[bs.Count];
        foreach (var r in bs)
        {
            PieceOnMilitaryHub[c].id = r;
            PieceOnMilitaryHub[c].Param = bi1[c];
            c++;
        }

        ProtectChanses = ScrLoader.StringToArrayFloat(db.ProtectChanses).ToArray();
        CountProtects = ScrLoader.StringToArrayInt(db.CountProtects).ToArray();
        LevelsPProtect = ScrLoader.StringToArrayInt(db.LevelsPProtect).ToArray();
        //
        HubDefenceTrimFactor = db.HubDefenceTrimFactor;
        ServDefenceTrimFactor = db.ServDefenceTrimFactor;
        ResOnServDifficultFactor = db.ResOnServDifficultFactor;
        ServDefenceDifficultFactor = db.ServDefenceDifficultFactor;
        HubDefenceDifficultFactor = db.HubDefenceDifficultFactor;
        ResOnServLvlFactor = db.ResOnServLvlFactor;
        ServDefenceLvlFactor = db.ServDefenceLvlFactor;
        HubDefenceLvlFactor = db.HubDefenceLvlFactor;
    }
}

[System.Serializable]
public class NetFactors
{
}

public class ScrNetController : MonoBehaviour {
    public static ScrNetController inst;

    public NetParametrs BaseNetParametrs;

    void CreateSQLdb()//Одноразовая функция, для создания базы данных.
    {
        var db = new SQLiteConnection(Application.dataPath + "/Resources/DataBase.db");

        db.CreateTable<NetParametrsDB>();

        db.Dispose();
    }

    void Awake () {
        //CreateSQLdb();
        inst = this;
	}

    public void LoadDB()
    {
        var db = new SQLiteConnection(Application.dataPath + "/DataBase.db");

        var paramDB = db.Table<NetParametrsDB>();

        NetParametrsDB param = paramDB.First();

        BaseNetParametrs = new NetParametrs(param);

        db.Dispose();
    }

    public void ThreatLvlvUp()
    {
        List<ScrHub> hubs = new List<ScrHub>(GameObject.FindObjectsOfType<ScrHub>());
        int c = (int)(hubs.Count * BaseNetParametrs.ThreatLvlUpPartChangedHubs);
        while (c > 0)
        {
            int n = Random.Range(0, hubs.Count);
            hubs[n].StartLvlUp();
            hubs.RemoveAt(n);
            c--;
        }
    }
}