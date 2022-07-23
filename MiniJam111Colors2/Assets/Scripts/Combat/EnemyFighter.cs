using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFighter : MonoBehaviour
{
    [SerializeField] float attackDamage = 1f;
    [SerializeField] float attackCooldown = 1f;
    [SerializeField] float projectileSpeed = .2f;
    ColorCheck checkColor;
    Animator animator;
    public float range = 5f;
    public bool hasEvent = true;
    float timeSinceLastAttack = Mathf.Infinity;
    IAttack attack;

    Transform player;

    private void Awake()
    {
        checkColor = GetComponent<ColorCheck>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        attack = GetComponent<IAttack>();
    }

    void Update()
    {
        if (CheckIsInRange() && timeSinceLastAttack > attackCooldown)
        {
            animator.SetTrigger("Attack");
            timeSinceLastAttack = 0f;
            transform.LookAt(player);
            if (!hasEvent)
                LaunchAttack();
        }

        UpdateTimers();
    }

    private void UpdateTimers()
    {
        timeSinceLastAttack += Time.deltaTime;
    }

    private bool CheckIsInRange()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance > range)
            return false;
        else
            return true;

        //RaycastHit hit;
        //if (Physics.Raycast(transform.position, transform.position - player.position, out hit, range))
        //{
        //    print(hit.collider.name);
        //    return hit.collider.transform.parent.CompareTag("Player");
        //}

        //return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void LaunchAttack()
    {
        attack.Attack(attackDamage, projectileSpeed, checkColor.GetMaterialColor(), transform, tag);
        timeSinceLastAttack = 0f;
    }
}
