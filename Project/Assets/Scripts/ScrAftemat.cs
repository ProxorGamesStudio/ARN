using UnityEngine;
using System.Collections.Generic;

public class ScrAftemat : MonoBehaviour {
    public static ScrAftemat inst;

    public GameObject[] OffObjects;

    public List<GameResStor> AllReses;//
    public int SuccessGrabHub;
    public int FailGrabHub;
    public int CountCreatedVirus;
    public int TimeLife;
    public ScrUILog log;
    public void Awake()
    {
        inst = this;
    }

    public void AfterLoadDB()
    {
        SuccessGrabHub = 0;
        FailGrabHub = 0;
        CountCreatedVirus = 0;
        TimeLife = 0;
}

    public void GameOver()
    {
        foreach(var o in OffObjects)
        {
            o.SetActive(false);
        }
        log.enabled = false;
        TimeLife = (int)Time.time;
        ScrUIController.inst.UIAftermat.SetActive(true);
        foreach (ScrHub h in FindObjectsOfType<ScrHub>())
            h.enabled = false;
    }
}
