using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushAttack : MonoBehaviour, IAttack
{
    [SerializeField] Transform hitPosition;
    [SerializeField] float attackRadius;
    [SerializeField] HitParticle hitEffect;

    public void Attack(float damage, float speed, Color attackColor, Transform direction, string factionTag)
    {
        foreach(Collider coll in Physics.OverlapSphere(hitPosition.position, attackRadius))
        {
            if (!coll.transform.parent)
                continue;

            if (coll.transform.parent.CompareTag("Player"))
            {
                coll.transform.parent.GetComponent<ColorChange>().BlendColor(attackColor, damage);
                SpawnParticle(attackColor);
            }
        }
    }

    void SpawnParticle(Color attackColor)
    {
        HitParticle particle = Instantiate(hitEffect, hitPosition.position, Quaternion.identity);
        particle.Activate(attackColor);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(hitPosition.position, attackRadius);
    }
}
