using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AcrylicAttack : MonoBehaviour, IAttack
{
    public Vector3 rotationOffset;
    float attackDamage;
    float speed;
    Color attack;
    bool attacking = false;
    public UnityEvent attackEvent;

    public void Attack(float damage, float speed, Color attackColor, Transform direction, string factionTag)
    {
        attacking = true;
        attackDamage = damage;
        attack = attackColor;
        this.speed = speed;
        attackEvent.Invoke();
    }

    //Animation Event
    public void FireAnimation()
    {
        transform.LookAt(GameObject.FindWithTag("Player").transform);
        transform.eulerAngles = transform.eulerAngles + rotationOffset;
        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        Destroy(gameObject, 4f);

        while (true)
        {
            transform.Translate(transform.forward * -speed);
            yield return null;
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (attacking && other.CompareTag("Player"))
        {
            other.GetComponent<ColorChange>().BlendColor(attack, attackDamage);
            
            //if (other.TryGetComponent<Health>(out Health playerHealth))
            //{
            //    playerHealth.Die();
            //}
            
            Destroy(gameObject);
        }
    }
}
