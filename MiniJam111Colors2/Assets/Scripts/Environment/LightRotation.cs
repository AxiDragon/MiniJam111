using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotation : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.Rotate(Vector3.up, 1f);
    }
}
