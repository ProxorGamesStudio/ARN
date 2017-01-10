using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log_adaptation : MonoBehaviour {
	public GameObject logmessage;

	// Use this for initialization
	void Start () {


	}

	// Update is called once per frame
	void Update () {
		
		if (logmessage.transform.localScale.x < 1f) {
			Debug.Log ("pam");
			logmessage.transform.localScale += new Vector3(0.47f,0.47f,0.47f);
		}	
	}
}
