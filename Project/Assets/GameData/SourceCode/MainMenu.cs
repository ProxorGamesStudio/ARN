using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public RectTransform Selector;
    RectTransform CurrentPageRect;
    public RectTransform[] ButtonsSelect;
    public RectTransform[] MenuPages;

    public float SelectorSpeed, ChangePageSpeed;
    float SelectorSize, SelectorPos;
    bool changePage;
    int CurrentPage, directionChangePage;

    public RawImage logo;
    public float speedLogoShow, speedCreditsShow;
    Color logoAlpha, creditsAlpha;
    void Start()
    {
        logoAlpha = Color.white;
        SetPage(0);
        CurrentPageRect = MenuPages[0];
        creditsAlpha = new Color(255, 255, 255, 1);
        foreach (Text txt in MenuPages[4].GetComponentsInChildren<Text>())
        {
            txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, 0);
        }
        foreach (Image img in MenuPages[4].GetComponentsInChildren<Image>())
            img.color = new Color(img.color.r, img.color.g, img.color.b, 0);
    }

    private void Update()
    {
        UpdateSelector();
        UpdateAlpha();
        if (changePage)
            ChangePage(directionChangePage);
    }

    void UpdateSelector()
    {
        Selector.position = new Vector2(Mathf.Lerp(Selector.position.x, SelectorPos, SelectorSpeed * Time.deltaTime), Selector.position.y);
        Selector.sizeDelta = new Vector2(Mathf.Lerp(Selector.sizeDelta.x, SelectorSize, SelectorSpeed * Time.deltaTime), Selector.sizeDelta.y);
    }

    void UpdateAlpha()
    {
        logo.color = Color.Lerp(logo.color, logoAlpha, speedLogoShow);
        foreach (Text txt in MenuPages[4].GetComponentsInChildren<Text>())
            txt.color = Color.Lerp(txt.color, creditsAlpha, speedCreditsShow);
        foreach (Image img in MenuPages[4].GetComponentsInChildren<Image>())
            img.color = Color.Lerp(img.color, creditsAlpha, speedCreditsShow);
    }

    public void SetPage(int page)
    {
        if (!(changePage && page == CurrentPage))
        {
            CurrentPageRect = MenuPages[CurrentPage];
            if (page != CurrentPage)
            {
                int dir = ButtonsSelect[CurrentPage].position.x < ButtonsSelect[page].position.x ? 1 : -1;
                directionChangePage = dir;
                CurrentPage = page;
                SetChangePage(dir);
                changePage = true;
            }

            float size = 0;
            switch (page)
            {
                case 0:
                    ShowHome();
                    size = ButtonsSelect[page].sizeDelta.x;
                    break;
                case 4:
                    size = 240;
                    ShowCredits();
                    break;
                case 5:
                    size = ButtonsSelect[page].sizeDelta.x;
                    ShowEmpty();
                    break;
                default:
                    ShowEmpty();
                    size = 240;
                    break;
            }
            MoveSelector(ButtonsSelect[page].position.x, size);
        }
    }

    public void MoveSelector(float pos, float size) //move down line of select in menu
    {
        SelectorPos = pos;
        SelectorSize = size;
    }

    public void SetChangePage(int dir)
    {
        MenuPages[CurrentPage].gameObject.SetActive(true);
        CurrentPageRect.localPosition = new Vector2(0, CurrentPageRect.localPosition.y);
        MenuPages[CurrentPage].localPosition = new Vector2(1250 * dir, MenuPages[CurrentPage].localPosition.y);
        CurrentPageRect.localScale = new Vector2(1, 1);
        MenuPages[CurrentPage].localScale = Vector2.zero;
    }

    void ChangePage(int dir)
    {
        const float near = 2f;
        if (dir > 0 && CurrentPageRect.localPosition.x > -1250 + near || dir < 0 && CurrentPageRect.localPosition.x < 1250 - near)
        {
            CurrentPageRect.localPosition = Vector2.Lerp(CurrentPageRect.localPosition, new Vector2(-1250 * dir, CurrentPageRect.localPosition.y), ChangePageSpeed * Time.deltaTime);
            MenuPages[CurrentPage].localPosition = Vector2.Lerp(MenuPages[CurrentPage].localPosition, new Vector2(0, MenuPages[CurrentPage].localPosition.y), ChangePageSpeed * Time.deltaTime);
            CurrentPageRect.localScale = Vector2.Lerp(CurrentPageRect.localScale, Vector2.zero, ChangePageSpeed * Time.deltaTime);
            MenuPages[CurrentPage].localScale = Vector2.Lerp(MenuPages[CurrentPage].localScale, new Vector2(1, 1), ChangePageSpeed * Time.deltaTime);
        }
        else
        {
            CurrentPageRect.gameObject.SetActive(false);
            CurrentPageRect = MenuPages[CurrentPage];
            changePage = false;
        }
    }

    void ShowHome() //show home page
    {
        if (ButtonsSelect[0].GetComponent<Toggle>().isOn)
        {
            logo.color = new Color(logo.color.r, logo.color.g, logo.color.b, 0);
            logoAlpha = new Color(logo.color.r, logo.color.g, logo.color.b, 1);
        }
    }

    void ShowCredits()
    {
        if (ButtonsSelect[0].GetComponent<Toggle>().isOn)
        {
            logo.color = new Color(logo.color.r, logo.color.g, logo.color.b, 0);
            logoAlpha = new Color(logo.color.r, logo.color.g, logo.color.b, 1);
        }
    }

    void ShowEmpty() //show empty page
    {
        if (ButtonsSelect[4].GetComponent<Toggle>().isOn)
        {
            foreach (Text txt in MenuPages[4].GetComponentsInChildren<Text>())
            {
                txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, 0);
                creditsAlpha = new Color(txt.color.r, txt.color.g, txt.color.b, 1);
            }
            foreach (Image img in MenuPages[4].GetComponentsInChildren<Image>())
                img.color = new Color(img.color.r, img.color.g, img.color.b, 0);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

}
