using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] UnityEvent die;
    [SerializeField] AudioSource sink;
    public bool immortal = false;

    bool deathInvoked = false;
    bool alive = true;
    bool updatedEnemiesSlain = false;
    ColorCheck colorchecker;
    float maxHealth;

    float sinkCooldown = 2f;
    float timeSinceLastSink = Mathf.Infinity;

    private void Awake()
    {
        colorchecker = GetComponent<ColorCheck>();
    }

    void Start()
    {
        maxHealth = health;
        
        if(CompareTag("Player"))
            StartCoroutine(ImmunityFrames(.2f));
    }

    private void Update()
    {
        timeSinceLastSink += Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (immortal)
            return;

        if (colorchecker.isSameColor)
        {
            TakeDamage(1f);
        }
    }

    private void TakeDamage(float amount)
    {
        if (timeSinceLastSink > sinkCooldown && alive)
        {
            sink.Play();
            timeSinceLastSink = 0;
        }

        health -= amount;
        if (health <= 0)
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
        alive = false;

        foreach(Collider coll in GetComponentsInChildren<Collider>())
        {
            coll.enabled = false;
        }

        if (!deathInvoked)
        {
            die.Invoke();
            deathInvoked = true;
        }

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

    IEnumerator ImmunityFrames(float time)
    {
        immortal = true;
        yield return new WaitForSeconds(time);
        immortal = false;
    }
}
