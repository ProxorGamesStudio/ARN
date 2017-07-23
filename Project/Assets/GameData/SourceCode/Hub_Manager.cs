using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hub_Manager : MonoBehaviour {

    [HideInInspector]
    public bool open, opened;
    public float speed;
    Vector2 OpenPoseSmall, OpenPoseBig, OpenScaleSmall, OpenScaleBig, pos;
    public RectTransform hubMenu;
    Image[] imgs;
    Text[] txts;
    [HideInInspector]
    public Hub TargetHub;
    GameObject halo, hint;
    RaycastHit hit;
    public bool on;
    public float opacity = 0, speedOpadcityPlus, speedOpadcityMinus;
    Camera mCam;
    public byte mode = 0;

    void Start()
    {
        OpenPoseSmall = hubMenu.position;
        OpenScaleSmall = hubMenu.localScale;
        imgs = hubMenu.transform.GetComponentsInChildren<Image>();
        txts = hubMenu.transform.GetComponentsInChildren<Text>();
        halo = GameObject.Find("SelectionHalo");
        hint = GameObject.Find("HubHint");
        mCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (mode == 1)
        {
            hint.GetComponent<MeshRenderer>().material.SetFloat("_Opacity", opacity);
            Hint();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit) && hit.transform.tag == "Hub")
            {
                halo.SetActive(true);
                halo.transform.parent = hit.transform.GetChild(0);
                SelectorControl();
                on = true;
                halo.transform.localPosition = new Vector3(0.69f, 0.32f, -1.62f);
                if (Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1))
                {
                    open = true;
                    TargetHub = hit.transform.GetComponent<Hub>();
                }
            }
            else
            {
                on = false;
                // hint.transform.GetChild(0).gameObject.SetActive(false);
                halo.SetActive(false);
            }

            if (TargetHub != null)
                pos = Camera.main.WorldToScreenPoint(TargetHub.transform.position);
            else
                pos = Camera.main.WorldToScreenPoint(transform.position);

            if (open)
            {
                if (!opened)
                {
                    opened = true;
                    hubMenu.position = pos;
                }
                hubMenu.position = Vector2.Lerp(hubMenu.position, OpenPoseSmall, Time.deltaTime * speed);
                hubMenu.localScale = Vector2.Lerp(hubMenu.localScale, OpenScaleSmall, Time.deltaTime * speed);
                foreach (Image img in imgs)
                    img.color = Color.Lerp(img.color, new Color(img.color.r, img.color.g, img.color.b, 1), Time.deltaTime * speed / 1.5f);
                foreach (Text txt in txts)
                    txt.color = Color.Lerp(txt.color, new Color(txt.color.r, txt.color.g, txt.color.b, 1), Time.deltaTime * speed / 1.5f);
            }
            else
            {
                opened = false;
                hubMenu.position = Vector2.Lerp(hubMenu.position, pos, Time.deltaTime * speed * 2.4f);
                hubMenu.localScale = Vector2.Lerp(hubMenu.localScale, Vector2.zero, Time.deltaTime * speed * 2);
                foreach (Image img in imgs)
                    img.color = Color.Lerp(img.color, new Color(img.color.r, img.color.g, img.color.b, 0), Time.deltaTime * speed / 1.5f);
                foreach (Text txt in txts)
                    txt.color = Color.Lerp(txt.color, new Color(txt.color.r, txt.color.g, txt.color.b, 0), Time.deltaTime * speed / 1.5f);
            }
        }
    }

    public void Close()
    {
        open = false;
    }

    void SelectorControl()
    {
     //   hint.transform.LookAt(mCam.transform);
      //  hint.transform.position = halo.transform.position;
    }

    void Hint()
    {
        if (on)
            opacity = Mathf.MoveTowards(opacity, 1, speedOpadcityPlus);
        else
            opacity = Mathf.MoveTowards(opacity, 0, speedOpadcityMinus);
    }
}
