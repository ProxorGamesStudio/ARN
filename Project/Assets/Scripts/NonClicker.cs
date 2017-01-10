using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonClicker : MonoBehaviour {

    public GameObject Target;
	
	// Update is called once per frame
	void Update () {
        if(FindObjectOfType<ScrUIHub>()._show  && Target.activeInHierarchy)
           gameObject.GetComponent<Collider>().enabled = true;
        else
           gameObject.GetComponent<Collider>().enabled = false;
    }
}
