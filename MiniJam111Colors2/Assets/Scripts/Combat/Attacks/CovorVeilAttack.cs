using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CovorVeilAttack : MonoBehaviour, IAttack
{
    [SerializeField] CovorVeilSegmentGetter segmentGetter;

    public void Attack(float damage, float speed, Color attackColor, Transform direction, string factionTag)
    {
        if (!segmentGetter.enabled)
            return;

        CovorVeilSegment segment = segmentGetter.GetCovorVeilSegment();
        segment.Launch(damage, speed, direction);
    }
}
