using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCheck : MonoBehaviour
{
    [SerializeField] Renderer rend;
    [SerializeField] RenderTexture textureSettings;
    [SerializeField] Camera texCam;
    [SerializeField] Color immuneColor;

    Material material;
    RenderTexture rt;
    
    [HideInInspector] public bool isSameColor;
    [HideInInspector] public float difference;
    //WebGL needs around 1
    [SerializeField] float errorMargin = 0.1f;
    float immuneHue = 0f;

    void Start()
    {
        Color.RGBToHSV(immuneColor, out immuneHue, out _, out _);

        material = GetColorable();

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
        Color newColor = GetColor();


        difference = GetDifference(material.color, newColor);

        isSameColor = difference < errorMargin;
    }

    private float GetDifference(Color curC, Color newC)
    {
        float hue1, hue2;

        Color.RGBToHSV(curC, out hue1, out _, out _);
        Color.RGBToHSV(newC, out hue2, out _, out _);

        if (Mathf.Approximately(hue2, immuneHue))
            return Mathf.Infinity;

        return Mathf.Abs(hue1 - hue2);

        //float redDif = Mathf.Abs(c1.r - c2.r);
        //float greenDif = Mathf.Abs(c1.g - c2.g);
        //float blueDif = Mathf.Abs(c1.b - c2.b);

        //return redDif + greenDif + blueDif;
    }

    private Color GetColor()
    {
        Texture2D convertedTexture = ConvertToTexture2D();
        Vector2Int center = new Vector2Int(convertedTexture.width, convertedTexture.height) / 2;
        Color newColor = convertedTexture.GetPixel(center.x, center.y);
        return newColor;
    }

    public Color GetMaterialColor()
    {
        return material.color;
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

    private Material GetColorable()
    {
        foreach(Material mat in rend.materials)
        {
            if(mat.name.Contains("Colorable"))
            {
                return mat;
            }
        }
        return rend.material;
    }
}
