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
    
    [SerializeField] float errorMargin = 0.1f;

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
        CheckColor();
    }

    private void CheckColor()
    {
        Color floorColor = GetColor();
        Vector3 difference = (Vector4)(material.color - floorColor);
        //Debug.Log(difference.sqrMagnitude);
        if (difference.sqrMagnitude < errorMargin)
        {
            //subtract health and other effects
            Debug.Log("IM DYING!!!");
        }
    }

    private Color GetColor()
    {
        Texture2D convertedTexture = ConvertToTexture2D();
        Color newColor = convertedTexture.GetPixel(0, 0);
        return newColor;
    }

    private Texture2D ConvertToTexture2D()
    {
        Texture2D texture = new Texture2D(rt.width, rt.height);
        RenderTexture.active = rt;
        texture.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        texture.Apply();
        RenderTexture.active = null;
        return texture;
    }
}
