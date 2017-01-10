using UnityEngine;
using System.Collections.Generic;
using SQLite;

/*Параметры ИИ для хранения в базе данных*/
public class AIParametrsDB
{
    public string id { get; set; }//идентификатор
    public int ScanHubTime { get; set; }//Время сканирования узла
    public int ScanHubMaxDistance { get; set; }//Максимальная дальность сканирования
    public float ScanHubCostFactorDistance { get; set; }//К увеличения стоимости сканирования от расстояния до узла
    public float ScanHubTimeFactorDistance { get; set; }//К увеличения времени сканирования от расстояния до узла
    public string ScanHubCostId { get; set; }//Какие ресурсы нужны для сканирования
    public string ScanHubCostCount { get; set; }//Сколько ресурсов нужно для сканирования ближайшего узла
    public int RemoveCoreMaxDistance { get; set; }//Максимальное расстояние, на которое можно переместить ИИ
    public int RemoveCoreTime { get; set; }//Время переещения ИИ на соседний узел
    public float RemoveCoreTimeFactorDistance { get; set; }//К времени перемещения ИИ от расстояния
    public float RemoveCoreCostFactorDistance { get; set; }//К стоимости перемещения ИИ от расстояния
    public int RemoveCoreCooldown { get; set; }//Перезарядка возможности переместить ИИ
    public string RemoveCoreCostId { get; set; }//Ресурсы для перемещение
    public string RemoveCoreCostCount { get; set; }//Кол-во ресурсов за перемещение на соседний узел
}

public class ScrAI : MonoBehaviour {
    public static ScrAI inst;

    public int DefultMaxDistanceRemove = 5;
    public int RemoveCoreAITime = 20;
    public float RemoveCoreAITimeK = 1f;
    public int RemoveCoreAICooldown = 60;
    public GameResStor[] CostMoveCoreAI;
    public float CostMoveCoreAIK = 1f;
    public int TimeScanHub = 10;
    [Header("Стоимость сканирования")]
    public GameResStor[] CostScanHub;
    [Tooltip("Коэффициент прироста стоимости от расстояния от базы")]
    public float CostScanHubK;

    public ScrLine PrefabLine;

    private ScrHub Core;
    private bool RmvCoreInPrgrs;
    private float Tm2EndRmvCore;
    private float CDRmvCore;
    private List<Piece> WorkedPieces;
    ScrPieceSystem PieceSystem;
    private ScrLine RmvCrAILine;

    void CreateSQLdb()//Одноразовая функция, для создания базы данных.
    {
        var db = new SQLiteConnection(Application.dataPath + "/Resources/DataBase.db");

        db.CreateTable<AIParametrsDB>();

        db.Dispose();
    }

    void Awake () {
        inst = this;
        PieceSystem = FindObjectOfType<ScrPieceSystem>();
    }

    public void LoadDB()
    {
        var db = new SQLiteConnection(Application.dataPath + "/DataBase.db");

        var paramDB = db.Table<AIParametrsDB>();

        AIParametrsDB param = paramDB.First();

        DefultMaxDistanceRemove = param.RemoveCoreMaxDistance;
        RemoveCoreAITime = param.RemoveCoreTime;
        RemoveCoreAITimeK = param.RemoveCoreTimeFactorDistance;
        RemoveCoreAICooldown = param.RemoveCoreCooldown;

        List<string> bs = ScrLoader.StringToArrayString(param.RemoveCoreCostId);
        List<int> bi1 = ScrLoader.StringToArrayInt(param.RemoveCoreCostCount);
        int c = 0;
        CostMoveCoreAI = new GameResStor[bs.Count];
        foreach (var r in bs)
        {
            CostMoveCoreAI[c] = new GameResStor(r, bi1[c]);
            c++;
        }
        
        CostMoveCoreAIK = param.RemoveCoreCostFactorDistance;
        TimeScanHub = param.ScanHubTime;

        bs = ScrLoader.StringToArrayString(param.ScanHubCostId);
        bi1 = ScrLoader.StringToArrayInt(param.ScanHubCostCount);
        c = 0;
        CostScanHub = new GameResStor[bs.Count];
        foreach (var r in bs)
        {
            CostScanHub[c] = new GameResStor(r,bi1[c]);
            c++;
        }
        
        CostScanHubK = param.ScanHubTimeFactorDistance;

    db.Dispose();
    }
    
