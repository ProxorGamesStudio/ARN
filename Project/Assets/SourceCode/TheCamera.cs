using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheCamera : MonoBehaviour {

    public Transform axisX, axisY;
    public float speed, smooth, MaxRot = 90;
    public bool invert;
    float rotX, rotY, asymptote;
	// Use this for initialization
	void Start () {
        transform.parent = axisX;
        axisX.parent = axisY;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(1))
        {
            if (invert)
                rotX = Mathf.Lerp(rotX, speed * Time.deltaTime * Input.GetAxis("Mouse Y"), smooth * Time.deltaTime);
            else
                rotX = Mathf.Lerp(rotX, -speed * Time.deltaTime * Input.GetAxis("Mouse Y"), smooth * Time.deltaTime);

            rotY = Mathf.Lerp(rotY, speed * Time.deltaTime * Input.GetAxis("Mouse X"), smooth * Time.deltaTime);
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
    }
}
