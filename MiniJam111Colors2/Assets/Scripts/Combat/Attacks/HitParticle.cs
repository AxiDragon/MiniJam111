using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticle : MonoBehaviour
{
    public void Activate(Color particleColor)
    {
        particleColor.a = 1f;
        ParticleSystem particle = GetComponentInChildren<ParticleSystem>();
        ParticleSystem.MainModule settings = particle.main;
        settings.startColor = new ParticleSystem.MinMaxGradient(particleColor);

        particle.Play();
        Destroy(gameObject, 2f);
    }
}
