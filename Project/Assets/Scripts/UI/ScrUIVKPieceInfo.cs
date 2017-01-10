using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrUIVKPieceInfo : MonoBehaviour {

    public Text Name;
    public Text[] Parametrs;

    public string piece;
    public ScrUIVK UIVK;

    public void SetPiece(PieceOfVirus p)
    {
        piece = p.id;
        Name.text = ScrLocalizationSystem.GetLocalString(piece);
        foreach (var t in Parametrs)
        {
            t.gameObject.SetActive(false);
        }
        int c = 0;
        if (p.GrabTimeFaktor != 1 && c < Parametrs.Length)
        {
            Parametrs[c].gameObject.SetActive(true);
            Parametrs[c].text = ScrLocalizationSystem.L_LblGrabTime + "x" + p.GrabTimeFaktor.ToString();
            c++;
        }
        if (p.GrabTimeSum != 0 && c < Parametrs.Length)
        {
            Parametrs[c].gameObject.SetActive(true);
            Parametrs[c].text = ScrLocalizationSystem.L_LblGrabTime + "+" + p.GrabTimeSum.ToString();
            c++;
        }
        if (p.AntivirusCrackChanceFaktor != 1 && c < Parametrs.Length)
        {
            Parametrs[c].gameObject.SetActive(true);
            Parametrs[c].text = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.idsHubProtectType[0]) + "x" + p.AntivirusCrackChanceFaktor.ToString();
            c++;
        }
        if (p.AntivirusCrackChanceSum != 0 && c < Parametrs.Length)
        {
            Parametrs[c].gameObject.SetActive(true);
            Parametrs[c].text = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.idsHubProtectType[0]) + "+" + p.AntivirusCrackChanceSum.ToString();
            c++;
        }
        if (p.FirewallCrackChanceFaktor != 1 && c < Parametrs.Length)
        {
            Parametrs[c].gameObject.SetActive(true);
            Parametrs[c].text = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.idsHubProtectType[1]) + "x" + p.FirewallCrackChanceFaktor.ToString();
            c++;
        }
        if (p.FirewallCrackChanceSum != 0 && c < Parametrs.Length)
        {
            Parametrs[c].gameObject.SetActive(true);
            Parametrs[c].text = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.idsHubProtectType[1]) + "+" + p.FirewallCrackChanceSum.ToString();
            c++;
        }
        if (p.AdmincontrolCrackChanceFaktor != 1 && c < Parametrs.Length)
        {
            Parametrs[c].gameObject.SetActive(true);
            Parametrs[c].text = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.idsHubProtectType[0]) + "x" + p.AdmincontrolCrackChanceFaktor.ToString();
            c++;
        }
        if (p.AdmincontrolCrackChanceSum != 0 && c < Parametrs.Length)
        {
            Parametrs[c].gameObject.SetActive(true);
            Parametrs[c].text = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.idsHubProtectType[0]) + "+" + p.AdmincontrolCrackChanceSum.ToString();
            c++;
        }
        if (p.GrabThreatFaktor != 1 && c < Parametrs.Length)
        {
            Parametrs[c].gameObject.SetActive(true);
            Parametrs[c].text = ScrLocalizationSystem.L_LblThreat + "x" + p.GrabThreatFaktor.ToString();
            c++;
        }
        if (p.GrabThreatSum != 0 && c < Parametrs.Length)
        {
            Parametrs[c].gameObject.SetActive(true);
            Parametrs[c].text = ScrLocalizationSystem.L_LblThreat + "+" + p.GrabThreatSum.ToString();
            c++;
        }
        if (p.DisquiseFaktor != 1 && c < Parametrs.Length)
        {
            Parametrs[c].gameObject.SetActive(true);
            Parametrs[c].text = ScrLocalizationSystem.L_LblDisq + "x" + p.DisquiseFaktor.ToString();
            c++;
        }
        if (p.DisquiseSum != 0 && c < Parametrs.Length)
        {
            Parametrs[c].gameObject.SetActive(true);
            Parametrs[c].text = ScrLocalizationSystem.L_LblDisq + "+" + p.DisquiseSum.ToString();
            c++;
        }
        if (p.AddGrabHubSum != 0 && c < Parametrs.Length)
        {
            Parametrs[c].gameObject.SetActive(true);
            Parametrs[c].text = ScrLocalizationSystem.L_LblAddGrabHub + "+" + p.AddGrabHubSum.ToString();
            c++;
        }
        if (p.CostFaktor != 1 && c < Parametrs.Length)
        {
            Parametrs[c].gameObject.SetActive(true);
            Parametrs[c].text = ScrLocalizationSystem.L_LblCost + "x" + p.CostFaktor.ToString();
            c++;
        }
        foreach (var r in p.CostSum)
        {
            if (r.count != 0)
            {
                Parametrs[c].gameObject.SetActive(true);
                Parametrs[c].text = ScrLocalizationSystem.GetLocalString(r.Id) + " +" + r.count.ToString();
                c++;
            }
        }
    }

    public void Btn()
    {
        UIVK.AddPiece(piece);
    }
}
