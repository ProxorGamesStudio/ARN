using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ScrViewsManager : MonoBehaviour
{

    public ScrHubVis[] Views;
    public int StartView = 0;
    public Material[] materials;
    public bool Showed = false;
    public int IdSetMaterial = -1;
	//public GameObject[] Effects;

    private ScrHubVis InstView;
	//private GameObject[] InstEffects;
    private int IdInstView;
    private int LstIdSetMaterial;

    /*public List<Renderer> Renderers=new List<Renderer>(4);
    public Color _color;
    */
    void Start()
	{
        SetView(StartView);
        LstIdSetMaterial = -1;
		//InstEffects = new GameObject[Effects.Length];
	}

    void Update()
    {
        InstView.gameObject.SetActive(Showed);
        if(LstIdSetMaterial != IdSetMaterial)
        {
            if(SetMaterial(IdSetMaterial))
            {
                LstIdSetMaterial = IdSetMaterial;
            }
        }
    }

    private bool SetMaterial(int id)
    {
        if(id<0 || id>=materials.Length)
        {
            return false;
        }
        if(!InstView)
        {
            return false;
        }
        foreach (var r in InstView.renderers)
            r.material = materials[id];
        return true;
    }

    public bool SetView(int id)
    {
        //Debug.Log(ID);
        if (Views.Length <= id)
            return false;

        if (InstView)
        {
            DestroyImmediate(InstView.gameObject);
        }
        InstView = Instantiate(Views[id]);
        InstView.transform.SetParent(transform);
        InstView.transform.localPosition = Vector3.zero;
        InstView.transform.LookAt(Vector3.zero);
        IdInstView = id;
        return true;
    }

	/*public void SetEffect(int ID, float time)
	{
		if (InstView) {
			if (InstEffects [ID])
				Destroy (InstEffects [ID]);
			InstEffects [ID] = (GameObject)Instantiate (Effects [ID]);
			InstEffects [ID].transform.SetParent (transform);
			InstEffects [ID].transform.localPosition = Vector3.zero;
			if (time > 0) {
				Destroy (InstEffects [ID], time);
			}
		}
	}*/

    public int GetIDInst()
    {
        if (InstView)
            return IdInstView;
        return -1;
    }

	/*public void DeletEffect(int ID)
	{
		if (InstEffects[ID])
			Destroy(InstEffects[ID]);
	}*/

    public void DeletView()
    {
        if (InstView)
            Destroy(InstView);
        IdInstView = -1;
    }

    /*public void SetColor(Color color)
    {
        _color = color;
        if (Renderers != null)
        {
            foreach (var renderer1 in Renderers)
            {
                renderer1.material.color = color;
            }
        }
    }*/

    /*public void Show(bool state)
    {
        if (InstView)
        {
            InstView.SetActive(state);
        }
    }*/
}
