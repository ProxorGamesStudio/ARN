using UnityEngine;
using System.Collections;

public class ScrMMAnimVideo : MonoBehaviour {

    public float TimeLife = 5f;
    public float TimeStart = 7f;
    public float TimeDie = 1f;

    public Vector2 position;
    public Vector2 size;

    public GameObject prefab;

    private bool on;
    private GameObject inst;

	void Start () {
        on = false;
	}
	
	void Update () {
	    if(!on)
        {
            if(Time.time > TimeStart)
            {
                inst = Instantiate(prefab);
                inst.GetComponent<RectTransform>().SetParent(transform);
                inst.GetComponent<RectTransform>().localPosition = position;
                inst.GetComponent<RectTransform>().sizeDelta = size;
                on = true;
            }
        }
        else if(inst)
        {
            if(Time.time > TimeStart + TimeLife)
            {
                inst.GetComponent<Animator>().SetBool("die", true);
                Destroy(inst, TimeDie);
            }
        }
	}
}
