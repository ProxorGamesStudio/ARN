using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ProceduralAnimation : MonoBehaviour {

    public enum Mode
    {
        List
    }
    public Mode mode;

    [Serializable]
    public struct ListMode
    {
        public RectTransform[] elements;
        [Range(1, 4)]
        public float ListSizeFactor;
    }

    [Range(0,10)]
    public float adaptiveFactor = 5;
    

    public ListMode listMode;

    #region local factors
    int listNum = -1;
    Vector2 targetSize, startSize, DeSelectSize;
    #endregion

    private void Awake()
    {
        if (mode == Mode.List && listMode.elements.Length > 0)
        {
            startSize = listMode.elements[0].localScale;
            DeSelectSize = startSize / listMode.ListSizeFactor;
            targetSize = startSize * listMode.ListSizeFactor;
        }
    }

    private void Update()
    {
        if(mode == Mode.List)
            UpdateSizeList();
    }

    public void SetListSelect(int num)
    {
         listNum = num;
    }

    public void UpdateSizeList()
    {
        for (int i = 0; i < listMode.elements.Length; i++)
            if (i != listNum || listNum < 0)
                listMode.elements[i].localScale = Vector2.Lerp(listMode.elements[i].localScale, listNum < 0? startSize : DeSelectSize, adaptiveFactor * Time.deltaTime);
        if(listNum >= 0)
        listMode.elements[listNum].localScale = Vector2.Lerp(listMode.elements[listNum].localScale, targetSize, adaptiveFactor * Time.deltaTime);
    }
}
