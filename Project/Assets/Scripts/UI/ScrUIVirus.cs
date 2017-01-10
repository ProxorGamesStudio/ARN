using UnityEngine;
using UnityEngine.UI;


public class ScrUIVirus : MonoBehaviour {

    public Text Name;
    public Text GrabTime;
    public Text GrabThreat;
    public Text[] ProtectCrackChance;
    public Text AddGrabHub;
    public Text Disq;
    public Text Cost;
    public Text[] Reses;

    public ScrUIVirusList List;
    public Virus virus;

    public void SetVirus(Virus v)
    {
        virus = v;
        Name.text = virus.name;
        GrabTime.text = ScrLocalizationSystem.L_LblGrabTime + virus.GrabTime.ToString();
        GrabThreat.text = ScrLocalizationSystem.L_LblThreat + virus.GrabThreat.ToString();
        ProtectCrackChance[0].text = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.idsHubProtectType[0]) + virus.AntivirusCrackChance.ToString();
        ProtectCrackChance[1].text = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.idsHubProtectType[1]) + virus.FirewallCrackChance.ToString();
        ProtectCrackChance[2].text = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.idsHubProtectType[2]) + virus.AdmincontrolCrackChance.ToString();
        AddGrabHub.text = virus.AddGrabHub.ToString();
        Disq.text = ScrLocalizationSystem.L_LblDisq + virus.Disquise.ToString();
        Cost.text = ScrLocalizationSystem.L_LblCost;

        int c = 0;
        foreach (var r in Reses)
        {
            c++;
            r.gameObject.SetActive(!(c > virus.Cost.Count));
        }
        c = 0;
        foreach (var r in virus.Cost)
        {
            Reses[c].text = ScrLocalizationSystem.GetLocalString(r.Id) + ": " + r.count.ToString();
            c++;
        }
    }

    public void BtnInstVirus()
    {
        List.SetVirusOnHub(virus);
    }
}
