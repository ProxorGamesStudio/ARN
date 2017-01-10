using UnityEngine;
using System.Collections.Generic;

using SQLite;

[System.Serializable]
public class GameEventDB
{
    [PrimaryKey, Unique]
    public string Id { get; set; }
    public string Description { get; set; }
    public string CostId { get; set; }
    public string CostCount { get; set; }
    public string PieceId { get; set; }
    public string PieceParametr { get; set; }
    public bool IsPositive { get; set; }
    public int Valuation { get; set; }
    public int AplArea { get; set; }
    public int LifeTime { get; set; }
}

[System.Serializable]
public class PieceOfGameEvent
{
    public string Id;
    public float Parametr;
    public int distance, depth;
    public float lifeTime;
}

[System.Serializable]
public class GameEvent
{
    public string Id;
    public string Description;
    public List<GameResStor> Cost;
    public List<PieceOfGameEvent> Pieces;
    public bool IsPositive;
    public int Valuation;
    public int AplArea;
    public int LifeTime;


   /* public GameEvent(GameEventDB db)
    {
        Id = db.Id;
        Description = db.Description;

        Cost = new List<GameResStor>();
        string reses = db.CostId;
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
        reses = db.CostCount;
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
        }

        Pieces = new List<PieceOfGameEvent>();
        string[] pieceTemp = db.PieceParametr.Split(' ');
        string[] pieceIdTemp = db.PieceId.Split(' ');

        for (int i = 0; i < pieceIdTemp.Length; i++)
            Debug.LogWarning(i + ": " + pieceIdTemp[i]);

        for (int i = 0; i < pieceTemp.Length; i++)
        {
            if(pieceTemp[i] != " ")
            {
                PieceOfGameEvent pc = new PieceOfGameEvent();
                pc.Id = pieceIdTemp[i];
                pc.Parametr = System.Convert.ToInt32(pieceTemp[i]);
                pc.lifeTime = db.LifeTime;
              
                Pieces.Add(pc);
            }
        }
        LifeTime = db.LifeTime;
        IsPositive = db.IsPositive;
        Valuation = db.Valuation;
        AplArea = db.AplArea;
        LifeTime = db.LifeTime;
    }*/
}

[System.Serializable]
public class GameEventTurn
{
    public string Id;
    public float TimeToDie;

    public GameEventTurn()
    {
        Id = "";
        TimeToDie = 0;
    }

    public GameEventTurn(GameEvent e)
    {
        Id = e.Id;
        TimeToDie = ScrEventController.inst.EventLifeTime;
    }

    public GameEventTurn(GameEvent e, int T2D)
    {
        Id = e.Id;
        TimeToDie = T2D;
    }
}

public class ScrEventController : MonoBehaviour {
    public static ScrEventController inst;
    
    public int EventLifeTime = 0;
    public int EventTimerMin = 5;
    public int EventTimerMax = 10;

    public bool AddEventOn = true;
    public int MaxEventInTurn = 5;
    public int MinPosEv = 3;
    public int MaxPosEv = 4;

    public List<GameEvent> gameEvents;

    public List<GameEventTurn> turn;
    public List<GameEventTurn> workedEvents;
    public ScrPieceSystem PieceSystem;

    private float TimeToAdd;
    private int CountPos;//кол-во положительных эвентов

    private string MesDefuseRes;

    void CreateSQLdb()//Одноразовая функция, для создания базы данных.
    {
        var db = new SQLiteConnection(Application.dataPath + "/Resources/DataBase.db");

        db.CreateTable<GameEventDB>();

        db.Dispose();
    }

    void Awake()
    {
        inst = this;

        // CreateSQLdb();
    }

