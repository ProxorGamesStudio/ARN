using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthGraphical : MonoBehaviour {
    public Material material;
    RaycastHit hit;
    public Transform Empty,Ring, RingModel;
    public Color color;
    public float width, speed, smooth;
    float y;
	// Use this for initialization
	void Start () {
        material.SetColor("_ColorPoloska", color);
        material.SetFloat("_Width", width);
        RingModel.localScale = new Vector3(RingModel.localScale.x, RingModel.localScale.y, width*4.2f);
        RingModel.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", color);
	}
	
	// Update is called once per frame
	void Update () {
         if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
         {
            y = Mathf.Lerp(y, hit.point.y, speed * Time.deltaTime * smooth);
             material.SetVector("_MousePosition",  new Vector4(0, y, 0, 1));
             Empty.LookAt(hit.point);
             Ring.rotation = Quaternion.Lerp(Ring.rotation, Quaternion.Euler(90, Empty.rotation.eulerAngles.y, Empty.rotation.eulerAngles.z), speed * Time.deltaTime * smooth);
             Ring.gameObject.SetActive(true);
        }
         else
        {
            material.SetVector("_MousePosition", new Vector4(0, hit.point.y, 0, 0));
         //   Ring.gameObject.SetActive(false);
        }
    }
}
