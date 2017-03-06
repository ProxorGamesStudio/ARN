using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hub : MonoBehaviour {
    RaycastHit hit;
    Hub_Manager hub_manager;
    public string Name;
    public int Level = 1;
    public enum type { Deafult, Scientific, Millitary, Commercial }
    public type Type;
    //[HideInInspector]
    public GameObject[] lines;
   // [HideInInspector]
    public GameObject[] links;

    void Start()
    {
        hub_manager = FindObjectOfType<Hub_Manager>();
    }

    void Update()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit) && hit.transform == transform && Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1))
        {
            hub_manager.open = true;
            hub_manager.TargetHub = this;
        }
    }
}
