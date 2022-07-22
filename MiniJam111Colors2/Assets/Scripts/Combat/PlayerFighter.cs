using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFighter : MonoBehaviour
{
    [SerializeField] Bullet bulletPrefab;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Attack(InputAction.CallbackContext callback)
    {
        if (callback.action.WasPerformedThisFrame())
        {
            ShootBullet();
        }
    }

    private void ShootBullet()
    {
        Bullet shotBullet = Instantiate(bulletPrefab, transform.position + transform.forward, Quaternion.identity);
        shotBullet.transform.LookAt(transform.forward);
    }
}
