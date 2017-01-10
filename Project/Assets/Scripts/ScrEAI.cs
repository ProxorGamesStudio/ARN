using UnityEngine;
using System.Collections.Generic;
using SQLite;
using UnityEngine.UI;

public class EAIParametrsDB
{
    public string id { get; set; }
    public float ScanTime { get; set; }
    public float ScanTimeFactorThreat { get; set; }

    public float ScanPeriod { get; set; }
    public float ScanPeriodFactorThreat { get; set; }
    public float ScanPeriodFactorSuccesScan { get; set; }
    public int ScanDepthBase { get; set; }
    public float ScanDepthBaseFactorThreat { get; set; }
    public float ScanDepthBaseFactorDifficult { get; set; }
    public float Scan { get; set; }
    public float ScanFactorThreat { get; set; }
    public float ScanFactorDifficult { get; set; }
    public float CoreDefence { get; set; }
    public float CoreDefenceFactorDifficult { get; set; }
    public float CoreDefenceFactorThreat { get; set; }
    public float Attack { get; set; }
    public float AttackFactorThreat { get; set; }
    public float AttackFactorDifficult { get; set; }
    public int ThreatAttack { get; set; }
}

public class ScrEAI : MonoBehaviour {
    public static ScrEAI inst;

    public float eai_TimeScanning = 5f;
    public float eai_PeriodScanning = 10f;
    public int eai_DepthScanning = 1;
    public float eai_PeriodScanningMultiplier = 1f;
    public float eai_PlayerDetectChance = 0.5f;
    public float eai_DefenceHub = 1f;
    public float eai_AttackParams = 1f;

    public bool TestScan;

    public List<ScrHub> ScanTurn, newTurn;

    private float timetoendscan;
    private float timetonextscan;
    private bool ScanInProgres;
    private bool IsLastScanFindAI;
    public ScrHub LastFindAIHub, eai_base;
    private float PeriodScanningMultiplier;
    public bool blind, ev_eai_scan_active;
    public float blindTime;
    public float blindTimeDown;
    public ScrHub ev_eai_scan_hub;
    public bool StartDeactive = true;

    void Awake()
    {
        inst = this;
        blindTimeDown = blindTime;
    }

    void Start()
    {
      
    }

    public void LoadDB()
    {
        var db = new SQLiteConnection(Application.dataPath + "/DataBase.db");
     
        var paramDB = db.Table<EAIParametrsDB>();
       
        EAIParametrsDB param = paramDB.First();

        eai_TimeScanning = param.ScanTime;
        eai_PeriodScanning = param.ScanPeriod;
        eai_DepthScanning = param.ScanDepthBase;
        eai_PeriodScanningMultiplier = param.ScanPeriodFactorSuccesScan;
        eai_PlayerDetectChance = param.Scan;
        eai_DefenceHub = param.CoreDefence;
        eai_AttackParams = param.Attack;
         

    db.Dispose();
    }

    public void AfterLoadDB()
    {
        ScanTurn = new List<ScrHub>();
   
        ScanInProgres = false;
        timetoendscan = 0;
        IsLastScanFindAI = false;
        PeriodScanningMultiplier = 1;
        timetonextscan = eai_PeriodScanning;
    }

    public void StartScanNet(int depth, ScrHub StartHub)
    {
        if(eai_base  == null)
            eai_base = FindObjectsOfType<ScrHub>()[Random.Range(0, FindObjectsOfType<ScrHub>().Length)];
        ScanTurn.Add(StartHub);
        int c = depth;
        ScrHub h = StartHub;
        while(c>0)
        {
          
            int nh = Random.Range(0, h.Lines.Count);
            ScrHub ah = new ScrHub();
                
            if (!ev_eai_scan_active)
            {
                ah = h.Lines[nh].GetAnother(h.transform).GetComponent<ScrHub>(); 
                nh = Random.Range(0, ScanTurn.Count);
                h = ScanTurn[nh];
            }
            else
            {
                ah = ev_eai_scan_hub;
                h = ScanTurn[ScanTurn.FindIndex(x => ev_eai_scan_hub)];
                ev_eai_scan_active = false;
            }
                if (ScanTurn.Contains(ah))
            {
                continue;
            }
            else
            {
                ScanTurn.Add(ah);
                if(ah.CoreAI())
                {
                    Debug.LogWarning(111111);
                    ScrUILog.inst.AddMessage(ScrLocalizationSystem.L_msgCoreAnderAttack,ah);
                }else if(ah.OwnerAI)
                {
                    Debug.LogWarning(111111);
                    ScrUILog.inst.AddMessage(ScrLocalizationSystem.L_msgHubAnderAttack, ah);
                }
                c--;
            }
        }
        ScanInProgres = true;
        timetoendscan = eai_TimeScanning;
        ScrUIController.inst.UIEAIScan.StartScan();
    }

