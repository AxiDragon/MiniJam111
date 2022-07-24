using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportDoor : MonoBehaviour
{
    AudioSource teleportSound;
    public int currentLevel = 1;
    public Transform teleportLocation;
    public TeleportType type;
    public bool canTeleport;
    LevelLoadManager levelLoadManager;

    private bool entered = false;

    public enum TeleportType
    {
        Enter,
        Exit
    }

    private void Awake()
    {
        teleportSound = GameObject.FindWithTag("TeleportSound").GetComponent<AudioSource>();
        levelLoadManager = FindObjectOfType<LevelLoadManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (type == TeleportType.Enter || !canTeleport || entered)
            return;

        if (other.CompareTag("Player"))
        {
            entered = true;
            StartCoroutine(Teleport(other.gameObject));
        }
    }

    private Vector3 GetTeleportLocation()
    {
        foreach(TeleportDoor door in FindObjectsOfType<TeleportDoor>())
        {
            if (door.currentLevel != currentLevel + 1)
                continue;

            if (door.type == TeleportType.Exit)
                continue;

            return door.teleportLocation.position;
        }

        return teleportLocation.position;
    }

    IEnumerator Teleport(GameObject player)
    {
        SceneFade fader = FindObjectOfType<SceneFade>();
        teleportSound.Play();

        levelLoadManager.ChangeLevelState(currentLevel + 1, true);

        yield return fader.Fade(1f, .2f);
        
        player.transform.position = GetTeleportLocation();

        yield return fader.Fade(0f, .2f);

        levelLoadManager.ChangeLevelState(currentLevel, false);
    }
}
