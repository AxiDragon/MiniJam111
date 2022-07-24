using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTrailColor : MonoBehaviour
{
    void Start()
    {
        SetColor();
    }

    public void SetColor()
    {
        Color trailColor = transform.parent.GetComponent<Renderer>().material.color;

        Gradient gradient;
        GradientColorKey[] colorKey;
        GradientAlphaKey[] alphaKey;
        GetGradient(trailColor, out gradient, out colorKey, out alphaKey);

        gradient.SetKeys(colorKey, alphaKey);

        GetComponent<TrailRenderer>().colorGradient = gradient;
    }

    private void GetGradient(Color trailColor, out Gradient gradient, out GradientColorKey[] colorKey, out GradientAlphaKey[] alphaKey)
    {
        gradient = new Gradient();
        colorKey = new GradientColorKey[2];
        colorKey[0].color = trailColor;
        colorKey[0].time = 1f;
        colorKey[1].color = trailColor;
        colorKey[1].time = 0f;

        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1f;
        alphaKey[0].time = 1f;
        alphaKey[1].alpha = 1f;
        alphaKey[1].time = 0f;
    }
}
