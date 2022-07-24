using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CovorVeilMovement : MonoBehaviour
{
    [SerializeField] Rigidbody playerRb;
    [SerializeField] float velocitySpeed = .02f;
    public bool affectedByPlayer = true;
    public float rotationSpeed = -3f;
    Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.localPosition;
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up, rotationSpeed, Space.Self);
        if (affectedByPlayer)
            transform.localPosition = startPosition + -playerRb.velocity * velocitySpeed;
    }
}
