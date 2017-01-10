using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event_differenticon : MonoBehaviour {
	public Image img;
	public GameObject D_nonclick;
	public Text txt;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (txt.text == "-") {
			
			D_nonclick.SetActive (true);

		} 

		else 
		{

			D_nonclick.SetActive (false);
		}

	}
}
