using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProxorNetwork;
using UnityEngine.EventSystems;

public class ProcedureOptionsAnimation : MonoBehaviour {

    const float discrepancy = 0.1f;

    public Text mainText;
    public RectTransform iconSettings, Buttons, AcceptButton, CancelButton, Line, Target;
    public GameObject[] arrows;

    [Range(0, 10)]
    public float speed;
    [Range(0, 2)]
    public float iconFactor = 0.43f;

    #region start params
    Color mainTextStartColor;
    Vector2 iconStartPosition, iconStartSize, buttonsStartSize, AcceptButtonStartPos, CancelButtonStartPos, LineStartPos;
    #endregion

    #region params
    bool mode = false;          //0 - BigPic, 1 - StandartPic
    Vector2 iconTargetPosition, iconTargetSize, buttonsTargetSize, AcceptButtonTargetPos, CancelButtonTargetPos, LineTargetPos;
    Transform[] childs = new Transform[4];
    int clicked;
    bool change;
    #endregion

    

    void Start () {
      
        mainTextStartColor = mainText.color;
        iconTargetPosition = iconSettings.InverseTransformPoint(mainText.rectTransform.position);
        iconStartPosition = iconSettings.localPosition;
        iconStartSize = iconSettings.sizeDelta;
        iconTargetSize = iconSettings.sizeDelta * iconFactor;
        buttonsStartSize = Buttons.localScale;
        buttonsTargetSize = new Vector3(0.12f, 0.12f, 0);
        AcceptButtonStartPos = AcceptButton.localPosition + new Vector3(280, 0, 0);
        AcceptButtonTargetPos = AcceptButton.localPosition;
        CancelButtonStartPos = CancelButton.localPosition - new Vector3(280, 0, 0);
        CancelButtonTargetPos = CancelButton.localPosition;
        LineStartPos = Line.localPosition;
        LineTargetPos = new Vector3(0,-155);
        for (int i = 0; i < Line.GetChild(0).childCount; i++)
        {
            childs[i] = Line.GetChild(0).GetChild(i);
            foreach (Text txt in childs[i].GetComponentsInChildren<Text>())
                txt.color = ProxorEngine.Colors.ColorNonAlpha(txt.color);
        }
    }

    // Update is called once per frame
    void Update () {
        if (!mode)  BigPictureMode();
        else        StandartPictureMode();
        Buttons.localPosition = iconSettings.localPosition;
        clicked = GetComponentInChildren<ProceduralAnimation>().nowSelect;
	}

    public void ResetMode()
    {
        mode = !mode;
    }

    public void BigPictureMode()
    {
        #region once
        if (change)
        {
            iconSettings.GetComponent<Button>().interactable = false;
            AcceptButton.GetComponent<Button>().enabled = false;
            CancelButton.GetComponent<Button>().enabled = true;
            foreach (Button trigger in Buttons.GetComponentsInChildren<Button>())
            {
                trigger.enabled = true;
                trigger.GetComponent<EventTrigger>().enabled = true;
            }
            Buttons.GetComponent<ProceduralAnimation>().enabled = true;
            change = false;
        }
        #endregion
        #region update
        mainText.color = Color.Lerp(mainText.color, mainTextStartColor, speed * Time.deltaTime);
        iconSettings.localPosition = Vector2.Lerp(iconSettings.localPosition, iconStartPosition, speed * Time.deltaTime);
        iconSettings.sizeDelta = Vector2.Lerp(iconSettings.sizeDelta, iconStartSize, speed * Time.deltaTime);
        RotateSettingIcon((-iconSettings.sizeDelta.x + iconStartSize.x)/1.3f);
        Buttons.localScale = Vector3.Lerp(Buttons.localScale, buttonsStartSize, speed * Time.deltaTime);
        foreach (Image img in Buttons.GetComponentsInChildren<Image>())
            if (img.transform.parent != Buttons.transform)
                img.color = Color.Lerp(img.color, ProxorEngine.Colors.DefalutColor(img.color), speed * Time.deltaTime);

        AcceptButton.localPosition = Vector3.Lerp(AcceptButton.localPosition, AcceptButtonStartPos, speed * 1.5f * Time.deltaTime);
        AcceptButton.GetComponent<Image>().color = Color.Lerp(AcceptButton.GetComponent<Image>().color, ProxorEngine.Colors.ColorNonAlpha(AcceptButton.GetComponent<Image>().color), speed * Time.deltaTime);
        AcceptButton.GetComponentInChildren<Text>().color = Color.Lerp(AcceptButton.GetComponentInChildren<Text>().color, ProxorEngine.Colors.ColorNonAlpha(AcceptButton.GetComponentInChildren<Text>().color), speed  * Time.deltaTime);

        CancelButton.localPosition = Vector3.Lerp(CancelButton.localPosition, CancelButtonStartPos, speed * 1.5f * Time.deltaTime);  
        CancelButton.GetComponent<Image>().color = Color.Lerp(CancelButton.GetComponent<Image>().color, ProxorEngine.Colors.ColorNonAlpha(CancelButton.GetComponent<Image>().color), speed * Time.deltaTime);
        CancelButton.GetComponentInChildren<Text>().color = Color.Lerp(CancelButton.GetComponentInChildren<Text>().color, ProxorEngine.Colors.ColorNonAlpha(CancelButton.GetComponentInChildren<Text>().color), speed * Time.deltaTime);
   
        for (int i = 0; i < Line.GetChild(0).childCount; i++)
        {
            Line.localPosition = Vector3.Lerp(Line.localPosition, LineStartPos, (speed * 1.5f * Time.deltaTime) / Line.GetChild(0).childCount);
            foreach (Text txt in childs[i].GetComponentsInChildren<Text>())
                txt.color = Color.Lerp(txt.color, ProxorEngine.Colors.ColorNonAlpha(txt.color), speed / 2.25f * Time.deltaTime);
            foreach(Image img in childs[i].GetComponentsInChildren<Image>())
                img.color = Color.Lerp(img.color, ProxorEngine.Colors.ColorNonAlpha(img.color), speed / 2.25f * Time.deltaTime);
            childs[i].position = Target.position;
        }
        childs[clicked].position = Target.position;
        #endregion
    }

