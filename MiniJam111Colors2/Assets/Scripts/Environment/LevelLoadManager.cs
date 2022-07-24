using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoadManager : MonoBehaviour
{
    [HideInInspector] public List<GameObject> levels = new();

    [SerializeField] bool hideOnStart = true;
    [SerializeField] Transform[] checkpoints;
    public static int checkpoint = 0;

    private void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");

        for (int i = 0; i < checkpoints.Length; i++)
        {
            if (i == checkpoint)
            {
                player.transform.position = checkpoints[i].position;
                CovorVeilSegmentGetter getter = player.GetComponentInChildren<CovorVeilSegmentGetter>();

                switch (i)
                {
                    case 1: 
                        getter.hideOnStart = false;
                        break;
                    case 2: 
                        getter.hideOnStart = false;
                        getter.yellowAtStart = true;
                        break;
                    default: break;
                }
            }

        }
    }

    void Start()
    {
        SetUpLevelList();
    }

    private void SetUpLevelList()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            levels.Add(transform.GetChild(i).gameObject);

            if (i != checkpoint * 6 && hideOnStart)
                transform.GetChild(i).gameObject.SetActive(false);
            else
                transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void ChangeLevelState(int level, bool show)
    {
        levels[level].SetActive(show);
    }

    public void CheckpointReached(int checkpointValue)
    {
        checkpoint = checkpointValue;
    }
}
