using UnityEngine;
using UnityEngine.UI;

public class ScrUIEventTurn : MonoBehaviour {

    public Button[] btns;

    public ScrUIEvent UIEvent;

    private ScrEventController events;
    
    public void updateTurn()
    {
        if(!events)
        {
            events = ScrEventController.inst;
        }
        foreach(var b in btns)
        {
            b.gameObject.SetActive(false);
        }
        int c = 0;
        if (events.turn.Count > 0)
        {
            foreach (var e in events.turn)
            {
                btns[c].gameObject.SetActive(true);
                string s;
                if (events.gameEvents.Find(x => x.Id == e.Id).IsPositive)
                {
                    s = "+";
                }
                else
                {
                    s = "-";
                }
                btns[c].transform.GetComponentInChildren<Text>().text = s;
                c++;
            }
        }
    }

	void Start () {
        events = ScrEventController.inst;
        updateTurn();
	}
	
    public void EvntBtn(int i)
    {
        if (UIEvent.NumEvent == i && UIEvent.gameObject.activeSelf)
        {
            UIEvent.gameObject.SetActive(false);
        }
        else
        {
            UIEvent.gameObject.SetActive(true);
            UIEvent.SetEvent(i);
        }
        
    }

	void Update () {
	
	}
}
