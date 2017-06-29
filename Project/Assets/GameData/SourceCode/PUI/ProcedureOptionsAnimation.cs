using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProxorNetwork;

public class ProcedureOptionsAnimation : MonoBehaviour {

    const float discrepancy = 0.1f;

    public Text mainText;
    public RectTransform iconSettings, Buttons, AcceptButton, CancelButton;

    [Range(0, 10)]
    public float speed;
    [Range(0, 2)]
    public float iconFactor = 0.43f;
    #region start params
    Color mainTextStartColor;
    Vector2 iconStartPosition, iconStartSize, buttonsStartSize;
    #endregion

    #region params
    bool mode = false;          //0 - BigPic, 1 - StandartPic
    Vector2 iconTargetPosition, iconTargetSize, buttonsTargetSize;
    Vector2 startScreenSize;

    ProxorNetwork.Pair<float, float>[] factors = new ProxorNetwork.Pair<float, float>[4];
    #endregion



    void Start () {
      
        mainTextStartColor = mainText.color;

        iconTargetPosition = mainText.rectTransform.position;
        iconStartPosition = iconSettings.position;
        iconStartSize = iconSettings.sizeDelta;
        iconTargetSize = iconSettings.sizeDelta * iconFactor;
        buttonsStartSize = Buttons.localScale;
        buttonsTargetSize = new Vector3(0.12f, 0.12f, 0);

        ProxorEngine.Display.SetupFactors(
             new Vector2[] {  iconStartPosition, iconStartSize, buttonsStartSize },
             ref factors
      );
        FindObjectOfType<GameManager>().Scripts.Add(this);
        ChangeScreen();
    }

    void ChangeScreen()
    {
       // FindObjectOfType<CanvasScaler>().referenceResolution = new Vector2(Screen.width, Screen.height);
        iconStartPosition = iconStartPosition * FindObjectOfType<CanvasScaler>().scaleFactor;
    }

    void AcceptScreenChanges(Vector2[] temp)
    {
        iconStartPosition = temp[0];
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
                img.color = Color.Lerp(img.color, ProxorEngine.Colors.DefalutColor(img.color), speed * Time.deltaTime);
        iconSettings.GetComponent<Button>().interactable = false;
    }

    public void StandartPictureMode()
    {
        mainText.color = Color.Lerp(mainText.color, ProxorEngine.Colors.ColorNonAlpha(mainText.color), speed * Time.deltaTime);
        iconSettings.position = Vector2.Lerp(iconSettings.position, iconTargetPosition, speed * Time.deltaTime);
        iconSettings.sizeDelta = Vector2.Lerp(iconSettings.sizeDelta, iconTargetSize, speed * Time.deltaTime);
        RotateSettingIcon((iconTargetSize.x - iconSettings.sizeDelta.x)/1.3f);
        Buttons.localScale = Vector3.Lerp(Buttons.localScale,buttonsTargetSize, speed * Time.deltaTime);
        foreach (Image img in Buttons.GetComponentsInChildren<Image>())
           if(img.transform.parent != Buttons.transform)
               img.color = Color.Lerp(img.color, ProxorEngine.Colors.ColorNonAlpha(img.color), speed * Time.deltaTime);
        iconSettings.GetComponent<Button>().interactable = true;
    }

  

    public void RotateSettingIcon(float speed)
    {
          iconSettings.Rotate(0, 0, speed);
    }
}
