using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] DialogueText dialogue;
    bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;

        if (other.CompareTag("Player"))
        {
            dialogue.gameObject.SetActive(true);
            dialogue.ShowDialogue();
            triggered = true;
        }
    }
}
