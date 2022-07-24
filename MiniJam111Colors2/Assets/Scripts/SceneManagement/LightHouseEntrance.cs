using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LightHouseEntrance : MonoBehaviour
{
    [SerializeField] int targetScene;
    bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;

        if (other.CompareTag("Player"))
        {
            triggered = true;   
            StartCoroutine(FindObjectOfType<SceneFade>().Fade(1f, 2f, targetScene));
        }
    }
}
