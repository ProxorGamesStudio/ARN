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
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit) && Input.GetMouseButtonDown(0) && hub.transform == hit.transform.parent && !Input.GetMouseButtonDown(1))
                 hub.ClickOnHub();

       
    }
}
