using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    [SerializeField] Renderer meshRenderer;
    [SerializeField] float resistance = 1f;
    [SerializeField] Color startingColor = Color.white;
    Material material;
    IEnumerator runningTransition;
    bool transitioning = false;

    void Awake()
    {
        material = meshRenderer.materials[0];
        material.color = startingColor;
    }

    public void BlendColor(Color blendColor, float share)
    {
        if (transitioning)
        {
            StopCoroutine(runningTransition);
        }

        Color matColor, newColor;
        GetColorBlend(blendColor, share, out matColor, out newColor);

        runningTransition = TransitionColor(matColor, newColor);
        StartCoroutine(runningTransition);
    }

    private void GetColorBlend(Color blendColor, float share, out Color matColor, out Color newColor)
    {
        float blendColorShare = share / resistance;
        matColor = material.color;
        Vector4 total = blendColor * blendColorShare + matColor;
        newColor = total / (blendColorShare + 1f);
    }

    IEnumerator TransitionColor(Color startColor, Color endColor)
    {
        transitioning = true;
        
        float timer = 0f;
        float time = .2f;
        while (timer < time)
        {
            material.color = Color.Lerp(startColor, endColor, timer / time);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transitioning = false;
    }
}
