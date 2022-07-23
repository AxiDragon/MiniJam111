using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscShoot : MonoBehaviour, IAttack
{
    [SerializeField] Bullet projectilePrefab;
    [SerializeField] Transform spawn;
    Transform target;

    private void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    public void Attack(float damage, float speed, Color color, Transform transform, string factionTag)
    {
        Bullet shotBullet = Instantiate(projectilePrefab, spawn.position + spawn.forward, spawn.rotation);
        shotBullet.transform.LookAt(target.position);
        shotBullet.SetColor(color);
        shotBullet.attackDamage = damage;
        shotBullet.faction = factionTag;
        shotBullet.speed = speed;
    }
}
