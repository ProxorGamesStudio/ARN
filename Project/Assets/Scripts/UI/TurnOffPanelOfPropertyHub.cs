using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffPanelOfPropertyHub : MonoBehaviour {
	public GameObject button_server;
	public GameObject Panels;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Panels.SetActive (button_server.activeInHierarchy);
	}
}
