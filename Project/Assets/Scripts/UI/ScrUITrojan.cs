using UnityEngine;
using UnityEngine.UI;

public class ScrUITrojan : MonoBehaviour {

    public Text LblId;
    public Text LblType;
    public Text LblLvl;
    public Text Cost;

    public Text[] Reses;
    public Text[] Dops;

    public ScrUITrojanList List;
    public TrojanItem trojan;

    private string StrLblType;
    private string StrLblLvl;
    int k = 5;

    public void SetTrojan(TrojanItem t)
    {
        trojan = t;
        LblId.text = ScrLocalizationSystem.GetLocalString(t.Id);
        LblType.text = StrLblType + ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.idsLocaltrojantype[t.Type]);
        LblLvl.text = StrLblLvl + t.Lvl.ToString();
        int c = 0;
        foreach(var r in Reses)
        {
            c++;
            r.gameObject.SetActive(!(c > t.Cost.Count));
        }
        c = 0;
        foreach(var r in t.Cost)
        {
            Reses[c].text = ScrLocalizationSystem.GetLocalString(r.Id) + ": " + r.count.ToString();
            c++;
        }

        if (t.trRes != 0)
        {
            Dops[k].text = ScrLocalizationSystem.GetLocalString("#trojan_res_ability") + ": " + t.trRes;
            k--;
        }
        if (t.tr_found != 0)
        {
            Dops[k].text = ScrLocalizationSystem.GetLocalString("#trojan_found_ability") + ": " + t.tr_found + "%";
            k--;
        }
        if (t.tr_trick != 0)
        {
            Dops[k].text = ScrLocalizationSystem.GetLocalString("#trojan_trick_ability") + ": " + t.tr_trick + "%";
            k--;
        }
        if (t.tr_hook != 0)
        {
            Dops[k].text = ScrLocalizationSystem.GetLocalString("#trojan_hook_ability") + ": " + t.tr_hook + "%";
            k--;
        }
        if (t.tr_time != 0)
        {
            Dops[k].text = ScrLocalizationSystem.GetLocalString("#trojan_time_ability") + ": " + t.tr_time + "%";
            k--;
        }
        if (t.tr_threat != 0)
        {
            Dops[k].text = ScrLocalizationSystem.GetLocalString("#trojan_threat_ability") + ": " + t.tr_threat + "%";
            k--;
        }


    }

    public void BtnInstTrojan()
    {
        List.SetTrojanOnServ(trojan);
    }
    
	void Start () {
        Cost.text = ScrLocalizationSystem.GetLocalString(Cost.name);//
        StrLblType = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.LblType);//
        StrLblLvl = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.LblLvl);//
    }
}
