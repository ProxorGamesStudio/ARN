using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcedureOptionsAnimation : MonoBehaviour {

    const float discrepancy = 0.1f;

    public Text mainText;
    public RectTransform iconSettings, Buttons, AcceptButton, CancelButton;

    [Range(0, 10)]
    public float speed;

    #region start params
    Color mainTextStartColor;
    Vector2 iconStartPosition, iconStartSize, buttonsStartSize;
    #endregion

    #region params
    bool mode = false;          //0 - BigPic, 1 - StandartPic
    Vector2 iconTargetPosition, iconTargetSize, buttonsTargetSize;
    #endregion

    void Start () {
        mainTextStartColor = mainText.color;
        iconTargetPosition = mainText.rectTransform.position;
        iconStartPosition = iconSettings.position;
        iconStartSize = iconSettings.sizeDelta;
        iconTargetSize = iconSettings.sizeDelta * 0.43f;
        buttonsStartSize = Buttons.localScale;
        buttonsTargetSize = new Vector3(0.12f, 0.12f, 0);
    }
	
	// Update is called once per frame
	void Update () {
        if (!mode)  BigPictureMode();
        else        StandartPictureMode();
        Buttons.position = iconSettings.position;
	}

    public void ResetMode()
    {
        mode = !mode;
    }

    public void BigPictureMode()
    {
        mainText.color = Color.Lerp(mainText.color, mainTextStartColor, speed * Time.deltaTime);
        iconSettings.position = Vector2.Lerp(iconSettings.position, iconStartPosition, speed * Time.deltaTime);
        iconSettings.sizeDelta = Vector2.Lerp(iconSettings.sizeDelta, iconStartSize, speed * Time.deltaTime);
        RotateSettingIcon((-iconSettings.sizeDelta.x + iconStartSize.x)/1.3f);
        Buttons.localScale = Vector3.Lerp(Buttons.localScale, buttonsStartSize, speed * Time.deltaTime);
        foreach (Image img in Buttons.GetComponentsInChildren<Image>())
            if (img.transform.parent != Buttons.transform)
                img.color = Color.Lerp(img.color, DefalutColor(img.color), speed * Time.deltaTime);
        iconSettings.GetComponent<Button>().interactable = false;
    }

    public void StandartPictureMode()
    {
        mainText.color = Color.Lerp(mainText.color, ColorNonAlpha(mainText.color), speed * Time.deltaTime);
        iconSettings.position = Vector2.Lerp(iconSettings.position, iconTargetPosition, speed * Time.deltaTime);
        iconSettings.sizeDelta = Vector2.Lerp(iconSettings.sizeDelta, iconTargetSize, speed * Time.deltaTime);
        RotateSettingIcon((iconTargetSize.x - iconSettings.sizeDelta.x)/1.3f);
        Buttons.localScale = Vector3.Lerp(Buttons.localScale,buttonsTargetSize, speed * Time.deltaTime);
        foreach (Image img in Buttons.GetComponentsInChildren<Image>())
           if(img.transform.parent != Buttons.transform)
               img.color = Color.Lerp(img.color, ColorNonAlpha(img.color), speed * Time.deltaTime);
        iconSettings.GetComponent<Button>().interactable = true;
    }

    Color ColorNonAlpha(Color inputColor)
    {
        return new Color(inputColor.r, inputColor.g, inputColor.b, 0);
    }

    Color DefalutColor(Color inputColor)
    {
        return new Color(inputColor.r, inputColor.g, inputColor.b, 1);
    }

    public void RotateSettingIcon(float speed)
    {
          iconSettings.Rotate(0, 0, speed);
    }
}
