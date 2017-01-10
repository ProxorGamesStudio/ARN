using UnityEngine;
using UnityEngine.UI;

public class ScrUIVKPlaceForPiece : MonoBehaviour {

    public Text Name;
    public int Num;

    public ScrUIVK konstr;

	public void Btn()
    {
        konstr.RemovePiece(Num);
    }
}
