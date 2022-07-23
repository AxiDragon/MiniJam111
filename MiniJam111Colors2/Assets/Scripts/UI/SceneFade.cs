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
        StartCoroutine(Fade(0f));
    }

    public IEnumerator Fade(float targetAlpha)
    {
        while (!Mathf.Approximately(targetAlpha, group.alpha))
        {
            group.alpha = Mathf.MoveTowards(group.alpha, targetAlpha, Time.deltaTime);
            yield return null;
        }

        group.alpha = targetAlpha;
    }

    public IEnumerator Fade(float targetAlpha, int sceneIndex)
    {
        while (!Mathf.Approximately(targetAlpha, group.alpha))
        {
            group.alpha = Mathf.MoveTowards(group.alpha, targetAlpha, Time.deltaTime);
            yield return null;
        }

        group.alpha = targetAlpha;

        SceneManager.LoadScene(sceneIndex);
    }
}
