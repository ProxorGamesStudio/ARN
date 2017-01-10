using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScrUILog : MonoBehaviour {
    public static ScrUILog inst;

    public ScrUILogMessage MessagePrefab;

    public Transform List;

    private bool NeedScrollDown;
    private ScrollRect sr;
   
    void Awake()
    {
        inst = this;
        ScrollRect sr = GetComponent<ScrollRect>();
        NeedScrollDown = false;
    }

    public void AddMessage(string text)
    {
        ScrUILogMessage NM = Instantiate(MessagePrefab);
        NM.transform.SetParent(List);
        NM.transform.SetSiblingIndex(0); 
        NM.Message.text = text;
	//	NM.transform.GetComponent<RectTransform> ().rect = new Rect (NM.transform.GetComponent<RectTransform> ().rect.position.x,NM.transform.GetComponent<RectTransform> ().rect.position.y,Screen.width/4,Screen.height/15);
        NeedScrollDown = true;
    }

    public void AddMessage(string text, ScrHub hub)
    {
        ScrUILogMessage NM = Instantiate(MessagePrefab);
        NM.transform.SetParent(List);
        NM.transform.SetSiblingIndex(0);
        NM.Message.text = text;
	//	NM.transform.GetComponent<RectTransform> ().rect = new Rect (NM.transform.GetComponent<RectTransform> ().rect.position.x,NM.transform.GetComponent<RectTransform> ().rect.position.y,Screen.width/4,Screen.height/15);
        NM.hub = hub;
        NeedScrollDown = true;
    }

    void Update()
    {
     

        /*if(NeedScrollDown)
        {
            sr.verticalNormalizedPosition = 0;
            NeedScrollDown = false;
        }*/
    }

   
}
