using UnityEngine;
using System.Collections.Generic;

using SQLite;

[System.Serializable]
public class HubParametrsDatabase
{
    [PrimaryKey, Unique]
    public string Id { get; set; }

    public float KoordinatX { get; set; }
    public float KoordinatY { get; set; }

    public float GenerationChance { get; set; }

    public float TC_Commn { get; set; }
    public float TC_Commerce { get; set; }
    public float TC_Science { get; set; }
    public float TC_Military { get; set; }

    public string Links { get; set; }
    public List<string> lLinks;

    public float C_DefenceLevel1 { get; set; }
    public float C_DefenceLevel2 { get; set; }
    public float C_DefenceLevel3 { get; set; }

    public float C_ServersLevel1 { get; set; }
    public float C_ServersLevel2 { get; set; }
    public float C_ServersLevel3 { get; set; }

    public int MaxCountServers { get; set; }
    public int MinCountServers { get; set; }
}

[System.Serializable]
public class GameResSet
{
    public string Id;
    public int Max;
    public int Min;

    public GameResSet()
    {
        Id = "";
        Max = 0;
        Min = 0;
    }

    public GameResSet(GameResSet r)
    {
        Id = r.Id;
        Max = r.Max;
        Min = r.Min;
    }
}

[System.Serializable]
public class GameResAdd
{
    public string Id;
    public int EndAdd;
    public float TimeAdd;
    public float Progress;
}

[System.Serializable]
public class Server
{
    public int Type;
    public int Lvl;
    public GameResSet[] Reses;
    public GameResAdd[] AddRes;
    public float Defens;
    public TrojanInServer trojan;
    public float CoolDown;
    public bool NeedUpdReses;
}

public class HubFactors
{
    public float Res;
    public float Threat;

    public HubFactors()
    {
        Res = 1f;
        Threat = 1f;
    }
}

public class ScrHub : MonoBehaviour {

    public string Id;
    public int Type;
    public int DefenceLvl;
    public bool[] Protects;
    public List<ScrLine> Lines;
    public List<string> Links;

    public bool IsInvis;

    public bool OwnerAI = false;
    public bool IsCoreEAI;

    public Server[] servers;

    public ScrViewsManager Visualisation;

    public HubFactors dynamicFactors;

    private bool IsCoreAI;
    private bool LstIsInvis;
    public bool Scanned;
    private float TimetoCheckTrojan;
    private float TimetoCheckVirus;

    private bool ScanInProgress;
    public float TimeToEndScan;
    public float ScanTime;

    private bool GrabInProgress;
    public float TimeToEndGrab;
    public float GrubTime;

    private bool LvlUpInProgress;
    private float TimeToEndLvlUp;

    private bool NeedUpdateDistance;
    private bool NeedUpdateVisualization;
    private Virus virus;

    private int[] ProtecteActivateTime;
    private bool[] ProtectActivate;

    private int DistToAI;

    public int ev_resources_increase;
    public float ev_resources_increase_time;
    public float ev_hub_level_change;
    public float ev_hub_level_change_time;
    public float ev_threat_decrease;
    public float ev_threat_decrease_time;
    public float ev_eai_base;
    public float ev_eai_base_time;
    public float ev_eai_base_find;
    public float tDown, tDown2, tDown3, tDown4;
    public bool ev_resources_increase_active = false,  ev_hub_level_change_active = false, ev_eai_attack_active = false, ev_threat_decrease_active = false, ev_eai_base_active = false;

    private ScrNetController net;
    public ScrPieceSystem piece;

    public GameObject cube;
    public string pieceText;
    public bool ungrab;
	void Start () {
        LstIsInvis = IsInvis;
        Visualisation = GetComponent<ScrViewsManager>();
        TimetoCheckTrojan = 0;
        TimetoCheckVirus = 10;
        Visualisation.Showed = !IsInvis;
        NeedUpdateVisualization = true;
        NeedUpdateDistance = true;
        ScanInProgress = false;
        GrabInProgress = false;
        LvlUpInProgress = false;
        TimeToEndScan = 0;
        TimeToEndGrab = 0;
        TimeToEndLvlUp = 0;
        net = ScrNetController.inst;
        virus = new Virus();
        dynamicFactors = new HubFactors();
        piece = FindObjectOfType<ScrPieceSystem>();
   
    }
	
