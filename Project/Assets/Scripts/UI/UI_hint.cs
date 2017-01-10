using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_hint : MonoBehaviour {
    int n;
    public Transform UI_panel;
    public Transform[] points;

	void Update () {
        UI_panel.position = points[n].position;
	}

	public void SetNumber(int number)
    {
        n = number;
    }
}
