using UnityEngine;
using UnityEngine.UI;

public class ScrMMAnimRSC : MonoBehaviour {

    public Text t;
    public float TChngV = 0.1f;

    private float timer;

	void Start () {
        t = GetComponentInChildren<Text>();
        timer = TChngV;
	}
	
	
	void Update () {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            //t.text = "0x" + ((int)(Random.value * 100000000)).ToString("X8");
            t.text = "0x" + ((int)(Time.time*100000+gameObject.GetInstanceID()*10)).ToString("X8");
            timer = TChngV;
        }
	}

    public void die()
    {
        GetComponent<Animator>().SetBool("die", true);
    }
}
