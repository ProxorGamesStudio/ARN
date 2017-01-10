using UnityEngine;
using System.Collections;

public class ScrMMAnim1 : MonoBehaviour {

    public Transform[] Points;
    public float LiveTime = 10f;
    public float TimeBetwen = 10f;
    public float DieTime = 1.5f;
    public GameObject SelectPrefab;

    private GameObject instselect;
    private float timer;
    
	void Start () {
        timer = TimeBetwen / 2;
    }
	
	void Update () {
        if (instselect)
            instselect.GetComponent<RectTransform>().LookAt(new Vector3(instselect.GetComponent<RectTransform>().position.x, instselect.GetComponent<RectTransform>().position.y, 10));
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            if(instselect)
            {
                instselect.GetComponentInChildren<ScrMMAnimRSC>().die();
                Destroy(instselect, DieTime);
                timer = TimeBetwen;
            }
            else
            {
                instselect = Instantiate(SelectPrefab);
                instselect.transform.SetParent(Points[Random.Range(0, Points.Length)]);
                instselect.GetComponent<RectTransform>().localPosition = Vector3.zero;
                instselect.GetComponent<Canvas>().worldCamera = Camera.main;
                timer = LiveTime;
            }
        }
    }
}
