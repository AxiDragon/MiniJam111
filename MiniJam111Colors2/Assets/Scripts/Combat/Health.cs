using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health;
    ColorCheck colorchecker;
    float maxHealth;

    private void Awake()
    {
        colorchecker = GetComponent<ColorCheck>();
    }

    void Start()
    {
        maxHealth = health;
    }

    void FixedUpdate()
    {
        if (colorchecker.isSameColor)
        {
            TakeDamage(1f);
        }
    }

    private void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    private void Heal(float amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
    }

    private void Die()
    {
        foreach(Collider coll in GetComponentsInChildren<Collider>())
        {
            coll.enabled = false;
        }
        Destroy(gameObject, 2f);
    }
}
