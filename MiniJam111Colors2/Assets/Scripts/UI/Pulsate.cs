using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulsate : MonoBehaviour
{
    Vector3 baseScale;
    public float magnitude = .05f;

    private void Awake()
    {
        baseScale = transform.localScale;
    }

    void Update()
    {
        transform.localScale = baseScale * (1f + (Mathf.Sin(Time.time) * magnitude));
    }
}
