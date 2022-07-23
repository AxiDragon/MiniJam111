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
    [SerializeField] Color backgroundOffset;
    Health health;
    ColorCheck colorCheck;

    void Awake()
    {
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
        background.color = color * backgroundOffset;

        
    }
}
