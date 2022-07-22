using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFighter : MonoBehaviour
{
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] Color testColor;

    public void Attack(InputAction.CallbackContext callback)
    {
        if (callback.action.WasPerformedThisFrame())
        {
            ShootBullet();
        }
    }

    private void ShootBullet()
    {
        Vector3 forwardDirection = transform.forward;
        Bullet shotBullet = Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation);
        shotBullet.SetColor(testColor);
        //shotBullet.transform.LookAt(transform.position + forwardDirection);
    }
}
