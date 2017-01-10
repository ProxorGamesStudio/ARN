using UnityEngine;
using UnityEngine.UI;

public class ScrUILogMessage : MonoBehaviour {

    public GameObject Button;
    public Text Message;
    public ScrHub hub;

    void Start()
    {
        Button.SetActive(hub);
    }

    public void ShowHub()
    {
        ScrViewControl.inst.ShowPoint(hub.transform.position);
    }
}
