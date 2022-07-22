using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float chaseDistance = 12f;
    NavMeshAgent agent;
    Transform player;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        print(CheckDistanceToPlayer());
        if (CheckDistanceToPlayer())
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
    }

    private bool CheckDistanceToPlayer()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        return distance < chaseDistance;
    }
}
