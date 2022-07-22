using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFighter : MonoBehaviour
{
    [SerializeField] float attackDamage = 1.5f;
    [SerializeField] float attackSpeed = 1f;
    [SerializeField] Color attackColor;
    [SerializeField] Transform cameraTransform;
    IAttack attack;

    private void Awake()
    {
        attack = GetComponent<IAttack>();
    }

    public void Attack(InputAction.CallbackContext callback)
    {
        if (callback.action.WasPerformedThisFrame())
        {
            attack.Attack(attackDamage, attackSpeed, attackColor, cameraTransform, tag);
        }
    }
}