    public void StandartPictureMode()
    {
        #region once
        if (!change)
        {
            iconSettings.GetComponent<Button>().interactable = true;
            AcceptButton.GetComponent<Button>().enabled = true;
            CancelButton.GetComponent<Button>().enabled = true;
            foreach (Button trigger in Buttons.GetComponentsInChildren<Button>())
            {
                trigger.enabled = false;
                trigger.GetComponent<EventTrigger>().enabled = false;
            }
            Buttons.GetComponent<ProceduralAnimation>().enabled = false;
            foreach (GameObject go in arrows)
                go.SetActive(false);
            change = true;
        }
        #endregion

        #region update
        mainText.color = Color.Lerp(mainText.color, ProxorEngine.Colors.ColorNonAlpha(mainText.color), speed * Time.deltaTime);
        iconSettings.localPosition = Vector2.Lerp(iconSettings.localPosition, iconTargetPosition, speed * Time.deltaTime);
        iconSettings.sizeDelta = Vector2.Lerp(iconSettings.sizeDelta, iconTargetSize, speed * Time.deltaTime);
        RotateSettingIcon((iconTargetSize.x - iconSettings.sizeDelta.x)/1.3f);
        Buttons.localScale = Vector3.Lerp(Buttons.localScale,buttonsTargetSize, speed * 1.4f * Time.deltaTime);
        foreach (Image img in Buttons.GetComponentsInChildren<Image>())
           if(img.transform.parent != Buttons.transform)
               img.color = Color.Lerp(img.color, ProxorEngine.Colors.ColorNonAlpha(img.color), speed * Time.deltaTime);
        
        AcceptButton.localPosition = Vector3.Lerp(AcceptButton.localPosition, AcceptButtonTargetPos, speed * Time.deltaTime);
       
        AcceptButton.GetComponent<Image>().color = Color.Lerp(AcceptButton.GetComponent<Image>().color, ProxorEngine.Colors.DefalutColor(AcceptButton.GetComponent<Image>().color), speed / 2 * Time.deltaTime);
        AcceptButton.GetComponentInChildren<Text>().color = Color.Lerp(AcceptButton.GetComponentInChildren<Text>().color, ProxorEngine.Colors.DefalutColor(AcceptButton.GetComponentInChildren<Text>().color), speed / 2 * Time.deltaTime);

        CancelButton.localPosition = Vector3.Lerp(CancelButton.localPosition, CancelButtonTargetPos, speed * Time.deltaTime);
      
        CancelButton.GetComponent<Image>().color = Color.Lerp(CancelButton.GetComponent<Image>().color, ProxorEngine.Colors.DefalutColor(CancelButton.GetComponent<Image>().color), speed / 2  * Time.deltaTime);
        CancelButton.GetComponentInChildren<Text>().color = Color.Lerp(CancelButton.GetComponentInChildren<Text>().color, ProxorEngine.Colors.DefalutColor(CancelButton.GetComponentInChildren<Text>().color), speed / 2 * Time.deltaTime);

        Line.localPosition = Vector3.Lerp(Line.localPosition, LineTargetPos, speed * 1.4f * Time.deltaTime);
        childs[clicked].position = Target.position;

        foreach (Text txt in childs[clicked].GetComponentsInChildren<Text>())
            txt.color = Color.Lerp(txt.color, ProxorEngine.Colors.DefalutColor(txt.color), speed/1.5f * Time.deltaTime);
        foreach (Image img in childs[clicked].GetComponentsInChildren<Image>())
            img.color = Color.Lerp(img.color, ProxorEngine.Colors.DefalutColor(img.color), speed / 1.5f * Time.deltaTime);

        #endregion
    }



    public void RotateSettingIcon(float speed)
    {
          iconSettings.Rotate(0, 0, speed);
    }
}
