using UnityEngine;
using UnityEngine.UI;

public class ScrUIMessanger : MonoBehaviour {
    public static ScrUIMessanger inst;

    public Text message;
    bool start = false;

    public void ShowMessage(string m)
    {
        message.text = m;
        gameObject.SetActive(true);
    }

    void Awake()
    {
        inst = this;
    }

    void Update()
    {
        if (!start)
        {
            gameObject.SetActive(false);
            start = true;
        }
    }
	
}
