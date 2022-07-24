using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafetyBox : MonoBehaviour
{
    Health playerHealth;

    private void Awake()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            playerHealth.immortal = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerHealth.immortal = false;
        }
    }
}
