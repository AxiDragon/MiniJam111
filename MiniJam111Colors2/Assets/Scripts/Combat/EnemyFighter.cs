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
            attack.Attack(attackDamage, projectileSpeed, checkColor.GetMaterialColor(), transform, tag);
            timeSinceLastAttack = 0f;
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
        return distance < range;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
