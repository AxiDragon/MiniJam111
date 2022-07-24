using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SceneFade : MonoBehaviour
{
    [SerializeField] bool fadeOnStart = true;
    CanvasGroup group;
    bool restarting = false;

    private void Awake()
    {
        group = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        if (fadeOnStart)
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

    public IEnumerator LoadSceneDelayed(float delay, int sceneIndex)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneIndex);
    }

    public void PlayerDeath()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(Fade(1f, .3f));
        group.interactable = true;
        group.blocksRaycasts = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void LampDestroy()
    {
        StartCoroutine(Fade(1f, 0.1f));
        StartCoroutine(LoadSceneDelayed(3f, SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void Restart()
    {
        if (restarting)
            return;

        restarting = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
