using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTrailColor : MonoBehaviour
{
    void Start()
    {
        Color trailColor = transform.parent.GetComponent<Renderer>().material.color;

        Gradient gradient = new Gradient();

        GradientColorKey[] colorKey = new GradientColorKey[2];
        colorKey[0].color = trailColor;
        colorKey[0].time = 1f;
        colorKey[1].color = trailColor;
        colorKey[1].time = 0f;

        GradientAlphaKey[] alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1f;
        alphaKey[0].time = 1f;
        alphaKey[1].alpha = 1f;
        alphaKey[1].time = 0f; 

        gradient.SetKeys(colorKey, alphaKey);

        GetComponent<TrailRenderer>().colorGradient = gradient;  
    }
}