    public void Start()
    {
        PieceSystem = FindObjectOfType<ScrPieceSystem>();
        TimeToAdd = Random.Range(EventTimerMin, EventTimerMax + 1);
        turn = new List<GameEventTurn>();
        workedEvents = new List<GameEventTurn>();
        CountPos = 0;
       
        MesDefuseRes = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.msg_defuse_res);
    }

    /*
    public void LoadDB()
    {
        var db = new SQLiteConnection(Application.dataPath + "/Resources/DataBase.db");
        var TDB = db.Table<GameEventDB>();
        gameEvents = new List<GameEvent>();
        foreach (var t in TDB)
        {
            gameEvents.Add(new GameEvent(t));
        }
        db.Dispose();
    }*/
    /*
    public void AfterLoadDB () {
        TimeToAdd = Random.Range(EventTimerMin, EventTimerMax + 1);
        turn = new List<GameEventTurn>();
        workedEvents = new List<GameEventTurn>();
        CountPos = 0;

        MesDefuseRes = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.msg_defuse_res);//пере
    }*/
	
	void Update () {
       


	    if (AddEventOn)
        {
            if(TimeToAdd > 0)
            {
                TimeToAdd-=Time.deltaTime;
            }
            else
            {
                if(turn.Count < MaxEventInTurn)
                {
     
                    int ie = Random.Range(0, gameEvents.Count);
                    Debug.LogWarning("NOT_InWhile");
                    while (turn.Find(x => x.Id == gameEvents[ie].Id) != null)                   
                    {
                        ie = Random.Range(0, gameEvents.Count);
                        Debug.LogWarning("InWhile");
                    }
                    turn.Add(new GameEventTurn(gameEvents[ie]));
                    if (gameEvents[ie].IsPositive) CountPos++;
                    ScrUIController.inst.UIEventTurn.updateTurn();
                }
                TimeToAdd = Random.Range(EventTimerMin, EventTimerMax + 1);
            }
        }
        
       int c = turn.Count - 1;
       while(c >= 0)
       {
           turn[c].TimeToDie -= Time.deltaTime;
           if(turn[c].TimeToDie<0)
           {
               if(!gameEvents.Find(x => x.Id == turn[c].Id).IsPositive)
               {
                   GameEvent e = gameEvents.Find(x => x.Id == turn[c].Id);
                  // workedEvents.Add(new GameEventTurn(e, e.LiveTime));
                   foreach (var p in e.Pieces)
                   {
                       PieceSystem.PieceFunctionOn(p, e.LifeTime);
                   }
               }
               RemoveEventFromTurn(c);
           }
           c--;
       }
       c = workedEvents.Count - 1;
       while (c >= 0)
       {
           workedEvents[c].TimeToDie -= Time.deltaTime;
           if (workedEvents[c].TimeToDie < 0 && turn != null)
           {
               GameEvent e = gameEvents.Find(x => x.Id == turn[c].Id);
               foreach (var p in e.Pieces)
               {
                   ScrPieceSystem.PieceFunctionOff(p);
               }
               workedEvents.RemoveAt(c);
           }
           c--;
       }
      
    }

    void RemoveEventFromTurn(int n)
    {
        if (gameEvents.Find(x => x.Id == turn[n].Id).IsPositive) CountPos--;
        turn.RemoveAt(n);
        ScrUIController.inst.UIEventTurn.updateTurn();
    }

    public void BuyEvent(int num)
    {
        if(num < 0 || num >= MaxEventInTurn)
        {
            return;
        }

        var e = gameEvents.Find(x => x.Id == turn[num].Id);

        bool b = true;
        foreach (var r in e.Cost)
        {
            if (r.count > ScrResesController.inst.GetResInfo(r.Id))
            {
                b = false;
                ScrUIMessanger.inst.ShowMessage(MesDefuseRes);
                break;
            }
        }
       
        if (b)
        {
            foreach (var r in e.Cost)
            {
                ScrResesController.inst.TakeRes(r);
            }
            if (e.IsPositive)
            {
               // workedEvents.Add(new GameEventTurn(e, e.LiveTime));
                foreach(var p in e.Pieces)
                {
                    PieceSystem.PieceFunctionOn(p, p.lifeTime);
                    Debug.LogWarning(p.Id + ": " + p.Parametr);
                }
            }
            RemoveEventFromTurn(num);
        }
    }
}
