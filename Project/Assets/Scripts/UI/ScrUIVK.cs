using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrUIVK : MonoBehaviour {

    public Text GrabTime;
    public Text Threat;
    public Text Disq;
    public Text AddHub;
    public Text Admin;
    public Text Firew;
    public Text Antivir;
    public Text[] Cost;
    public InputField Name;
    public Transform ListOfPiece;
    public ScrUIVKPieceInfo prefabPI;

    public ScrUIVKPlaceForPiece[] places;

    private string[] pieces;
    private int FirstPlace;

    private ScrVirusKonstructor konstr;

    private ScrUIVKPieceInfo[] instpi;

    public void AfterLoadLocalization () {
        konstr = ScrVirusKonstructor.inst;
        instpi = new ScrUIVKPieceInfo[0];
        pieces = new string[places.Length];
        ResetPlaceList();
	}
	
	public void UpdatePieceList ()
    {
        for(int p = instpi.Length-1; p>=0;p--)
        {
            Destroy(instpi[p].gameObject);
        }
        instpi = new ScrUIVKPieceInfo[konstr.pieces.Count];
        int c = 0;
        foreach(var p in konstr.pieces)
        {
            if (p.IsVisible)
            {
                instpi[c] = Instantiate(prefabPI);
                instpi[c].transform.SetParent(ListOfPiece);
                instpi[c].SetPiece(p);
                instpi[c].UIVK = this;
                c++;
            }
        }
    }

    public void UpdateVirInfo()
    {
        GrabTime.text = ScrLocalizationSystem.L_LblGrabTime + konstr.CreatedVirus.GrabTime.ToString();
        Threat.text = ScrLocalizationSystem.L_LblThreat + konstr.CreatedVirus.GrabThreat.ToString() ;
        Disq.text = ScrLocalizationSystem.L_LblDisq + konstr.CreatedVirus.Disquise.ToString();
        AddHub.text = ScrLocalizationSystem.L_LblAddGrabHub + konstr.CreatedVirus.AddGrabHub.ToString();
        Admin.text = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.idsHubProtectType[2]) + konstr.CreatedVirus.AdmincontrolCrackChance.ToString();
        Firew.text = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.idsHubProtectType[1]) + konstr.CreatedVirus.FirewallCrackChance.ToString();
        Antivir.text = ScrLocalizationSystem.GetLocalString(ScrLocalizationSystem.inst.idsHubProtectType[0]) + konstr.CreatedVirus.AntivirusCrackChance.ToString();
        int c = 0;
        foreach (var t in Cost)
        {
            if(c < konstr.CreatedVirus.Cost.Count)
            {
                t.gameObject.SetActive(true);
                t.text = ScrLocalizationSystem.GetLocalString(konstr.CreatedVirus.Cost[c].Id) + ": " + konstr.CreatedVirus.Cost[c].count.ToString();
            }
            else
            {
                t.gameObject.SetActive(false);
            }
            c++;
        }
    }

    public void ResetPlaceList()
    {
        int c = 0;
        foreach (var p in places)
        {
            p.gameObject.SetActive(false);
            pieces[c] = "non";
            c++;
        }
        FirstPlace = 0;
    }

    public void UpdatePlaceList()
    {
        int c = 0;
        FirstPlace = -1;
        foreach (var p in places)
        {
            if (pieces[c] == "non")
            {
                p.gameObject.SetActive(false);
                if (FirstPlace == -1) FirstPlace = c;
            }
            else
            {
                p.gameObject.SetActive(true);
                p.Name.text = ScrLocalizationSystem.GetLocalString(pieces[c]);
            }
            c++;
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void AddPiece(string id)
    {
        if (FirstPlace >= 0)
        {
            konstr.AddPiece(id);
            pieces[FirstPlace] = id;
            UpdateVirInfo();
            UpdatePlaceList();
        }
    }

    public void RemovePiece(int num)
    {
        konstr.RemovePiece(pieces[num]);
        pieces[num] = "non";
        UpdateVirInfo();
        UpdatePlaceList();
    }

    public void AddVirus()
    {
        if(konstr.CheckSimilar(konstr.CreatedVirus))
        {
            ScrUIMessanger.inst.ShowMessage(ScrLocalizationSystem.L_msgSimilarVirus);
            return;
        }
        konstr.CreatedVirus.name = Name.text;
        Name.text = "";
        konstr.DoneCreatedVirus();
        ScrUIController.inst.UIVirusList.AddNewVirus();
        UpdateVirInfo();
        ResetPlaceList();
    }
}
