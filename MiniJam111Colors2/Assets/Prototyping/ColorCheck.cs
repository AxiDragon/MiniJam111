using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCheck : MonoBehaviour
{
    [SerializeField] Renderer meshRenderer;
    [SerializeField] RenderTexture textureSettings;
    [SerializeField] Camera texCam;
    Material material;
    RenderTexture rt;

    void Start()
    {
        material = meshRenderer.material;

        rt = new RenderTexture(textureSettings);
        rt.Create();
        
        if (texCam.targetTexture != null)
        {
            texCam.targetTexture.Release();
        }

        texCam.targetTexture = rt;
    }

    void Update()
    {
        UpdateColor(textureSettings);
    }

    private void UpdateColor(RenderTexture rt)
    {
        Texture2D convertedTexture = ConvertToTexture2D(rt);
        Color newColor = convertedTexture.GetPixel(0,0);
        material.color = newColor;
    }

    private Texture2D ConvertToTexture2D(RenderTexture rt)
    {
        Texture2D texture = new Texture2D(rt.width, rt.height);
        RenderTexture.active = rt;
        texture.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        texture.Apply();
        RenderTexture.active = null;
        return texture;
    }
}
