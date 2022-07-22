using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] GameObject groundCheck;
    [SerializeField] LayerMask mask;
    [SerializeField] float groundCheckDistance = 0.1f;
    [SerializeField] float jumpForce = 50f;
    [SerializeField] float airSpeed = 2f;
    [SerializeField] float speed = 5f;
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] float decelerationRate = 5f;

    Vector3 moveVector; 
    Vector2 input;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        ClampVelocity();
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        bool grounded = OnGround();
        if (Mathf.Approximately(moveVector.magnitude, 0f) && grounded)
        {
            Vector3 deceleration = new Vector3(rb.velocity.x, 0f, rb.velocity.z) 
                * -decelerationRate;
            rb.AddForce(deceleration);
        }
        else
        {
            Vector3 moveForce = ModifiedMovement(grounded);
            rb.AddForce(moveForce, ForceMode.Impulse);
        }
    }
    
    private Vector3 ModifiedMovement(bool grounded)
    {
        Vector3 moveForce = grounded ? moveVector * speed : moveVector * airSpeed;
        return moveForce;
    }

    private void ClampVelocity()
    {
        Vector3 clampedVelocity = Vector3.Scale(rb.velocity, new Vector3(1f, 0f, 1f));
        clampedVelocity = Vector3.ClampMagnitude(clampedVelocity, maxSpeed);
        rb.velocity = new Vector3(clampedVelocity.x, rb.velocity.y, clampedVelocity.z);
    }

    public void Move(InputAction.CallbackContext callback)
    {
        input = callback.action.ReadValue<Vector2>().normalized;
        moveVector = Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * new Vector3(input.x, 0f, input.y);
    }

    public void Jump(InputAction.CallbackContext callback)
    {
        if (OnGround() && callback.action.WasPerformedThisFrame())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool OnGround()
    {
        bool canJump = Physics.CheckSphere(groundCheck.transform.position, 
            groundCheckDistance, mask);
        return canJump;
    }
}
