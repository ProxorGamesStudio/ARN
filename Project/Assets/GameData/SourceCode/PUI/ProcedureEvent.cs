using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ProcedureEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    [HideInInspector]
    public int num;
    [HideInInspector]
    public ProceduralAnimation PA;

    public void SetEventParams(int n, ProceduralAnimation pa)
    {
        num = n;
        PA = pa;
    }

    public void OnPointerEnter(PointerEventData data)
    {
        PA.SetListSelect(num);
    }

    public void OnPointerExit(PointerEventData data)
    {
        PA.SetListSelect(-1);
    }

    public void OnPointerClick(PointerEventData data)
    {
        PA.SetClick(num);
    }
}