    void UpdateVisualization()
    {
        if(Scanned && !IsInvis)
        {
            Visualisation.SetView(Type+1);
        }
        if(IsCoreAI)
        {
            Visualisation.IdSetMaterial = 3;
        }
        else if(IsCoreEAI)
        {
            Visualisation.IdSetMaterial = 4;
        }
        else if (!OwnerAI)
        {
            if (Scanned)
            {
                Visualisation.IdSetMaterial = 1;
            }
            else
            {
                Visualisation.IdSetMaterial = 0;
                Visualisation.SetView(0);
            }
        }
        else
        {
            Visualisation.IdSetMaterial = 2;
            Scanned = true;
        }
        NeedUpdateVisualization = false;
    }

    void UpdateDistance()
    {
        if (IsCoreAI)
        {
            List<ScrHub> Updt = new List<ScrHub>();
            List<ScrHub> IterUpdt = new List<ScrHub>();
            int CountHubs = FindObjectsOfType<ScrHub>().Length;
            int DistToCoreNow = 0;
            IterUpdt.Add(this);
            while(Updt.Count < CountHubs)
            {
                List<ScrHub> NxtIterUpdt = new List<ScrHub>();
                foreach (var h in IterUpdt)
                {
                    h.DistToAI = DistToCoreNow;
                    Updt.Add(h);
                    foreach(var n in h.Lines)
                    {
                        if(!Updt.Contains(n.GetAnother(h.transform).GetComponent<ScrHub>()) && !IterUpdt.Contains(n.GetAnother(h.transform).GetComponent<ScrHub>()))
                        {
                            NxtIterUpdt.Add(n.GetAnother(h.transform).GetComponent<ScrHub>());
                        }
                    }
                }
                IterUpdt.Clear();
                IterUpdt.AddRange(NxtIterUpdt);
                DistToCoreNow++;
            }
        }
        NeedUpdateDistance = false;
    }