    public void EndScanNet()
    {
        bool b = false;
        foreach (ScrHub hub in ScanTurn)
        {
            if(ScanHub(hub))
            {
                b = true;
                LastFindAIHub = hub;
            }
        }
        if(b)
        {
            PeriodScanningMultiplier *= eai_PeriodScanningMultiplier;
            IsLastScanFindAI = true;
        }
        else
        {
            PeriodScanningMultiplier = 1;
            IsLastScanFindAI = false;
        }
        ScanTurn.Clear();
        ScanInProgres = false;
        timetonextscan = eai_PeriodScanning * PeriodScanningMultiplier;
    }

    public bool ScanHub(ScrHub hub)
    {
        bool b = false;
        if (!blind)
        {
            if (hub.CoreAI())
            {
                Debug.LogWarning(111111);
                ScrUILog.inst.AddMessage(ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.msgCoreAnderAttack) + ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.Key + hub.Id), hub);
                hub.Ungrab();
                b = true;
            }
            else if (hub.OwnerAI)
            {
                Debug.LogWarning(111111);
                ScrUILog.inst.AddMessage(ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.msgHubAnderAttack) + ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.Key + hub.Id), hub);
                hub.Ungrab();
                b = true;
            }
            if (b)
            {
                foreach (Server s in hub.servers)
                {
                    if (s.trojan.Id != "non")
                    {
                        ScrTrojanController.inst.DelTrojan();
                    }
                    s.trojan = new TrojanInServer();
                }
                ScrAlertSystem.Inst.AddAlert(ScrAlertSystem.Inst.EUHAlert);
            }

            Debug.Log("Был просканирован узел " + hub.Id);
        }
        return b;
    }

    public int GetTimeToEndScan()
    {
        return (int)timetoendscan;
    }

    public bool GetScanInProgress()
    {
        return ScanInProgres;
    }

    void CreateSQLdb()//Одноразовая функция, для создания базы данных.
    {
        var db = new SQLiteConnection(Application.dataPath + "/Resources/DataBase.db");

        db.CreateTable<EAIParametrsDB>();

        db.Dispose();
    }

    void Update()
    {

        if (!StartDeactive)
        {
            if (blind)
            {
                blindTimeDown -= Time.deltaTime;
                if (blindTimeDown <= 0)
                {
                    blindTimeDown = blindTime;
                    blind = false;
                }
            }

            if (TestScan)
            {
                TestScan = false;
                List<ScrHub> h = new List<ScrHub>(GameObject.FindObjectsOfType<ScrHub>());
                int sh = Random.Range(0, h.Count);
                StartScanNet(eai_DepthScanning, h[sh]);
            }
            if (ScanInProgres)
            {
                timetoendscan -= Time.deltaTime;
                if (timetoendscan < 0)
                {
                    EndScanNet();
                }
            }
            else
            {
                timetonextscan -= Time.deltaTime;
                if (timetonextscan < 0)
                {
                    if (IsLastScanFindAI)
                    {
                        StartScanNet(eai_DepthScanning, LastFindAIHub);
                    }
                    else
                    {
                        List<ScrHub> h = new List<ScrHub>(GameObject.FindObjectsOfType<ScrHub>());
                        int sh = Random.Range(0, h.Count);
                        StartScanNet(eai_DepthScanning, h[sh]);
                    }
                }
            }
        }
    }
}
