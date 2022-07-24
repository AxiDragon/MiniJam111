using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CovorVeilFinalLaunch : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] UnityEvent hitEvent;
    [SerializeField] UnityEvent startLaunch;
    [SerializeField] Light[] muteLights;
    Action hitAction;

    private void Start()
    {
        hitAction = () => { hitEvent.Invoke(); };
    }

    public void Trigger()
    {
        GetComponent<CovorVeilMovement>().affectedByPlayer = false;
        startLaunch.Invoke();

        transform.parent = null;
        StartCoroutine(Launch());
    }

    IEnumerator Launch()
    {

        Light light = GetComponent<Light>();
        Color color = light.color;
        float timer = 0f;
        float time = 5f;

        while (timer < time)
        {
            color = Color.HSVToRGB((Time.time % 1), 1f, 1f);
            light.color = color;
            light.intensity = Mathf.Lerp(0f, 10000f, timer / time);

            foreach(Light light2 in muteLights)
            {
                light2.intensity = Mathf.Lerp(light2.intensity, 1000f, timer / time);
            }

            timer += Time.deltaTime;
            yield return null;
        }

        LeanTween.move(gameObject, target.position, .2f).setEaseInQuad().setOnComplete(hitAction);
    }
}
