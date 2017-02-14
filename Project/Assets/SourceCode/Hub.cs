using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hub : MonoBehaviour {

    public bool open;
    public float speed;
    Vector2 OpenPoseSmall, OpenPoseBig, OpenScaleSmall, OpenScaleBig, pos;
    public RectTransform hubMenu;
    Image[] imgs;
    Text[] txts;
    RaycastHit hit;

    void Start()
    {
        OpenPoseSmall = hubMenu.position;
        OpenScaleSmall = hubMenu.localScale;
        imgs = hubMenu.transform.GetComponentsInChildren<Image>();
        txts = hubMenu.transform.GetComponentsInChildren<Text>();
    }

	// Update is called once per frame
	void Update () {
        pos = Camera.main.WorldToScreenPoint(transform.position);
		if(open)
        {
            hubMenu.position = Vector2.Lerp(hubMenu.position, OpenPoseSmall, Time.deltaTime * speed);
            hubMenu.localScale = Vector2.Lerp(hubMenu.localScale, OpenScaleSmall, Time.deltaTime * speed);
            foreach (Image img in imgs)
                img.color = Color.Lerp(img.color, new Color(img.color.r, img.color.g, img.color.b, 1), Time.deltaTime * speed);
            foreach(Text txt in txts)
                txt.color = Color.Lerp(txt.color, new Color(txt.color.r, txt.color.g, txt.color.b, 1), Time.deltaTime * speed);
        }
        else
        {
            hubMenu.position = Vector2.Lerp(hubMenu.position, pos, Time.deltaTime * speed*2.4f);
            hubMenu.localScale = Vector2.Lerp(hubMenu.localScale, Vector2.zero, Time.deltaTime * speed * 2);
            foreach (Image img in imgs)
                img.color = Color.Lerp(img.color, new Color(img.color.r, img.color.g, img.color.b, 0), Time.deltaTime * speed/1.5f);
            foreach (Text txt in txts)
                txt.color = Color.Lerp(txt.color, new Color(txt.color.r, txt.color.g, txt.color.b, 0), Time.deltaTime * speed / 1.5f);
        }
         if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit) && hit.transform == transform && Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1))
            open = true;
    }

    public void Close()
    {
        open = false;
    }

}
