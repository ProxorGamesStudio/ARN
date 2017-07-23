using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public enum WeAre { FreeAI, GovernmentAI };
    public WeAre weAre;
    public int Money, Information, Research;
    public Text moneyUI, informationUI, researchUI;
    [HideInInspector]
    public List<MonoBehaviour> Scripts;
    public int Defucult;
    #region params
    Vector2 screenSize;
    #endregion

    // Use this for initialization
    void Start () {
        screenSize = new Vector2(Screen.width, Screen.height);
	}
	
	// Update is called once per frame
	void Update () {
        moneyUI.text = Money.ToString();
        informationUI.text = Information.ToString();
        researchUI.text = Research.ToString();
	}

    void CheckScreenChange()
    {
        foreach(MonoBehaviour script in Scripts)
            script.Invoke("ChangeScreen", 0);
    }
}
