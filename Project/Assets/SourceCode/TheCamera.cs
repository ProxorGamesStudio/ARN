using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheCamera : MonoBehaviour {

    public Transform axisX, axisY;
    public float speed, smooth;
    public bool invert;
    float rotX, rotY;
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
        axisX.Rotate(rotX, 0, 0);
        axisY.Rotate(0, rotY , 0);
    }
}
