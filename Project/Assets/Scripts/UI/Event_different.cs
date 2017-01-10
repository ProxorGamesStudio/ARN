using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event_different : MonoBehaviour {
	public GameObject green;
	public GameObject red;
	public Text txt;
	public Text EvenText;
	public Text even1;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if (txt.text == "-") {

			EvenText.color =   Color.red;
			even1.color =   Color.red;

			red.SetActive (true);
			green.SetActive (false);
		} 

		else 
		{
			EvenText.color =   Color.green;
			even1.color =   Color.green;
			green.SetActive (true);
			red.SetActive (false);
		}
	
	}
}
