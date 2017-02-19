using UnityEngine;
using System.Collections;

public class ScrHubVis : MonoBehaviour {

    public Renderer[] renderers;
    RaycastHit hit;
    public ScrHub hub;
    
	void Start () {
	    if(!hub)
        {
            hub = transform.parent.GetComponent<ScrHub>();
        }
	}

    void Update()
    {   
    }
}
