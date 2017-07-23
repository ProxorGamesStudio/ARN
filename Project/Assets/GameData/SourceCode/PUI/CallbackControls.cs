using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ProxorGamesLocalisation;

public class CallbackControls : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Options options;
    public KeyCode newKey;
    public int n;
    public bool status;
    public Sprite HighLighted, Normal;
    Image img { get { return GetComponent<Image>(); }  set { value = GetComponent<Image>(); } }
    public Text KeyText;
    public Localisation localisation;
    ProcedureOptionsAnimation poa;

    private void Start()
    {
        poa = FindObjectOfType<ProcedureOptionsAnimation>();
    }

    public void OnPointerClick(PointerEventData data)
    {
        status = true;
    }

    public void OnPointerEnter(PointerEventData data)
    {
        img.sprite = HighLighted;
    }

    public void OnPointerExit(PointerEventData data)
    {
        img.sprite = Normal;
    }

    void SendCall()
    {
        KeyText.color = new Color(KeyText.color.r, KeyText.color.g, KeyText.color.b, 1);
        Controller.Button btn = options.settings.controls.controls.Buttons[n];
        btn.key = newKey;
        options.settings.controls.controls.Buttons[n] = btn;
    }

    private void Update()
    {
        options.controlSelect = status;
        if (status)
        {
            poa.enabled = false;
            KeyText.text = localisation.GetLocalizationString("#lbl_SetControlButton");
            KeyText.color = new Color(KeyText.color.r, KeyText.color.g, KeyText.color.b, Mathf.Abs(Mathf.Cos(Time.time * 4)));
            Event e = Event.current;
            if (Input.anyKeyDown)
            {
                foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
                    if (Input.GetKey(vKey) && !options.settings.controls.controls.Buttons.Exists(c => c.key == vKey))
                        newKey = vKey;
                System.Enum.GetNames(typeof(KeyCode));
                SendCall();
                Invoke("Enable", 0.4f);
                status = false;
            }
        }
    }

    public void Enable()
    {
        poa.enabled = true;
    }
}
