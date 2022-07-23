using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColorChange : MonoBehaviour
{
    [SerializeField] Renderer rend;
    [SerializeField] float resistance = 1f;
    [SerializeField] Color startingColor = Color.white;
    [SerializeField] UnityEvent colorChanged;
    Material material;
    IEnumerator runningTransition;
    bool transitioning = false;

    void Awake()
    {
        material = GetColorable();
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

        colorChanged.Invoke();
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

    private Material GetColorable()
    {
        foreach (Material mat in rend.materials)
        {
            if (mat.name.Contains("Colorable"))
            {
                return mat;
            }
        }

        return rend.material;
    }
}
