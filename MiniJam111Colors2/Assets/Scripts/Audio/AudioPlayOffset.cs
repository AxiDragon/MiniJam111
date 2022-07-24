using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayOffset : MonoBehaviour
{
    [SerializeField] float offset = 0f;
    [SerializeField] AudioSource audioSource;

    public void PlayAudio()
    {
        audioSource.time = offset;
        audioSource.Play();
    }
}