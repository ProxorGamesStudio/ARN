using UnityEngine;
using UnityEngine.UI;

public class ScrUIHub : MonoBehaviour {

    public Text Name, Name2, Name3;
    public Text Type, Type2;
    public Text Core;
    public Text Lvl, Lvl2;
    public Text[] Protects;
    
    public string LblType;
    public string LblLvl;
    public string LblAICore;
    public string LblEAICore;
    public string LblBtnScan;
    public string LblBtnGrab;
    public string LblBtnVirus;
    public string LblBtnAttack;
    public int ev_hub_level_change;
    public float ev_hub_level_change_time;
    float tDown;
    bool active_ev_hub_level_change;
    bool active_ev_resources_increase;
    public Button btnInter;
    public Button btnSetAICore;
    public Text Pieces;

    //public ScrUIServersList ServersList;
    public ScrUIVirusList VirusList;
    public ScrUIVirusonhubInfo VirusInfo;
    
    private ScrHub SelectedHub;
    private int BtnInterFunc;
    private bool NeedUpdate;
    public ScrHub currnetHub;
    public Button mainButton;
    public GameObject progressbarMainCore, progressScan, progessGrub, CoreHub, UnscanHub, NormalHub;
    public Text costScan, ScanPersent, GrubPersent;
    public Image ScanPercentViz, GrubPersentViz;
    public ScrUIServersList Slist;
    bool show;
    [HideInInspector]
    public Vector2 startPos, pos, scale;
    [HideInInspector]
    public ScrHub hub;
    public bool _show;
    public int VirusNum;

    void Start()
    {
        startPos = transform.position;
        transform.localScale = Vector3.zero;
    }

    public void Ev_hub_level_change()
    {
        active_ev_hub_level_change = true;
    }

    void Ev_resources_increase()
    {
        active_ev_resources_increase = true;
    }


    public void GetNum(int num)
    {
        VirusNum = num;
    }

    public void HideHub()
    {
        scale = Vector3.zero;
        
        _show = false;
    }

    public void SetHub(ScrHub h)
    {
        currnetHub = h;
        NeedUpdate = false;
        SelectedHub = h;
        Name.text = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.Key + h.Id);
        btnSetAICore.gameObject.SetActive(false);
        Core.gameObject.SetActive(false);
        hub = h;
        _show = true;
        foreach (var t in Protects)
        {
            t.gameObject.SetActive(false);
        }

        Pieces.text = "Кирпичики: " + h.pieceText;

