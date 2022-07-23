using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    Vector3 respawnLocation;
    public float yThreshold = 25f;

    void Start()
    {
        respawnLocation = transform.position;
    }

    void Update()
    {
        if (transform.position.y < yThreshold)
        {
            transform.position = respawnLocation;
        }
    }
}
