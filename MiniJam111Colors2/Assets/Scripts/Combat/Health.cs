using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] UnityEvent die;
    [SerializeField] bool immortal = false;
    bool updatedEnemiesSlain = false;
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
        if (health <= 0 && !immortal )
        {
            health = 0;
            Die();
        }
    }

    public float GetHealthPercentage()
    {
        return health / maxHealth;
    }

    private void Heal(float amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
    }

    public void Die()
    {
        foreach(Collider coll in GetComponentsInChildren<Collider>())
        {
            coll.enabled = false;
        }

        die.Invoke();

        if (CompareTag("Enemy"))
        {
            Destroy(gameObject, 4f);
        }

        if (updatedEnemiesSlain)
            return;

        if (CompareTag("Enemy"))
        {
            UpdateEnemiesSlain();
        }
    }

    private void UpdateEnemiesSlain()
    {
        float closestDistance = Mathf.Infinity;
        LevelCompletionCheck closestCheck = null;

        foreach (LevelCompletionCheck check in FindObjectsOfType<LevelCompletionCheck>())
        {
            float distance = Vector3.Distance(check.transform.position, transform.position);
            
            if (distance < closestDistance)
            {
                closestCheck = check;
                closestDistance = distance;
            }
        }
        
        if (closestCheck != null)
        {
            updatedEnemiesSlain = true;
            closestCheck.UpdateEnemiesSlain();
        }
    }
}
