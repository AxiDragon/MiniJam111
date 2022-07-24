using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLook : MonoBehaviour
{
    [SerializeField] float sensitivity = 100f;
    [SerializeField] Transform cameraTransform;
    float xRotation = 0f;

    void Start()
    {
        sensitivity *= StartMenuHandler.sensitivity;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void MoveCamera(InputAction.CallbackContext callback)
    {
        Vector2 rotation = callback.ReadValue<Vector2>() * sensitivity;

        xRotation -= rotation.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.Rotate(Vector3.up * rotation.x);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