        if (!h.IsScanned())
        {
            if (h.IsScanInProgress())
            {
                btnInter.GetComponentInChildren<Text>().text = h.GetTimeToEndScan().ToString();
               // btnInter.interactable = false;
                NeedUpdate = true;
            }
            else
            {
                btnInter.GetComponentInChildren<Text>().text = LblBtnScan;
               // btnInter.interactable = true;
                BtnInterFunc = 0;
            }
        }
        else
        {
            if (!h.OwnerAI)
            {
                if (h.CoreEAI())
                {
                    btnInter.GetComponentInChildren<Text>().text = LblBtnAttack;
                  //  btnInter.interactable = true;
                    BtnInterFunc = 3;
                    Core.gameObject.SetActive(true);
                    Core.text = LblEAICore;
                }
                else if (h.IsGrabInProgress())
                {
                    btnInter.GetComponentInChildren<Text>().text = h.GetTimeToEndGrab().ToString();
                  //  btnInter.interactable = false;
                    NeedUpdate = true;
                }
                else
                {
                    btnInter.GetComponentInChildren<Text>().text = LblBtnGrab;
                   // btnInter.interactable = true;
                    BtnInterFunc = 1;
                    NeedUpdate = true;
                }
                int ct = 0;
                int c = 0;
                foreach (var p in h.Protects)
                {
                    if (ct > Protects.Length - 1)
                    {
                        break;
                    }
                    if (p)
                    {
                        Protects[ct].gameObject.SetActive(true);
                        Protects[ct].text = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.idsHubProtectType[c]);//
                        ct++;
                    }
                    c++;
                }
            }
            else
            {
                if (SelectedHub.DistanceToAICore() <= ScrAI.inst.DefultMaxDistanceRemove && !h.CoreAI() && !ScrAI.inst.RmvAIcrInCD() && ScrAI.inst.IsRmvAIcrInPrgrs() <= 0)
                {
                    btnSetAICore.gameObject.SetActive(true);
                }
                if (h.CoreAI())
                {
                    Core.gameObject.SetActive(true);
                    Core.text = LblAICore;
                }
                btnInter.GetComponentInChildren<Text>().text = LblBtnVirus;
               // btnInter.interactable = true;
                BtnInterFunc = 2;
            }
        }
        if (h.IsScanned())
        {
            if (h.Type < 0 || h.Type > ScrLocalizationSystem.inst.idsLocaltype.Length)
            {
                Debug.LogError("Ошибка типа узла " + h.Id);
            }
            else
            {
                Type.text = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.idsLocaltype[h.Type]);//
            }
            Lvl.text = h.DefenceLvl.ToString();
        }
        else
        {
            Type.text =  ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.LblUnknow);//
            Lvl.text = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.LblUnknow);//
        }
    }

    public void BtnInter()
    {
        NeedUpdate = true;
        switch(BtnInterFunc)
        {
            case 0:
                BtnScan();
                break;
            case 1:
                BtnGrab();
                break;
            case 2:
                BtnSetAICore();
                break;
            default:
                break;
        }
    }

    public void BtnScan()
    {
        bool b = true;
        foreach (var r in ScrAI.inst.CostScanHub)
        {
            GameResStor br = new GameResStor(r.Id, r.count);
            br.count = (int)(r.count + (r.count * (SelectedHub.DistanceToAICore() - 1) * ScrAI.inst.CostScanHubK));
            if (ScrResesController.inst.GetResInfo(br.Id) < br.count)
            {
                b = false;
                ScrUIMessanger.inst.ShowMessage(ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.msg_defuse_res));//
                break;
            }
        }
        if (SelectedHub && b)
        {
            SelectedHub.StartScan();
        }
    
    }

    public void BtnGrab()
    {
        VirusList.gameObject.SetActive(!VirusList.gameObject.activeSelf);
    }

    public void BtnVirusInfo()
    {
        VirusInfo.gameObject.SetActive(!VirusInfo.gameObject.activeSelf);
    }

    public void BtnSetAICore()
    {
        bool b = true;
        foreach(var r in ScrAI.inst.CostMoveCoreAI)
        {
            GameResStor br = new GameResStor(r.Id, r.count);
            br.count = (int)(r.count + (r.count * (SelectedHub.DistanceToAICore() - 1) * ScrAI.inst.CostMoveCoreAIK));
            if (ScrResesController.inst.GetResInfo(br.Id) < br.count)
            {
                b = false;
                break;
            }
        }
        if (b)
        {
            ScrAI.inst.StrtRmvCrAI(SelectedHub);
        }
        else
        {
            ScrUIMessanger.inst.ShowMessage(ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.msg_defuse_res));//
        }
    }

    public void Load()
    {
        /**/
        LblType = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.LblType);
        LblLvl = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.LblLvl);
        LblAICore = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.AICoreHere);
        LblEAICore = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.EAICoreHere);
        LblBtnScan = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.LblBtnScan);
        LblBtnGrab = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.LblBtnGrab);
        LblBtnAttack = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.LblBtnAttack);
        LblBtnVirus = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.LblBtnVirus);
        /**/
        BtnInterFunc = -1;
        NeedUpdate = false;
        VirusList = ScrUIController.inst.UIVirusList;
        VirusInfo = ScrUIController.inst.UIVirusonhubInfo;
        progessGrub.SetActive(false);
        progressScan.SetActive(false);
        CoreHub.SetActive(false);
        UnscanHub.SetActive(false);
        NormalHub.SetActive(false);
    }
	
	void Update () {

        if (!_show && hub != null)
            pos = Camera.main.WorldToScreenPoint(hub.transform.position);
        transform.position = Vector2.Lerp(transform.position, pos, 8 * Time.deltaTime);
        transform.localScale = Vector2.Lerp(transform.localScale, scale, 8 * Time.deltaTime);

        if (NeedUpdate)
        {
            SetHub(SelectedHub);
        }

        Name2.text = Name3.text = Name.text;
        Lvl2.text = Lvl.text;
        Type2.text = Type.text;

        if (currnetHub != null)
        {
            if (!currnetHub.IsScanInProgress() && !currnetHub.IsGrabInProgress())
            {
                
                if (currnetHub.OwnerAI)
                {

                    progressbarMainCore.SetActive(false);
                    UnscanHub.SetActive(false);
                    CoreHub.SetActive(true);
                    NormalHub.SetActive(false);
                    progressScan.SetActive(false);
                    if (currnetHub.CoreAI())
                    {
                        mainButton.enabled = false;
                        mainButton.GetComponent<Image>().color = Color.grey;
                    }
                    else
                    {
                        mainButton.enabled = true;
                        mainButton.GetComponent<Image>().color = Color.white;
                    }
                    if (!show)
                    {
                        foreach (Button btn in Slist.serverButton)
                        {
                            btn.interactable = true;
                        }
                        show = true;
                    }
                }
                else
                {
                    if (!currnetHub.Scanned)
                    {
                        UnscanHub.SetActive(true);
                        CoreHub.SetActive(false);
                        NormalHub.SetActive(false);
                        mainButton.enabled = true;
                        mainButton.GetComponent<Image>().color = Color.white;
                        progressScan.SetActive(true);
                        foreach (Button btn in Slist.serverButton)
                        {
                            btn.gameObject.SetActive(false);
                        }
                        show = true;
                        costScan.text = "";
                        if (ScrAI.inst.CostScanHub.Length > 0)
                        costScan.text += ScrAI.inst.CostScanHub[0].count.ToString() + " " + ScrLocalizationSystem.GetLocalString("#res_data") + " ";
                        if (ScrAI.inst.CostScanHub.Length > 1)
                            costScan.text += ScrAI.inst.CostScanHub[1].count.ToString() + " " + ScrLocalizationSystem.GetLocalString("#res_money") + " ";
                        if (ScrAI.inst.CostScanHub.Length > 2)
                            costScan.text += ScrAI.inst.CostScanHub[2].count.ToString() + " " + ScrLocalizationSystem.GetLocalString("#res_PMI") + " ";
                        if (ScrAI.inst.CostScanHub.Length > 3)
                            costScan.text += ScrAI.inst.CostScanHub[3].count.ToString() + " " + ScrLocalizationSystem.GetLocalString("#res_PSI") + " ";
                    }
                    else
                    {
                        if (show)
                        {
                            Slist.Show(currnetHub);
                            Slist.hub = currnetHub;
                            Slist.InitServers();
                            show = false;
                            foreach (Button btn in Slist.serverButton)
                            {
                                btn.interactable = false;
                            }
                        }
                        mainButton.enabled = true;
                        mainButton.GetComponent<Image>().color = Color.white;
                        UnscanHub.SetActive(false);
                        CoreHub.SetActive(false);
                        NormalHub.SetActive(true);
                        progressbarMainCore.SetActive(true);
                        Slist.gameObject.SetActive(true);
                    }
                }
                progressScan.SetActive(false);
                progessGrub.SetActive(false);
            }
            else
            {
                if (currnetHub.IsScanInProgress())
                {
                    mainButton.enabled = false;
                    mainButton.GetComponent<Image>().color = Color.grey;
                    progressScan.SetActive(true);
                    float percent = 1 - currnetHub.ScanTime / 100 * currnetHub.TimeToEndScan;
                    ScanPercentViz.fillAmount = percent;
                    ScanPersent.text = Mathf.Floor(percent * 100).ToString() + "%";
                    costScan.text = "";
                    if (ScrAI.inst.CostScanHub.Length > 0)
                        costScan.text += ScrAI.inst.CostScanHub[0].count.ToString() + " " + ScrLocalizationSystem.GetLocalString("#res_data") + " ";
                    if (ScrAI.inst.CostScanHub.Length > 1)
                        costScan.text += ScrAI.inst.CostScanHub[1].count.ToString() + " " + ScrLocalizationSystem.GetLocalString("#res_money") + " ";
                    if (ScrAI.inst.CostScanHub.Length > 2)
                        costScan.text += ScrAI.inst.CostScanHub[2].count.ToString() + " " + ScrLocalizationSystem.GetLocalString("#res_PMI") + " ";
                    if (ScrAI.inst.CostScanHub.Length > 3)
                        costScan.text += ScrAI.inst.CostScanHub[3].count.ToString() + " " + ScrLocalizationSystem.GetLocalString("#res_PSI") + " ";
                }
                if(currnetHub.IsGrabInProgress())
                {
                    mainButton.enabled = false;
                    mainButton.GetComponent<Image>().color = Color.grey;
                    progessGrub.SetActive(true);
                    float percent =  1 - (currnetHub.TimeToEndGrab / (currnetHub.GrubTime / 100))/100;
                    GrubPersentViz.fillAmount = percent;
                    GrubPersent.text = Mathf.Floor(percent*100).ToString() + "%";
                }
            }
        }

       
    }
}
