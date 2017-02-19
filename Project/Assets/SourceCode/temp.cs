using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour {

    ScrHub[] hubs;
    Hub[] hbs;
	// Use this for initialization
	void Awake () {

        hubs = FindObjectsOfType<ScrHub>();
        

        for (int i = 0; i < hubs.Length; i++)
        {
            Destroy(hubs[i].GetComponent<BoxCollider>());
            Destroy(hubs[i].GetComponent<ScrViewsManager>());
            Destroy(hubs[i].GetComponentInChildren<ScrHubVis>());
            Destroy(hubs[i]);
        }
      
	}
	
	// Update is called once per frame
	void Update () {

    }
}
