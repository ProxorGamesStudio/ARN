using UnityEngine;
using UnityEngine.UI;

public class ScrUIServer : MonoBehaviour {
    
    public Text Lvl;
    public Text Type;
    public Text State;
    public Text[] Reses;

    private string LblLvl;
    private string LblType;
    private string LblState;
    private ScrHub hub;
    private int serv;


    public void Start()
    {
        gameObject.SetActive(false);
    }


    public void Load()
    {
        LblLvl = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.LblLvl);//
        LblType = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.LblType);//
            }

    public void SetServ(ScrHub h, int s)
    {
        //serv = s;
        if (Lvl)
            Lvl.text = LblLvl + h.servers[s].Lvl.ToString();
        if(Type)
        {
            Type.text = LblType + ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.idsLocaltype[h.servers[s].Type]);//
        }

        int c = 0;
        foreach(Text t in Reses)
        {
            c++;
            t.gameObject.SetActive(!(c > h.servers[s].Reses.Length));
        }

        c = 0;
        foreach(GameResSet r in h.servers[s].Reses)
        {
            Reses[c].text = r.Max.ToString();//
            c++;
        }

        hub = h;
        serv = s;
    }

    void Update()
    {
        if (hub)
        {
            if (hub.servers[serv].CoolDown > 0)
            {
                State.text = LblState + ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.IdStCD) + "(" + ((int)hub.servers[serv].CoolDown).ToString() + ")";//
            }
            else
                if (hub.servers[serv].trojan.Id != "non")
            {
                State.text = LblState + ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.IdStInfe);//
            }
            else
            {
                State.text = LblState + ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.IdStNorm);//
            }
        }
    }
}
