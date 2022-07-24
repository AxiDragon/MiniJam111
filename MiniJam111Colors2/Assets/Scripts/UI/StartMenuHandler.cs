using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuHandler : MonoBehaviour
{
    public static float sensitivity = 1f;
    bool initiatingPlay = false;
    SceneFade fader;
    Slider sensitivitySlider;

    void Awake()
    {
        fader = FindObjectOfType<SceneFade>();
        sensitivitySlider = FindObjectOfType<Slider>();
    }

    public void Play()
    {
        if (initiatingPlay)
            return;

        initiatingPlay = true;
        sensitivity = sensitivitySlider.value;
        StartCoroutine(fader.Fade(1f, 1f, 1));
        StartCoroutine(BackupCoroutine());
    }

    IEnumerator BackupCoroutine()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
}
