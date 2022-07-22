using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticle : MonoBehaviour
{
    public void Activate(Color particleColor)
    {
        ParticleSystem particle = GetComponentInChildren<ParticleSystem>();
        ParticleSystem.MainModule settings = particle.main;
        settings.startColor = new ParticleSystem.MinMaxGradient(particleColor);

        particle.Play();
    }
}
