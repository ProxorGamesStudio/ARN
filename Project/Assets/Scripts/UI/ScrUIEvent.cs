using UnityEngine;
using UnityEngine.UI;

public class ScrUIEvent : MonoBehaviour {

    /**/
    public string BtnTxtPosEvent = "#Btn_Activate_Positive_Event";
    public string BtnTxtNegEvent = "#Btn_Deactivate_Negative_Event";
    public string TxtTimer = "#btn_event_timer";
    /**/

    public Text LblName;
    public Text LblDescription;
    public Text LblTimer;
    public Button BtnActivate;
    public Text[] Reses;

    public int NumEvent;
    public string IdEvent;

    public void SetEvent(int id)
    {
        NumEvent = id;
        IdEvent = ScrEventController.inst.turn[NumEvent].Id;
        var e = ScrEventController.inst.gameEvents.Find(x => x.Id == IdEvent);
        LblName.text = ScrLocalizationSystem.GetLocalString(IdEvent);
        LblDescription.text = ScrLocalizationSystem.GetLocalString(e.Description);
        int c = 0;
        foreach (var r in Reses)
        {
            c++;
            r.gameObject.SetActive(!(c > e.Cost.Count));
        }
        c = 0;
        foreach (var r in e.Cost)
        {
            Reses[c].text = ScrLocalizationSystem.GetLocalString(r.Id) + ": " + r.count.ToString();//пере
            c++;
        }
        if (e.IsPositive)
        {
            BtnActivate.GetComponentInChildren<Text>().text = ScrLocalizationSystem.GetLocalString(BtnTxtPosEvent);//
        }
        else
        {
            BtnActivate.GetComponentInChildren<Text>().text = ScrLocalizationSystem.GetLocalString(BtnTxtNegEvent);//
        }
    }
	
	void Update () {
        if (NumEvent < ScrEventController.inst.turn.Count)
        {
            if (ScrEventController.inst.turn[NumEvent].Id != IdEvent)
            {
                if (!ScrEventController.inst.turn.Exists(x => x.Id == IdEvent))
                    gameObject.SetActive(false);
                else
                    NumEvent = ScrEventController.inst.turn.FindIndex(x => x.Id == IdEvent);
            }
            else
                LblTimer.text = ScrLocalizationSystem.GetLocalString(TxtTimer) + ((int)ScrEventController.inst.turn[NumEvent].TimeToDie).ToString();//
        }
        else
            gameObject.SetActive(false);
    }

    public void BtnDone()
    {
        ScrEventController.inst.BuyEvent(NumEvent);
        gameObject.SetActive(false);
    }
}
