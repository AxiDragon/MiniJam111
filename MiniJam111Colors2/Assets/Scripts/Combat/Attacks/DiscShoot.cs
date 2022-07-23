using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShoot : MonoBehaviour, IAttack
{
    [SerializeField] Bullet projectilePrefab;
    Transform target;

    private void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    public void Attack(float damage, float speed, Color color, Transform transform, string factionTag)
    {
        Bullet shotBullet = Instantiate(projectilePrefab, transform.position + transform.forward, transform.rotation);
        shotBullet.transform.LookAt(target.position);
        shotBullet.SetColor(color);
        shotBullet.attackDamage = damage;
        shotBullet.faction = factionTag;
        shotBullet.speed = speed;
    }
}
