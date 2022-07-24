using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float chaseDistance = 12f;
    [SerializeField] float wanderRadius = 5f;
    [SerializeField] float wanderTime = 10f;

    const int ATTEMPTS = 30;

    float timeSinceLastWander = 0f;
    bool alive = true;
    NavMeshAgent agent;
    Transform player;
    Animator animator;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!alive)
            return;

        if (CheckIsInChaseDistance())
        {
            Alerted();
        }
        else
        {
            Wander();
        }

        animator.SetFloat("Speed", agent.velocity.magnitude * 2f);
    }

    private void Wander()
    {
        timeSinceLastWander += Time.deltaTime;
        if (timeSinceLastWander > wanderTime)
        {
            timeSinceLastWander = Random.Range(0f, 2.5f);
            agent.SetDestination(GetRandomLocation());
        }
    }

    private Vector3 GetRandomLocation()
    {
        for (int i = 0; i < ATTEMPTS; i++)
        {
            Vector3 randomPoint = transform.position + Random.insideUnitSphere * wanderRadius;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 0.1f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
        return transform.position;
    }

    private bool CheckIsInChaseDistance()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance > chaseDistance)
            return false;
        else
            return true;

        //RaycastHit hit;
        //if (Physics.Raycast(player.position, transform.position, out hit, chaseDistance))
        //{
        //    return hit.collider.CompareTag("Player");
        //}

        //return false;
    }

    public void Alerted()
    {
        agent.SetDestination(player.position);
    }

    public void Die()
    {
        alive = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
}
