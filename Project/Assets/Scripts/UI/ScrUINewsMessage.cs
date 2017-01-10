using UnityEngine;
using UnityEngine.UI;

public class ScrUINewsMessage : MonoBehaviour {

    
    public Text Message;
    public Sprite hide, show;

   public void HidePanel()
   {
        transform.GetComponent<Image>().sprite = hide;
   }
    public void ShowPanel()
    {
        transform.GetComponent<Image>().sprite = show;
    }

}
