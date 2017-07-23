using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheCamera : MonoBehaviour {

    Transform axisX, axisY;
    public float speed, smooth, MaxRot = 90, zoomSpeed = 1, zoomSensivity = 10, sensivity;
    public bool invert;
    float rotX, rotY, asymptote,  _x, _z, _ux, _uScale, _uA = 1;
    public RectTransform EarthUI;
    Image[] EarthUI_sprites;

    // Use this for initialization
    void Start () {
        axisX = new GameObject("AxisX").transform;
        axisY = new GameObject("AxisY").transform;
        axisX.rotation = axisY.rotation = transform.rotation;
        transform.parent = axisX;
        axisX.parent = axisY;
        _x = transform.localPosition.x;
        _z = transform.localPosition.z;
        _ux = EarthUI.transform.position.x;
        _uScale = EarthUI.transform.localScale.x;
        EarthUI_sprites = EarthUI.GetComponentsInChildren<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(1))
        {
            if (invert)
                rotX = Mathf.Lerp(rotX, speed * sensivity * Time.deltaTime * Input.GetAxis("Mouse Y"), smooth * Time.deltaTime);
            else
                rotX = Mathf.Lerp(rotX, -speed * sensivity * Time.deltaTime * Input.GetAxis("Mouse Y"), smooth * Time.deltaTime);

            rotY = Mathf.Lerp(rotY, speed * sensivity * Time.deltaTime * Input.GetAxis("Mouse X"), smooth * Time.deltaTime);
        }
        else
        {
            rotX = Mathf.Lerp(rotX, 0, smooth * Time.deltaTime);
            rotY = Mathf.Lerp(rotY, 0, smooth * Time.deltaTime);
        }
        asymptote += rotX;
        if (asymptote > MaxRot)
            asymptote = 89.9999f;
        if(asymptote < -MaxRot)
            asymptote = -89.9999f;
        axisX.rotation = Quaternion.Euler(asymptote, axisX.rotation.eulerAngles.y, axisX.rotation.eulerAngles.z);
        axisY.Rotate(0, rotY , 0);
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (_x < 0)
                {
                    _x += zoomSensivity;
                    _ux -= zoomSensivity * 0.73f;
                    _uScale += zoomSensivity * 0.007f;
                    _z += zoomSensivity * 2;
                    _uA -= zoomSensivity * 0.014f;
                }          
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (_x > -108)
                {
                    _x -= zoomSensivity;
                    _ux += zoomSensivity * 0.73f;
                    _uScale -= zoomSensivity * 0.007f;
                    _z -= zoomSensivity * 2;
                    _uA += zoomSensivity * 0.014f;
                }
            }
        }
        foreach (Image s in EarthUI_sprites)
            s.color = Color.Lerp(s.color, new Color(s.color.r, s.color.g, s.color.b, _uA), zoomSpeed * Time.deltaTime);
        EarthUI.transform.position = Vector3.Lerp(EarthUI.transform.position, new Vector3(_ux, EarthUI.transform.position.y, EarthUI.transform.position.z), zoomSpeed * Time.deltaTime);
        EarthUI.transform.localScale = Vector3.Lerp(EarthUI.transform.localScale, new Vector3(_uScale, _uScale, 1), zoomSpeed * Time.deltaTime);
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(_x, transform.localPosition.y, _z), zoomSpeed * Time.deltaTime);
    }
}
