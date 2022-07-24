using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelCompletionCheck : MonoBehaviour
{
    [SerializeField] UnityEvent completionEvent;
    bool eventTriggered = false;

    public int enemiesRequired = 1;
    public TeleportDoor door;
    int enemiesSlain = 0;

    public void UpdateEnemiesSlain()
    {
        if (eventTriggered)
            return;

        enemiesSlain++;

        if (enemiesSlain >= enemiesRequired)
        {
            LevelComplete();
        }
    }

    public void LevelComplete()
    {
        eventTriggered = true;
        door.canTeleport = true;
        completionEvent.Invoke();
    }
}
