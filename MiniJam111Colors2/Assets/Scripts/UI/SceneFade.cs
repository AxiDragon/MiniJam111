using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SceneFade : MonoBehaviour
{
    CanvasGroup group;

    private void Awake()
    {
        group = GetComponent<CanvasGroup>();
        StartCoroutine(Fade(0f, 2f));
    }

    public IEnumerator Fade(float targetAlpha, float time)
    {
        while (!Mathf.Approximately(targetAlpha, group.alpha))
        {
            group.alpha = Mathf.MoveTowards(group.alpha, targetAlpha, Time.deltaTime / time);
            yield return null;
        }

        group.alpha = targetAlpha;
    }

    public IEnumerator Fade(float targetAlpha, float time, int sceneIndex)
    {
        while (!Mathf.Approximately(targetAlpha, group.alpha))
        {
            group.alpha = Mathf.MoveTowards(group.alpha, targetAlpha, Time.deltaTime / time);
            yield return null;
        }

        group.alpha = targetAlpha;

        SceneManager.LoadScene(sceneIndex);
    }
}
