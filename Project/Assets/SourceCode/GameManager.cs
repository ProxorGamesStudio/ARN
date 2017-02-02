using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public enum WeAre { FreeAI, GovernmentAI };
    public WeAre weAre;
    public int Money, Information, Research;
    public Text moneyUI, informationUI, researchUI;
   
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        moneyUI.text = Money.ToString();
        informationUI.text = Information.ToString();
        researchUI.text = Research.ToString();
	}
}
