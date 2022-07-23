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
    //bool chasing = false;
    NavMeshAgent agent;
    Transform player;
    Animator animator;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        if (TryGetComponent<Animator>(out animator))
        {

        }
    }

    void Update()
    {
        if (!alive)
            return;

        if(agent.destination != null)
        {
            float yRotation = transform.eulerAngles.y;
            transform.LookAt(agent.destination);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
        }

        if (CheckIsInChaseDistance())
        {
            Alerted();
        }
        else
        {
            Wander();
        }

        if (animator)
        animator.SetFloat("Speed", agent.velocity.magnitude);
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
        return distance < chaseDistance;
    }

    public void Alerted()
    {
        agent.SetDestination(player.position);
    }

    public void Die()
    {
        alive = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
}