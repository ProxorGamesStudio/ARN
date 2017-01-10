using UnityEngine;
using UnityEngine.UI;

public class ScrMMAnimTimer : MonoBehaviour {

    public Text t;
    private int s;
    private float ms;

	void Start () {
        t = GetComponent<Text>();
        s = 0;
        ms = 0;
	}
	
	void Update () {
        ms += Time.deltaTime;
        if(ms>=1)
        {
            ms -= 1;
            s++;
        }
        t.text = s.ToString("D5") + "." + ((int)(ms*100)).ToString("D2");
	}
}