    public void GenerateGame(ScrHub StartHub)
    {
        Core = StartHub;
        Core.IsInvis = false;
        Core.Scan();
        Core.OwnerAI = true;
        Core.SetAICore();
    }

    void Start()
    {
        Tm2EndRmvCore = 0;
        RmvCoreInPrgrs = false;
        CDRmvCore = 0;
        WorkedPieces = new List<Piece>();
    }

    public void StrtRmvCrAI(ScrHub trgt)
    {
        foreach (var r in CostMoveCoreAI)
        {
            GameResStor br = new GameResStor(r.Id, r.count);
            br.count = (int)(r.count + (r.count * (trgt.DistanceToAICore() - 1) * CostMoveCoreAIK));
            ScrResesController.inst.TakeRes(br);
        }
        RmvCrAILine = Instantiate(PrefabLine);
        RmvCrAILine.From = Core.transform;
        RmvCrAILine.To = trgt.transform;
        Core.ResetAICore();
        Core = trgt;
        RmvCoreInPrgrs = true;
        Tm2EndRmvCore = RemoveCoreAITime + (RemoveCoreAITime * (trgt.DistanceToAICore() - 1) * RemoveCoreAITimeK);
        ScrUIController.inst.UIRemoveAICore.RemoveIsStart();
        int c = WorkedPieces.Count;
        while(c > 0)
        {
            c--;
            ScrPieceSystem.PieceFunctionOff(WorkedPieces[c]);
            WorkedPieces.RemoveAt(c);
        }
    }

    void EndRmvCrAI()
    {
        Core.SetAICore();
        CDRmvCore = RemoveCoreAICooldown;
        RmvCoreInPrgrs = false;
        DestroyImmediate(RmvCrAILine.gameObject);
        SetPieces();
        ScrUILog.inst.AddMessage(ScrLocalizationSystem.GetLocalString("#msg_AI_remove_succes"),Core);
    }

    public bool RmvAIcrInCD()
    {
        if (CDRmvCore > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public float GetCDRmvAIcr()
    {
        return CDRmvCore;
    }

    public float IsRmvAIcrInPrgrs()
    {
        return Tm2EndRmvCore;
    }

    void SetPieces()
    {
        switch(Core.Type)
        {
            case 0:
                foreach(var p in ScrNetController.inst.BaseNetParametrs.PieceOnCommonHub)
                {
                    PieceSystem.PieceFunctionOn(p, 0);
                    WorkedPieces.Add(p);
                }
                break;
            case 1:
                foreach (var p in ScrNetController.inst.BaseNetParametrs.PieceOnComercHub)
                {
                    PieceSystem.PieceFunctionOn(p, 0);
                    WorkedPieces.Add(p);
                }
                break;
            case 2:
                foreach (var p in ScrNetController.inst.BaseNetParametrs.PieceOnScienceHub)
                {
                    PieceSystem.PieceFunctionOn(p, 0);
                    WorkedPieces.Add(p);
                }
                break;
            case 3:
                foreach (var p in ScrNetController.inst.BaseNetParametrs.PieceOnMilitaryHub)
                {
                    PieceSystem.PieceFunctionOn(p, 0);
                    WorkedPieces.Add(p);
                }
                break;
            default:
                Debug.LogError("Ошибка типа узла");
                break;
        }
    }

    void Update () {
        /*if(!Core)
        {
            Core = StartHub;
            SetPieces();
        }*/
        if (RmvCoreInPrgrs)
        {
            Tm2EndRmvCore -= Time.deltaTime;
            if (Tm2EndRmvCore <= 0)
            {
                EndRmvCrAI();
            }
        }
        if (CDRmvCore > 0)
        {
            CDRmvCore -= Time.deltaTime;
        }
    }
}
