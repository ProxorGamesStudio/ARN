using UnityEngine;
using UnityEngine.UI;

public class ScrUIController : MonoBehaviour {
    public static ScrUIController inst;

    public ScrUIHub UIHub;
    public ScrUIHubGrabConsol UIHubGrabLog;
    public ScrUIVirusList UIVirusList;
    public ScrUIServersList UIServList;
    public ScrUIServer UIServ;
    public ScrUITrojanList UITrjnList;
    public ScrUIEAIScan UIEAIScan;
    public ScrUIEventTurn UIEventTurn;
    public ScrUIEvent UIEvent;
    public ScrUIRemoveAICore UIRemoveAICore;
    public ScrUIVirusonhubInfo UIVirusonhubInfo;
    public ScrUIVK UIVK;
    public ScrUINewsPanel UINewsPanel;
    public GameObject UIAftermat;
    
    void Awake()
    {
        inst = this;
    }

	public void AfterLoadDB () {
        AutoLocaliz();
        UIHub.Load();
        UIRemoveAICore.Load();
        UIServ.Load();
        UIVirusonhubInfo.Load();
        UIHubGrabLog.gameObject.SetActive(false);
        UIEvent.gameObject.SetActive(false);
        UIVirusList.SetViruses();
        UIVirusList.gameObject.SetActive(false);
        UIVirusonhubInfo.gameObject.SetActive(false);
        UIVK.AfterLoadLocalization();
        UIVK.UpdatePieceList();
        UIVK.gameObject.SetActive(false);
        UINewsPanel.gameObject.SetActive(false);
        UIAftermat.SetActive(false);

    }
	
    public void AutoLocaliz()
    {
        Text[] Lbls = FindObjectsOfType<Text>();
        foreach(var l in Lbls)
        {
            if(l.name[0] == ScrLocalizationSystem.inst.Key)
            {
                string s = ScrLocalizationSystem.GetLocalString(l.name);
                if (s != null) l.text = s;
            }
            else
            {
                continue;
            }
        }
    }

    public void ShowUIHub(ScrHub h)
    {
        if (h.IsGrabInProgress())
        {
            UIHubGrabLog.SetHub(h);
            UIHubGrabLog.gameObject.SetActive(true);
        }
        else
        {
            UIHub.SetHub(h);
            if(UIHub.scale.y < 1)
            UIHub.transform.position = Camera.main.WorldToScreenPoint(h.transform.position);
            UIHub.scale = new Vector2(1, 1);
            UIHub.pos = UIHub.startPos;
            UIHub.hub = h;
            if (h.IsScanned())
            {
                UIServList.Show(h);
            }
            UIVirusList.hub = h;
            UIVirusonhubInfo.SetVirus(h.GetVirusInfo());
        }
        if (FindObjectOfType<ScrUIServersList>() != null)
        FindObjectOfType<ScrUIServersList>().InitServers();
    }

    public void Hide(GameObject o)
    {
        o.SetActive(false);
    }

    public void HideTrjnList()
    {
        UITrjnList.gameObject.SetActive(false);
    }

    public void HideServ()
    {
        HideTrjnList();
        UIServ.gameObject.SetActive(false);
    }

    public void HideServList()
    {
        HideServ();
        UIServList.gameObject.SetActive(false);
    }

    public void HideHub()
    {
        HideServList();
        UIHub.gameObject.SetActive(false);
        UIVirusList.gameObject.SetActive(false);
        UIVirusonhubInfo.gameObject.SetActive(false);
    }

    public void SwtchVK()
    {
        UIVK.gameObject.SetActive(!UIVK.gameObject.activeSelf);
    }

	void Update () {
	
	}
}
