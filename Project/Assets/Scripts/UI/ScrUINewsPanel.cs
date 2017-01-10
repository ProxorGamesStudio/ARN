using UnityEngine;
using System.Collections;

public class ScrUINewsPanel : MonoBehaviour {
    public static ScrUINewsPanel inst;
    public ScrUINewsMessage prefabNews;

    public Transform List;

    private int CountNews;

    void Awake()
    {
        inst = this;
        CountNews = 0;
    }
	
    public void ShowHidePanel()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }


	void Update () {
	    if(ScrNewsSystem.inst.loadNews.Count > CountNews)
        {
            ScrUINewsMessage NM = Instantiate(prefabNews);
            NM.transform.SetParent(List);
            NM.Message.text = ScrLocalizationSystem.GetLocalString(ScrNewsSystem.inst.loadNews[CountNews]);
            CountNews++;
        }
	}
}
