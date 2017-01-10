using UnityEngine;
using UnityEngine.UI;

public class ScrUIVirusonhubInfo : MonoBehaviour {

    public Text Name;
    public Text Disq;

    private string LblDisq;
    private string LblUnknow;
    private Virus v;

	public void SetVirus(Virus sv)
    {
        v = sv;
        if (v.id == "non")
        {
            Name.text = LblUnknow;
            Disq.text = LblDisq + LblUnknow;
        }
        else
        {
            Name.text = v.name;
            Disq.text = LblDisq + v.Disquise.ToString();
        }
    }

    public void Load()
    {
        LblDisq = ScrLocalizationSystem.L_LblDisq;
        LblUnknow = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.LblUnknow);//
    }
}
