using UnityEngine;

interface IAttack
{
    public void Attack(float damage, float speed, Color attackColor, Transform transform, string factionTag);
}