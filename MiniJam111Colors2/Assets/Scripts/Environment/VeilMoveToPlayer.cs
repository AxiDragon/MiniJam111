using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeilMoveToPlayer : MonoBehaviour
{
    public void MoveToPlayer()
    {
        Transform player = GameObject.FindWithTag("Player").transform;

        LeanTween.move(gameObject, player, 1f).setEaseInSine();
        LeanTween.scale(gameObject, Vector3.one * .01f, 1f).setEaseInSine()
            .setOnComplete(() => { Destroy(gameObject); });
    }
}
