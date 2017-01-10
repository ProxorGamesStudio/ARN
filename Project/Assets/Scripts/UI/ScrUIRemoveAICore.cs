using UnityEngine;
using UnityEngine.UI;

public class ScrUIRemoveAICore : MonoBehaviour {

    public string TxtStatRmv;
    public string TxtStatCD;

    public Text LblStatus;

    public Vector2 ShowPos;
    public Vector2 HidePos;

    public bool IsShow;

    private Vector2 Pos;

    void Start()
    {
        Pos = GetComponent<RectTransform>().localPosition;
    }

    public void Load()
    {
        TxtStatRmv = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.TimerAICoreRemove);//
        TxtStatCD = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.TimerAICoreRemoveCooldown);//
    }
     
    public void HideShow()
    {
        IsShow = !IsShow;
    }

    public void RemoveIsStart()
    {
        gameObject.SetActive(true);
        IsShow = true;
    }

    void Update () {
	    if(ScrAI.inst.IsRmvAIcrInPrgrs() > 0)
        {
            LblStatus.text = TxtStatRmv + ((int)ScrAI.inst.IsRmvAIcrInPrgrs()).ToString();
        }
        else if(ScrAI.inst.RmvAIcrInCD())
        {
            LblStatus.text = TxtStatCD + ((int)ScrAI.inst.GetCDRmvAIcr()).ToString();
        }
        else
        {
            gameObject.SetActive(false);
        }
        if (IsShow)
        {
            GetComponent<RectTransform>().localPosition = Pos + ShowPos;
        }
        else
        {
            GetComponent<RectTransform>().localPosition = Pos + HidePos;
        }
    }
}
