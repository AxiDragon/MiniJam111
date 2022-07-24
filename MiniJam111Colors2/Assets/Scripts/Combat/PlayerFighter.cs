using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFighter : MonoBehaviour
{
    public float attackDamage = 1.5f;
    public float attackCooldown = 2f;
    [SerializeField] float attackSpeed = 1f;
    [SerializeField] Color attackColor;
    [SerializeField] Transform cameraTransform;
    public AudioSource attackAudio;
    float timeSinceLastAttack = Mathf.Infinity;
    //Animator animator;
    IAttack attack;

    private void Awake()
    {
        //animator = GetComponent<Animator>();
        attack = GetComponent<IAttack>();
    }

    public void Attack(InputAction.CallbackContext callback)
    {
        if (callback.action.WasPerformedThisFrame() && timeSinceLastAttack > attackCooldown)
        {
            attack.Attack(attackDamage, attackSpeed, attackColor, cameraTransform, tag);
            //animator.SetTrigger("Attack");
        }
    }

    //Animation Event
    //public void AnimationAttack()
    //{
    //    animator.ResetTrigger("Attack");
    //    attack.Attack(attackDamage, attackSpeed, attackColor, cameraTransform, tag);
    //}
}
