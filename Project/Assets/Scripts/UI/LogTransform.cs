using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogTransform : MonoBehaviour {
	public ScrollRect myScrollRect;
	public RectTransform content;
	public Vector2 offsetMax;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		content.offsetMax = new Vector2 (1000f, 0f);

	}
	public void Max()
	{
		content.offsetMax = new Vector2 (1000f, 0f);
	}



}
