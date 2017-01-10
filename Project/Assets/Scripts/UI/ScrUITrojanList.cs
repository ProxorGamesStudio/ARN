using UnityEngine;
using System.Collections;

public class ScrUITrojanList : MonoBehaviour {

    public ScrUITrojan UITrojanPrefab;

    public Transform List;

    public ScrHub hub;
    public int serverID;

    public void Start()
    {
        gameObject.SetActive(false);
    }

    public void SetTrojans()
    {
        foreach(var t in ScrTrojanController.inst.trojans)
        {
            ScrUITrojan iUIT = Instantiate(UITrojanPrefab);
            iUIT.transform.SetParent(List);
            iUIT.SetTrojan(t);
            iUIT.List = this;
        }
    }
    
    public void SetTrojanOnServ(TrojanItem t)
    {
        bool b = true;
        foreach(var r in t.Cost)
        {
            if(r.count > ScrResesController.inst.GetResInfo(r.Id))
            {
                b = false;
                ScrUIMessanger.inst.ShowMessage(ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.msg_defuse_res));//
                break;
            }
        }
        if(ScrTrojanController.inst.GetCount() >= ScrTrojanController.inst.MaxTrojanCount)
        {
            b = false;
            ScrUIMessanger.inst.ShowMessage(ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.msgMaxTrojans));//
        }
        if (b)
        {
            foreach (var r in t.Cost)
            {
                ScrResesController.inst.TakeRes(r);
            }
            hub.SetTrojan(new TrojanInServer(t), serverID);
            gameObject.SetActive(false);
        }
    }
}
