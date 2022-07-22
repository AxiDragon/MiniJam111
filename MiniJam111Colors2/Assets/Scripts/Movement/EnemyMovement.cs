using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float chaseDistance = 12f;
    [SerializeField] float speed = 3.5f;
    bool alive = true;
    NavMeshAgent agent;
    Transform player;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Start()
    {
        agent.speed = speed;
    }

    void Update()
    {
        if (!alive)
            return;

        if (CheckIsInChaseDistance())
        {
            agent.SetDestination(player.position);
        }
    }

    private bool CheckIsInChaseDistance()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        return distance < chaseDistance;
    }

    public void Die()
    {
        alive = false;
    }
}
