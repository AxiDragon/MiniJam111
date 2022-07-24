using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoadManager : MonoBehaviour
{
    List<GameObject> levels = new();

    void Start()
    {
        SetUpLevelList();
    }

    private void SetUpLevelList()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            levels.Add(transform.GetChild(i).gameObject);

            if (i > 0)
                transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void ChangeLevelState(int level, bool show)
    {
        levels[level].SetActive(show);
    }
}
