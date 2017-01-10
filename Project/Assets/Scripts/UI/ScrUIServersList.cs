using UnityEngine;
using UnityEngine.UI;

public class ScrUIServersList : MonoBehaviour {

    public Button[] ServBtns;
    public ScrUIServer UIServ;
    public ScrUITrojanList UITrojan;

    public ScrHub hub;

    public Sprite Type1, Type1Active, Type2, Type2Active, Type3, Type3Active, Type4, Type4Active, Type5, Type5Active, Type6, Type6Active;
    public Button[] serverButton;
   

    public void Show(ScrHub hub)
    {
        foreach(Button b in ServBtns)
        {
            b.gameObject.SetActive(true);
        }
        int c = ServBtns.Length;
        while(c > hub.servers.Length)
        {
            c--;
            ServBtns[c].gameObject.SetActive(false);
        }
        gameObject.SetActive(true);
        this.hub = hub;
    }

    public void ShowServ(int id)
    {
      
        UIServ.SetServ(hub,id);
       
        if(hub.OwnerAI && hub.servers[id].trojan.Id == "non")
        {
          
            UITrojan.hub = hub;
            UITrojan.serverID = id;
        }
    }

    public void InitServers()
    {
        for (int i = 0; i < hub.servers.Length; i++)
        {
            if (hub.servers[i].Type == 0)            
                serverButton[i].targetGraphic.GetComponent<Image>().sprite = Type1;       
            if (hub.servers[i].Type == 1)
                serverButton[i].targetGraphic.GetComponent<Image>().sprite = Type2;
            if (hub.servers[i].Type == 2)
                serverButton[i].targetGraphic.GetComponent<Image>().sprite = Type3;
            if (hub.servers[i].Type == 3)
                serverButton[i].targetGraphic.GetComponent<Image>().sprite = Type4;


        }
    }

    void Update()
    {
        
    }
}
