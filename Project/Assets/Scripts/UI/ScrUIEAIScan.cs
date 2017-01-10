using UnityEngine;
using UnityEngine.UI;

public class ScrUIEAIScan : MonoBehaviour {
    
    public Color ClrOurBase = Color.blue;
    public Color ClrOurHub = Color.green;
    public Color ClrScanedHub = Color.white;
    public Color ClrUnscanedHub = Color.gray;

    public Text LblTimerText;
    public Text[] LblHubs;
    public Vector2 ShowPos;
    public Vector2 HidePos;

    public bool IsShow;

    private Vector2 Pos;

    public void StartScan()
    {
        foreach (Text t in LblHubs)
        {
            t.gameObject.SetActive(true);
        }
        int c = LblHubs.Length;
        while (c > ScrEAI.inst.ScanTurn.Count)
        {
            c--;
            LblHubs[c].gameObject.SetActive(false);
        }
        c = 0;
        foreach(ScrHub h in ScrEAI.inst.ScanTurn)
        {
            LblHubs[c].text = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.Key + h.Id);
            if(h.CoreAI())
            {
                LblHubs[c].color = ClrOurBase;
            }
            else if(h.OwnerAI)
            {
                LblHubs[c].color = ClrOurHub;
            }
            else if(h.IsScanned())
            {
                LblHubs[c].color = ClrScanedHub;
            }
            else
            {
                LblHubs[c].color = ClrUnscanedHub;
            }
            c++;
        }
        IsShow = true;
        gameObject.SetActive(true);
    }

    public void HideShow()
    {
        IsShow = !IsShow;
    }

    void Start()
    {
        Pos = GetComponent<RectTransform>().localPosition;
    }

    void Update()
    {
        LblTimerText.text = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.TimerEAIScan) + ScrEAI.inst.GetTimeToEndScan().ToString();//пере
        if(!ScrEAI.inst.GetScanInProgress())
        {
            gameObject.SetActive(false);
        }
        if(IsShow)
        {
            GetComponent<RectTransform>().localPosition = Pos + ShowPos;
        }
        else
        {
            GetComponent<RectTransform>().localPosition = Pos + HidePos;
        }
    }
}
