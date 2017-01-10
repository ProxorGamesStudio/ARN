using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balancer : MonoBehaviour {

    public float TimerMin, TimerMax;
    float timer;
    bool active;
    public ScrEAI eai;

    void Start()
    {
        timer = Random.Range(TimerMin, TimerMax);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            eai.StartDeactive = false;
            Destroy(this);
        }
    }
}
