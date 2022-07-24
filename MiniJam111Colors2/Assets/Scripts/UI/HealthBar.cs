using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] Image background; 
    [SerializeField] Image foreground;
    [SerializeField] Image heart;
    [SerializeField] Color backgroundOffset;
    Health health;
    ColorCheck colorCheck;

    Action healthPulsateBack;

    void Awake()
    {
        healthPulsateBack = () => { LeanTween.scale(heart.gameObject, Vector3.one, 0.1f).setEaseOutSine(); };

        GameObject player = GameObject.FindWithTag("Player");
        health = player.GetComponent<Health>();
        colorCheck = player.GetComponent<ColorCheck>();
    }

    void Update()
    {
        healthSlider.value = health.GetHealthPercentage();
        Color materialColor = colorCheck.GetMaterialColor();
        Color color = new Color(materialColor.r, materialColor.g, materialColor.b, 1f);

        foreground.color = color;
        heart.color = color;
        background.color = color * backgroundOffset;
    }

    public void PulsateHeart()
    {
        LeanTween.scale(heart.gameObject, Vector3.one * 1.5f, 0.1f).setEaseOutSine().setOnComplete(healthPulsateBack);
    }
}
