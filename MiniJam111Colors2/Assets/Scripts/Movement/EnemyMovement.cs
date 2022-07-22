using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float chaseDistance = 12f;
    bool alive = true;
    bool chasing = false;
    NavMeshAgent agent;
    Transform player;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (!alive)
            return;

        if (chasing)
        {
            transform.LookAt(player.position);
        }


        if (CheckIsInChaseDistance())
        {
            Alerted();
        }
    }

    private bool CheckIsInChaseDistance()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        return distance < chaseDistance;
    }

    public void Alerted()
    {
        agent.SetDestination(player.position);
        chasing = true;
    }

    public void Die()
    {
        alive = false;
    }
}
