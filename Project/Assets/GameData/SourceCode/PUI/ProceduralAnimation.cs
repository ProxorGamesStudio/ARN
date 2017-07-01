using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ProceduralAnimation : MonoBehaviour
{
#if UNITY_STANDALONE

    public enum Mode
    {
        List, Once
    }
    public Mode mode;
    
    [Serializable]
    public struct ListMode
    {
        public RectTransform[] elements;
        [Range(0.01f, 4)]
        public float ListSizeFactorSelected, ListSizeFactorDeselected;
    }

    [Serializable]
    public struct OnceMode
    {
        [Range(0.01f, 4)]
        public float SizeFactor;
    }

    [Range(0, 10)]
    public float adaptiveFactor = 5;

    [SerializeField]
    public ListMode listMode;
    public OnceMode onceMode;
    

    #region local factors
    int listNum = -1;
    Vector2 targetSize, startSize, DeSelectSize;
    #endregion

    #region callbacks varablies
    [HideInInspector]
    public int nowSelect;
    #endregion

    private void Awake()
    {
        if (mode == Mode.List && listMode.elements.Length > 0)
        {
            startSize = listMode.elements[0].localScale;
            DeSelectSize = startSize / listMode.ListSizeFactorDeselected;
            targetSize = startSize * listMode.ListSizeFactorSelected;
            SetTriggers(listMode.elements);
        }
        if (mode == Mode.Once)
        {
            startSize = transform.localScale;
            targetSize = startSize * onceMode.SizeFactor;
            SetTriggers(new RectTransform[] { this.GetComponent<RectTransform>() });
        }
    }

    private void Update()
    {
        if (mode == Mode.List && listMode.elements.Length > 0)
            UpdateSizeList();
        if (mode == Mode.Once)
            UpdateSizeOnce();
    }

    public void SetListSelect(int num)
    {
        listNum = num;
    }

    public void SetClick(int num)
    {
        nowSelect = num;
    }

    public int GetClick()
    {
        return nowSelect;
    }

    public void UpdateSizeList()
    {
        for (int i = 0; i < listMode.elements.Length; i++)
            if (i != listNum || listNum < 0)
                listMode.elements[i].localScale = Vector2.Lerp(listMode.elements[i].localScale, listNum < 0 ? startSize : DeSelectSize, adaptiveFactor * Time.deltaTime);      
        if (listNum >= 0)
            listMode.elements[listNum].localScale = Vector2.Lerp(listMode.elements[listNum].localScale, targetSize, adaptiveFactor * Time.deltaTime);
    }

    public void UpdateSizeOnce()
    {
        if (listNum == 0)
            transform.localScale = Vector2.Lerp(transform.localScale, targetSize, adaptiveFactor * Time.deltaTime);
        else
            transform.localScale = Vector2.Lerp(transform.localScale, startSize, adaptiveFactor * Time.deltaTime);
    }

    public void SetTriggers(RectTransform[] elems)
    {
        for(int i = 0; i < elems.Length; i++)
           elems[i].gameObject.AddComponent<ProcedureEvent>().SetEventParams(i, this);
    }

#endif
}
