using UnityEngine;
using UnityEngine.UI;

public class ScrUIHubGrabConsol : MonoBehaviour {

    public Text HubName;
    public Text GrabTimer;
    public Text[] Info;

    private ScrHub hub;
    
    public void SetHub(ScrHub h)
    {
        hub = h;
        HubName.text = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.Key + h.Id);
    }

	void Update () {
        if (hub)
        {
            if (!hub.IsGrabInProgress())
            {
                gameObject.SetActive(false);
            }
            GrabTimer.text = ScrLocalizationSystem.L_LblGrabTime + hub.GetTimeToEndGrab();
            int c = 0;
            if (hub.IsProtectActive(0))
            {
                Info[c].gameObject.SetActive(true);
                Info[c].text = ScrLocalizationSystem.L_idsHubProtectType[0];
                c++;
            }
            if (hub.IsProtectActive(1))
            {
                Info[c].gameObject.SetActive(true);
                Info[c].text = ScrLocalizationSystem.L_idsHubProtectType[1];
                c++;
            }
            if (hub.IsProtectActive(2))
            {
                Info[c].gameObject.SetActive(true);
                Info[c].text = ScrLocalizationSystem.L_idsHubProtectType[2];
                c++;
            }
            while (c < Info.Length)
            {
                Info[c].gameObject.SetActive(false);
                c++;
            }
        }
    }
}
