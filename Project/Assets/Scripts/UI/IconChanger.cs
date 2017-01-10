using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconChanger : MonoBehaviour {

	public GameObject money_Icon;
	public GameObject data_Icon;
	public GameObject PSI_Icon;
	public GameObject PMI_Icon;

	public Text Cost;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Cost.text == ScrLocalizationSystem.GetLocalString("#res_money"))
		{
			money_Icon.SetActive(true);
			data_Icon.SetActive(false);
			PSI_Icon.SetActive(false);
			PMI_Icon.SetActive(false);
		}

		if (Cost.text == ScrLocalizationSystem.GetLocalString ("#res_data")) 
		{
			money_Icon.SetActive(false);
			data_Icon.SetActive(true);
			PSI_Icon.SetActive(false);
			PMI_Icon.SetActive(false);
		}

		if (Cost.text == ScrLocalizationSystem.GetLocalString ("#res_PSI")) 
		{
			money_Icon.SetActive(false);
			data_Icon.SetActive(false);
			PSI_Icon.SetActive(true);
			PMI_Icon.SetActive(false);
		}

		if (Cost.text == ScrLocalizationSystem.GetLocalString ("#res_PMI")) 
		{
			money_Icon.SetActive(false);
			data_Icon.SetActive(false);
			PSI_Icon.SetActive(false);
			PMI_Icon.SetActive(true);
		}

		
	}
}
