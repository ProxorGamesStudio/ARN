using UnityEngine;
using System.Collections;

public class ScrUIVirusList : MonoBehaviour {

    public ScrUIVirus UIVirusPrefab;

    public Transform List;

    public ScrHub hub;

    public void SetViruses()
    {
        //gameObject.SetActive(true);
        foreach (var t in ScrVirusController.inst.VirusBase)
        {
            ScrUIVirus iUIV = Instantiate(UIVirusPrefab);
            iUIV.transform.SetParent(List);
            iUIV.SetVirus(t);
            iUIV.List = this;
        }
    }

    public void AddNewVirus()
    {
        ScrUIVirus iUIV = Instantiate(UIVirusPrefab);
        iUIV.transform.SetParent(List);
        iUIV.SetVirus(ScrVirusController.inst.VirusBase[ScrVirusController.inst.VirusBase.Count-1]);
        iUIV.List = this;
    }

    public void SetVirusOnHub(Virus v)
    {
        hub.StartGrab(v);
       // ScrUIController.inst.HideHub();
       // ScrUIController.inst.ShowUIHub(hub);
        gameObject.SetActive(false);
    }
}
