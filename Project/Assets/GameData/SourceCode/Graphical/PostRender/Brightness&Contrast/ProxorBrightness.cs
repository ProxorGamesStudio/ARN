using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Proxor/Brightness Effect")]
public class ProxorBrightness : ImageEffectBase
{
    [Range(0f, 2f)] public float _Brightness = 1f;
    [Range(0f, 2f)] public float _Contrast = 1f;

    // Called by camera to apply image effect
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat("_Brightness", _Brightness);
        material.SetFloat("_Contrast", _Contrast);
        Graphics.Blit(source, destination, material);
    }
    public void SetBrightness(float value)
    {
        _Brightness = value;
    }
    public void SetContrast(float value)
    {
        _Contrast = value;
    }
}