	void Update () {
        if(ungrab)
            Ungrab();
        if (ev_hub_level_change != 0)
        {
            if (ev_hub_level_change_active)
            {
                tDown = ev_hub_level_change_time; 
                DefenceLvl += (int)ev_hub_level_change;
                if(!IsInvis)
                cube = Instantiate(piece.cube1, transform.position, Quaternion.identity);
                ev_hub_level_change_active = false;
            }
            if (tDown > 0)
                tDown -= Time.deltaTime;
            else
            {
                DefenceLvl -= (int)ev_hub_level_change;
                ev_hub_level_change = 0;
                pieceText = pieceText.Remove(pieceText.IndexOf(" #ev_hub_level_change"), (" #ev_hub_level_change").Length);
                Destroy(cube);
            }
            
        }

        if(ev_resources_increase != 0)
        {
            if(ev_resources_increase_active)
            {
                tDown2 = ev_resources_increase_time;
                ev_resources_increase_active = false;
                if (!IsInvis)
                    cube = Instantiate(piece.cube1, transform.position, Quaternion.identity);
            }
            if (tDown2 > 0)
                tDown2 -= Time.deltaTime;
            else
            {
                Destroy(cube);
                ev_resources_increase = 0;
                pieceText = pieceText.Remove(pieceText.IndexOf(" #ev_resources_increase"), (" #ev_resources_increase").Length);
            }
            
        }

        if(ev_threat_decrease != 0)
        {
            if (ev_threat_decrease_active)
            {
                tDown3 = ev_threat_decrease_time;
                ev_threat_decrease_active = false;
                if (!IsInvis)
                    cube = Instantiate(piece.cube1, transform.position, Quaternion.identity);
            }
            if (tDown3 > 0)
                tDown3 -= Time.deltaTime;
            else
            {
                Destroy(cube);
                ev_threat_decrease = 0;
                pieceText = pieceText.Remove(pieceText.IndexOf(" #ev_threat_decrease"), (" #ev_threat_decrease").Length);
            }
        }

        if (ev_eai_base != 0)
        {
            if (ev_eai_base_active)
            {
                tDown4 = ev_eai_base_time;
                ev_eai_base_active = false;
                if (!IsInvis)
                    cube = Instantiate(piece.cube2, transform.position, Quaternion.identity);
            }
            if (tDown4 > 0)
                tDown4 -= Time.deltaTime;
            else
            {
                Destroy(cube);
                ev_eai_base = 0;
                pieceText = pieceText.Remove(pieceText.IndexOf(" #ev_eai_base"), (" #ev_eai_base").Length);
            }
        }

        if (ev_eai_base_find != 0)
        {
            if (!IsInvis)
                cube = Instantiate(piece.cube3, transform.position, Quaternion.identity);
            ev_eai_base_find = 0;
        }

        if (LstIsInvis != IsInvis)
        {
            Visualisation.Showed = !IsInvis;
            LstIsInvis = IsInvis;
        }
        if(NeedUpdateVisualization)
        {
            UpdateVisualization();
        }
        if(NeedUpdateDistance)
        {
            UpdateDistance();
        }
        if(ScanInProgress)
        {
            TimeToEndScan -= Time.deltaTime;
            if(TimeToEndScan < 0)
            {
                Scan();
            }
        }
        if(GrabInProgress)
        {
            TimeToEndGrab -= Time.deltaTime;
            int c = 0;
            while(c < ProtecteActivateTime.Length)
            {
                if(ProtecteActivateTime[c] > TimeToEndGrab)
                {
                    ProtectActivate[c] = true;
                    ProtecteActivateTime[c] = -1;
                }
                c++;
            }
            if(TimeToEndGrab < 0)
            {
                ScrAlertSystem.Inst.AddAlert(ScrAlertSystem.Inst.HGAlert*10);
                Grab();
            }
        }
        if(LvlUpInProgress)
        {
            TimeToEndLvlUp -= Time.deltaTime;
            if(TimeToEndLvlUp <= 0)
            {
                EndLvlUp();
            }
        }
      /*  if(virus.id != "non")
        {
            TimetoCheckVirus -= Time.deltaTime;
            if(TimetoCheckVirus <= 0)
            {
                float ch = Random.value;
                if(ch > virus.Disquise)
                {
                    ScrVirusController.inst.DeactivateVirus(virus);
                    Ungrab();
                }
                if(ScrVirusController.inst.DeactivatedViruses.Contains(virus.id))
                {
                    Ungrab();
                }
            }
        }*/
        foreach(var s in servers)
        {
            if(s.trojan.Id != "non")
            {
                s.trojan.LifeTime += Time.deltaTime;
                ScrAlertSystem.Inst.AddAlert(ScrAlertSystem.Inst.TGAlert * Time.deltaTime + (ScrAlertSystem.Inst.TGAlert * Time.deltaTime)/100 * s.trojan.tr_threat + (ScrAlertSystem.Inst.TGAlert * Time.deltaTime) / 100 * ev_threat_decrease);
                if (s.AddRes != null) 
                    foreach (var adres in s.AddRes)
                    {
                        adres.Progress += Time.deltaTime;
                        if (adres.Progress >= adres.TimeAdd * (1 - s.trojan.tr_time / 100))
                        {
                            adres.Progress -= adres.TimeAdd * (1 - s.trojan.tr_time / 100);
                            GameResStor gr = new GameResStor();
                            gr.Id = adres.Id;
                            if(!IsCoreAI)
                            gr.count = 1 + (int)s.trojan.trRes > -1? (int)s.trojan.trRes : 0 + ev_resources_increase;
                            else
                            gr.count = adres.EndAdd / (s.trojan.MaxLifeTime / s.trojan.LifeTime) / 2 + (int)s.trojan.trRes > -1 ? (int)s.trojan.trRes : 0 + ev_resources_increase;
                            ScrResesController.inst.AddRes(gr);
                        }
                    }
                TimetoCheckTrojan -= Time.deltaTime;
                if (TimetoCheckTrojan < 0)
                {
                    TimetoCheckTrojan = net.BaseNetParametrs.TTrojanCheck;
                    float ch = Random.value;
                    float v = (s.Lvl + s.trojan.Lvl) * (s.Lvl + s.trojan.Lvl) * net.BaseNetParametrs.GK4 * s.Defens;
                    v += v*(s.trojan.tr_found/100); // добавление нашего кирпичика по дополнительному нахождению
                    if (ch < v)
                    {
                        if (s.trojan.tr_hook > Random.Range(0, 99))
                        {
                            s.trojan = new TrojanInServer();
                            ScrTrojanController.inst.DelTrojan();
                            s.CoolDown = net.BaseNetParametrs.ServCDTime;
                            ScrAlertSystem.Inst.AddAlert(ScrAlertSystem.Inst.USDTAlert);
                            ScrUIMessanger.inst.ShowMessage(ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.msg_usdt) + ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.Key + Id));//пере
                            ScrUILog.inst.AddMessage(ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.msg_usdt) + ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.Key + Id), this);
                            if(s.trojan.tr_trick > Random.Range(0, 99) && s.AddRes != null)
                                    foreach (var grd in s.AddRes)
                                    {
                                        GameResStor gr = new GameResStor();
                                        gr.Id = grd.Id;
                                        gr.count = (int)Mathf.Floor(grd.EndAdd*0.35f);
                                        ScrResesController.inst.AddRes(gr);
                                    }
                        }
                        else
                        {
                            ScrAlertSystem.Inst.AddAlert(ScrAlertSystem.Inst.USDTAlert);
                            ScrUIMessanger.inst.ShowMessage(ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.msg_failed_usdt) + ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.Key + Id));//пере
                            ScrUILog.inst.AddMessage(ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.msg_failed_usdt) + ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.Key + Id), this);
                        }
                    }
                }
                if (s.trojan.LifeTime > s.trojan.MaxLifeTime)
                {
                    if (s.AddRes != null)
                        foreach (var grd in s.AddRes)
                        {
                            GameResStor gr = new GameResStor();
                            gr.Id = grd.Id;
                            gr.count = grd.EndAdd;
                            ScrResesController.inst.AddRes(gr);
                        }
                    s.trojan = new TrojanInServer();
                    ScrTrojanController.inst.DelTrojan();
                    s.CoolDown = net.BaseNetParametrs.ServCDTime;
                }
            }
            if(s.CoolDown > 0)
            {
                s.CoolDown -= Time.deltaTime;
                ScrAlertSystem.Inst.AddAlert(ScrAlertSystem.Inst.CDSAlert * Time.deltaTime);
            }
            if(s.NeedUpdReses)
            {
                GameResSet[] reses;
                int c;
                switch (s.Type)
                {
                    case 1:
                        reses = ScrNetController.inst.BaseNetParametrs.ResesInComercServ;
                        s.Reses = new GameResSet[reses.Length];
                        c = 0;
                        foreach(var r in reses)
                        {
                            s.Reses[c] = new GameResSet(r);
                            s.Reses[c].Max += (int)(r.Max * ScrNetController.inst.BaseNetParametrs.ResOnServLvlFactor * (s.Lvl - 1));
                            s.Reses[c].Min += (int)(r.Min * ScrNetController.inst.BaseNetParametrs.ResOnServLvlFactor * (s.Lvl - 1));
                            c++;
                        }
                        break;
                    case 2:
                        reses = ScrNetController.inst.BaseNetParametrs.ResesInScienceServ;
                        s.Reses = new GameResSet[reses.Length];
                        c = 0;
                        foreach (var r in reses)
                        {
                            s.Reses[c] = new GameResSet(r);
                            s.Reses[c].Max += (int)(r.Max * ScrNetController.inst.BaseNetParametrs.ResOnServLvlFactor * (s.Lvl - 1));
                            s.Reses[c].Min += (int)(r.Min * ScrNetController.inst.BaseNetParametrs.ResOnServLvlFactor * (s.Lvl - 1));
                            c++;
                        }
                        break;
                    case 3:
                        reses = ScrNetController.inst.BaseNetParametrs.ResesInMilitaryServ;
                        s.Reses = new GameResSet[reses.Length];
                        c = 0;
                        foreach (var r in reses)
                        {
                            s.Reses[c] = new GameResSet(r);
                            s.Reses[c].Max += (int)(r.Max * ScrNetController.inst.BaseNetParametrs.ResOnServLvlFactor * (s.Lvl - 1));
                            s.Reses[c].Min += (int)(r.Min * ScrNetController.inst.BaseNetParametrs.ResOnServLvlFactor * (s.Lvl - 1));
                            c++;
                        }
                        break;
                    case 0:
                    default:
                        reses = ScrNetController.inst.BaseNetParametrs.ResesInCommonServ;
                        s.Reses = new GameResSet[reses.Length];
                        c = 0;
                        foreach (var r in reses)
                        {
                            s.Reses[c] = new GameResSet(r);
                            s.Reses[c].Max += (int)(r.Max * ScrNetController.inst.BaseNetParametrs.ResOnServLvlFactor * (s.Lvl - 1));
                            s.Reses[c].Min += (int)(r.Min * ScrNetController.inst.BaseNetParametrs.ResOnServLvlFactor * (s.Lvl - 1));
                            c++;
                        }
                        break;
                }
                s.NeedUpdReses = false;
            }
        }
	}

    public void StartLvlUp()
    {
        LvlUpInProgress = true;
        TimeToEndLvlUp = net.BaseNetParametrs.HubLvlUpTime;
        dynamicFactors.Res *= net.BaseNetParametrs.HubLvlUpResFactor;
        dynamicFactors.Threat *= net.BaseNetParametrs.HubLvlUpThreatFactor;
    }

    public void EndLvlUp()
    {
        LvlUpInProgress = false;
        dynamicFactors.Res /= net.BaseNetParametrs.HubLvlUpResFactor;
        dynamicFactors.Threat /= net.BaseNetParametrs.HubLvlUpThreatFactor;
        DefenceLvl++;
        foreach(var s in servers)
        {
            s.Lvl++;
            s.NeedUpdReses = true;
        }
        Debug.Log(Id + "повысил уровень");
    }

    public void CheckLines()
    {
        foreach (ScrLine ls in Lines)
        {
            if(!ls.GetAnother(transform).GetComponent<ScrHub>().IsInvis)
            {
                ls.IsVisibl = true;
            }
        }
    }

    public void SetAICore()
    {
        IsCoreAI = true;
        NeedUpdateVisualization = true;
        NeedUpdateDistance = true;
    }

    public void ResetAICore()
    {
        if(IsCoreAI)
        {
            IsCoreAI = false;
            NeedUpdateVisualization = true;
        }
    }

    public void StartScan()
    {
        foreach (var r in ScrAI.inst.CostScanHub)
        {
            GameResStor br = new GameResStor(r.Id, r.count);
            br.count = (int)(r.count + (r.count * (DistToAI - 1) * ScrAI.inst.CostScanHubK));
            ScrResesController.inst.TakeRes(br);
        }
        ScanInProgress = true;
        TimeToEndScan = ScrAI.inst.TimeScanHub;
        ScanTime = ScrAI.inst.TimeScanHub;
    }

    public void Scan()
    {
        if (!GrabInProgress)
        {
            foreach (ScrLine ls in Lines)
            {
                ScrHub hn = ls.GetAnother(transform).GetComponent<ScrHub>();
                hn.IsInvis = false;
            }
            CheckLines();
            Scanned = true;
            ScanInProgress = false;
            NeedUpdateVisualization = true;
            ScrUILog.inst.AddMessage(ScrLocalizationSystem.GetLocalString("#msg_hub_scan_succes"), this);
        }
    }

    public void StartGrab(Virus v)
    {
        virus = v;
        bool can = true;
       
        for (int i = 0; i < virus.Cost.Count; i++)
        {
            
            if (ScrResesController.inst.Reses[i].count < virus.Cost[i].count)
            {
                can = false;
              
            }
           
        }
        if (can)
        {
            foreach (var r in virus.Cost)
                ScrResesController.inst.TakeRes(r);

          
            GrabInProgress = true;
            TimeToEndGrab = virus.GrabTime;
            GrubTime = virus.GrabTime;

            ProtectActivate = new bool[Protects.Length];
            int c = 0;
            while (c < ProtectActivate.Length)
            {
                ProtectActivate[c] = false;
                c++;
            }
            ProtecteActivateTime = new int[Protects.Length];
            float chc;
            if (Protects[0])
            {
                chc = Random.value;
                if (chc > virus.AntivirusCrackChance)
                    ProtecteActivateTime[0] = Random.Range(1, (int)TimeToEndGrab - 1);
                else
                    ProtecteActivateTime[0] = -1;
            }
            else
            {
                ProtecteActivateTime[0] = -1;
            }
            if (Protects[1])
            {
                chc = Random.value;
                if (chc > virus.FirewallCrackChance)
                    ProtecteActivateTime[1] = Random.Range(1, (int)TimeToEndGrab - 1);
                else
                    ProtecteActivateTime[1] = -1;
            }
            else
            {
                ProtecteActivateTime[1] = -1;
            }
            if (Protects[2])
            {
                chc = Random.value;
                if (chc > virus.AdmincontrolCrackChance)
                    ProtecteActivateTime[2] = Random.Range(1, (int)TimeToEndGrab - 1);
                else
                    ProtecteActivateTime[2] = -1;
            }
            else
            {
                ProtecteActivateTime[2] = -1;
            }
        }
        else
            ScrUILog.inst.AddMessage(ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.msg_defuse_res) + ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.Key + Id), this);

    }
    public void Grab()
    {
     //   ScrAlertSystem.Inst.AddAlert(ScrAlertSystem.Inst.HGAlert);
        //NeedUpdateVisualization = true;
      
        
        bool grabSucces = true;

        foreach(var b in ProtectActivate)
        {
            if(b)
            {
                grabSucces = false;
                break;
            }
        }
        int k = 0;
        for(int i = 0; i < Lines.Count && i < virus.v_worm_HC; i++)
        if(Random.Range(0, 99) < virus.v_worm)
        {
                i = k;
            ScrHub h = Lines[Random.Range(0, Lines.Count)].GetAnother(transform).GetComponent<ScrHub>();
                if (h.virus == null)
                {
                    h.Scan();
                    h.OwnerAI = true;
                    h.virus = virus;
                    h.NeedUpdateVisualization = true;
                    h.StartGrab(virus);
                    k++;
                }
                else
                    i--;
            
        }
            if (grabSucces)
            {
            //захват дополнительных узлов
            int c = virus.AddGrabHub;
            List<ScrHub> GrabTurn = new List<ScrHub>();
            ScrHub h = this;
            GrabTurn.Add(this);
            int bc = 0;
            for (int i = 0; i < Lines.Count && i < virus.v_worm_HC; i++)
                if (Random.Range(0, 99) < virus.v_worm)
                {
                int nh = Random.Range(0, h.Lines.Count);
                ScrHub ah = h.Lines[nh].GetAnother(h.transform).GetComponent<ScrHub>();
                nh = Random.Range(0, GrabTurn.Count);
                h = GrabTurn[nh];
                if (GrabTurn.Contains(ah) || ah.OwnerAI || !ah.Scanned)
                {
                    bc++;
                }
                else
                {
                    GrabTurn.Add(ah);
                    bc = 0;
                    c--;
                }
                if (bc > 50)
                {
                    break;
                }
            }
            //захват узлов
            foreach (var gh in GrabTurn)
            {
                gh.Scan();
                gh.OwnerAI = true;
                gh.virus = virus;
                gh.NeedUpdateVisualization = true;
                ScrUILog.inst.AddMessage(ScrLocalizationSystem.GetLocalString("#msg_hub_grab_succes"), gh);
                ScrNewsSystem.inst.HapEvent("hub_grab_succes", gh.Id);
            }
            ScrAftemat.inst.SuccessGrabHub++;
            GrabInProgress = false;
        }
        else
        {
            
            ScrUILog.inst.AddMessage(ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.msgGrabHubFail) + ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.Key + Id), this);
            ScrAftemat.inst.FailGrabHub++;
            //ScrUIMessanger.inst.ShowMessage(ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.msgGrabHubFail) + ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.Key + Id));//пере
            GrabInProgress = false;
        }
    }

    public void Ungrab()
    {
        if (!ScanInProgress)
        {
            ScrUILog.inst.AddMessage(ScrLocalizationSystem.L_msgUngrabHub + ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.Key + Id), this);
            //ScrUIMessanger.inst.ShowMessage(ScrLocalizationSystem.L_msgUngrabHub);
            OwnerAI = false;
            if (IsCoreAI)
            {
                ScrAftemat.inst.GameOver();
            }
            IsCoreAI = false;
            virus = new Virus();
            NeedUpdateVisualization = true;
        }
    }

    public bool IsScanInProgress()
    {
        return ScanInProgress;
    }

    public bool IsGrabInProgress()
    {
        return GrabInProgress;
    }

    public bool IsProtectActive(int num)
    {
        return ProtectActivate[num];
    }

    public int GetTimeToEndScan()
    {
        return (int)TimeToEndScan;
    }

    public int GetTimeToEndGrab()
    {
        return (int)TimeToEndGrab;
    }

    public bool IsScanned()
    {
        return Scanned;
    }

    public bool CoreAI()
    {
        return IsCoreAI;
    }

    public bool CoreEAI()
    {
        return IsCoreEAI;
    }

    public int DistanceToAICore()
    {
        return DistToAI;
    }

    public Virus GetVirusInfo()
    {
        return virus;
    }

    public void ClickOnHub()
    {
        ScrUIController.inst.ShowUIHub(this);
    }

    public void SetTrojan(TrojanInServer t, int s)
    {
        if(servers[s].CoolDown>0)
        {
            return;
        }
        float rndm = Random.value;
        float dL = servers[s].Lvl / (t.Lvl + net.BaseNetParametrs.GK1);
        float ch = net.BaseNetParametrs.GK2 / dL;
        ch = ch / servers[s].Defens;
        if (ch < rndm)
        {
            ScrUILog.inst.AddMessage(ScrLocalizationSystem.GetLocalString("#msg_trojan_set_fail"), this);
            //ScrUIMessanger.inst.ShowMessage("Установка трояна провалилась, шанс установки был равен: " + ch.ToString());
            ScrAlertSystem.Inst.AddAlert(ScrAlertSystem.Inst.TFSAlert);
            return;
        }
        servers[s].trojan = t;
        ScrTrojanController.inst.AddTrojan();
        servers[s].AddRes = new GameResAdd[servers[s].Reses.Length];
        int c = 0;
        foreach (var r in servers[s].Reses)
        {
            servers[s].AddRes[c] = new GameResAdd();
            servers[s].AddRes[c].Id = r.Id;
            servers[s].AddRes[c].EndAdd = Random.Range(r.Min,r.Max+1) / 2;
            servers[s].AddRes[c].TimeAdd = t.MaxLifeTime / servers[s].AddRes[c].EndAdd;
            servers[s].AddRes[c].Progress = 0;
            c++;
        }

        ScrUILog.inst.AddMessage(ScrLocalizationSystem.GetLocalString("#msg_trojan_set_succes"), this);
        ScrVirusKonstructor.inst.FindPcsInServ();
        TimetoCheckTrojan = net.BaseNetParametrs.TTrojanCheck;
    }
}
